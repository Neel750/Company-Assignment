using AutoMapper;
using NLog;
using PMS.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PMS.DAL.Repository
{
    public class UserRepository : IUserRepository
    {
        private readonly Database.PMSEntities _dbContext;
        private readonly Logger loggerFile;
        private readonly Logger loggerDB;
        //Controller
        public UserRepository()
        {
            _dbContext = new Database.PMSEntities();
            loggerFile = LogManager.GetCurrentClassLogger();//log into file
            loggerDB = LogManager.GetLogger("databaseLogger");//log into database
        }
        //Authenticate Accept Email Id and Password As Parameter And Sends List Of String Including Name And Id.
        public List<string> AuthenticateUser(UserData model)
        {
            var entity = _dbContext.UserDatas.Where(x => x.Email.Equals(model.Email) && x.Password.Equals(model.Password)).FirstOrDefault();//Finds User
            List<string> session = new List<string>();
            if (entity != null)//If User Exist
            {
                session.Add(entity.Name);
                session.Add(entity.Id.ToString());
                return session;//Return List Of String
            }
            session.Add("Not Found");
            return session;//Not Found
        }
        //Create new user. Accept Userdata type as argument. Return List of string which includes name and id.
        public List<string> CreateUser(UserData model)
        {
            List<string> session = null;
            try
            {
                if (model != null)
                {
                    session = new List<string>();
                    Database.UserData entity = new Database.UserData();
                    var config = new MapperConfiguration(cfg => cfg.CreateMap<UserData, Database.UserData>());
                    var Mapper = new Mapper(config);
                    entity = Mapper.Map<Database.UserData>(model);//Mapper map type UserData to Database.UserData
                    entity.CreatedDate = DateTime.Now;//Created Date
                    _dbContext.UserDatas.Add(entity);//Add User Data
                    _dbContext.SaveChanges();
                    session.Add(entity.Name);
                    session.Add(entity.Id.ToString());
                    return session;//Return List Of String
                }
                return session;//If model is null
            }
            catch (Exception ex)
            {
                loggerFile.Error("Error While Creating User Error = " + ex.Message);//Log error into file
                loggerDB.Error("Error While Creating User Error = " + ex.Message);//Log error into database
                return session;
            }
        }
    }
}
