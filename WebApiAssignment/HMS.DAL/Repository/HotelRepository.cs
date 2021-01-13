using HMS.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
namespace HMS.DAL.Repository
{
    public class HotelRepository : IHotelRepository
    {
        private readonly Database.HotelManagementEntities _dbContext;

        public HotelRepository()
        {
            _dbContext = new Database.HotelManagementEntities();
        }
        public string CreateHotel(Hotel model)
        {
            try
            {
                if (model != null)
                {
                    Database.Hotel entity = new Database.Hotel();
                    var config = new MapperConfiguration(cfg => cfg.CreateMap<Hotel, Database.Hotel>());
                    var Mapper = new Mapper(config);
                    entity = Mapper.Map<Database.Hotel>(model);
                    entity.CreatedDate = DateTime.Now;
                    _dbContext.Hotels.Add(entity);
                    _dbContext.SaveChanges();
                    return "Successfully Added!";
                }
                return "Model Is Null!";
            }
            catch (Exception ex)
            {
                return ex.Message;
            }

        }

        public string DeleteHotel(int id)
        {
            try
            {
                var entity = _dbContext.Hotels.Find(id);
                if (entity != null)
                {
                    _dbContext.Hotels.Remove(entity);
                    _dbContext.SaveChanges();
                    return "Deleted!";
                }
                return "No data found";
            }
            catch (Exception ex)
            {
                return ex.Message;
            }
        }

        public List<Hotel> GetAllHotels()
        {
            var entities = _dbContext.Hotels.ToList().OrderBy(s => s.Name);

            List<Hotel> hotelList = new List<Hotel>();

            if (entities != null)
            {
                var config = new MapperConfiguration(cfg => cfg.CreateMap<Database.Hotel, Hotel>());
                var Mapper = new Mapper(config);
                hotelList = (List<Hotel>)Mapper.Map<List<Hotel>>(entities);
            }
            return hotelList;
        }

        public Hotel GetHotel(int id)
        {
            var entity = _dbContext.Hotels.Find(id);

            Hotel hotel = new Hotel();
            if (entity != null)
            {
                var config = new MapperConfiguration(cfg => cfg.CreateMap<Database.Hotel, Hotel>());
                var Mapper = new Mapper(config);
                hotel = Mapper.Map<Hotel>(entity);
            }
            return hotel;
        }

        public string UpdateHotel(Hotel model)
        {
            try
            {
                var entity = _dbContext.Hotels.Find(model.Id);
                if (entity != null)
                {
                    if (model.Name != null)
                    {
                        entity.Name = model.Name;
                    }
                    if (model.Address != null)
                    {
                        entity.Address = model.Address;
                    }
                    if (model.City != null)
                    {
                        entity.City = model.City;
                    }
                    if (model.Pincode != 0)
                    {
                        entity.Pincode = model.Pincode;
                    }
                    if (model.ContactNumber != 0)
                    {
                        entity.ContactNumber = model.ContactNumber;
                    }
                    if (model.ContactPerson != null)
                    {
                        entity.ContactPerson = model.ContactPerson;
                    }
                    if (model.Website != null)
                    {
                        entity.Website = model.Website;
                    }
                    if (model.Address != null)
                    {
                        entity.Address = model.Address;
                    }
                    if (model.Facebook != null)
                    {
                        entity.Facebook = model.Facebook;
                    }
                    if (model.Twitter != null)
                    {
                        entity.Twitter = model.Twitter;
                    }
                    if (model.IsActive != 0)
                    {
                        entity.IsActive = model.IsActive;
                    }
                    entity.UpdatedDate = DateTime.Now;
                    _dbContext.SaveChanges();
                    return "Updated!";
                }
                return "No data found";
            }
            catch (Exception ex)
            {
                return ex.Message;
            }
        }
    }
}
