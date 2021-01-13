using AutoMapper;
using HMS.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HMS.DAL.Repository
{
    public class BookingRepository : IBookingRepository
    {
        private readonly Database.HotelManagementEntities _dbContext;

        public BookingRepository()
        {
            _dbContext = new Database.HotelManagementEntities();
        }
        public string CreateBooking(Booking model)
        {
            try
            {
                if (model != null)
                {
                    Database.Booking entity = new Database.Booking();
                    var config = new MapperConfiguration(cfg => cfg.CreateMap<Booking, Database.Booking>());
                    var Mapper = new Mapper(config);
                    entity = Mapper.Map<Database.Booking>(model);
                    entity.BookingDate = DateTime.Parse(entity.BookingDate.ToString());
                    _dbContext.Bookings.Add(entity);
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

        public string DeleteBooking(int id)
        {
            try
            {
                var entity = _dbContext.Bookings.Find(id);
                if (entity != null)
                {
                    entity.Status = "Deleted";
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

        public List<Booking> GetAllBookings()
        {
            var entities = _dbContext.Bookings.ToList();

            List<Booking> bookingList = new List<Booking>();

            if (entities != null)
            {
                var config = new MapperConfiguration(cfg => cfg.CreateMap<Database.Booking, Booking>());
                var Mapper = new Mapper(config);
                bookingList = (List<Booking>)Mapper.Map<List<Booking>>(entities);
            }
            return bookingList;
        }

        public Booking GetBooking(int id)
        {
            var entity = _dbContext.Bookings.Find(id);

            Booking booking = new Booking();
            if (entity != null)
            {
                var config = new MapperConfiguration(cfg => cfg.CreateMap<Database.Booking, Booking>());
                var Mapper = new Mapper(config);
                booking = Mapper.Map<Booking>(entity);
            }
            return booking;
        }

        public string UpdateBooking(Booking model)
        {
            try
            {
                var entity = _dbContext.Bookings.Find(model.Id);
                if (entity != null)
                {
                    if (model.BookingDate != null)
                    {
                        entity.BookingDate = DateTime.Parse(model.BookingDate.ToString());
                    }
                    if (model.Status != null)
                    {
                        entity.Status = model.Status;
                    }
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