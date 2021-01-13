using AutoMapper;
using HMS.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HMS.DAL.Repository
{
    public class RoomRepository : IRoomRepository
    {
        private readonly Database.HotelManagementEntities _dbContext;

        public RoomRepository()
        {
            _dbContext = new Database.HotelManagementEntities();
        }

        public bool AvailableRoom(int id, DateTime date)
        {
            var entity = _dbContext.Rooms.Find(id);
            var bookings = entity.Bookings;
            foreach (var item in bookings)
            {
                if (item.BookingDate.Equals(date))
                {
                    return false;
                }
            }
            return true;
        }

        public string BookRoom(Booking model, String status)
        {
            try
            {
                if (model != null)
                {
                    var room = _dbContext.Rooms.Find(model.RoomId);
                    var alreadybooked = room.Bookings;
                    foreach (var item in alreadybooked)
                    {
                        if (item.BookingDate == model.BookingDate)
                        {
                            return "Already Booked On " + model.BookingDate + ".";
                        }
                    }
                    var config = new MapperConfiguration(cfg => cfg.CreateMap<Booking, Database.Booking>());
                    var Mapper = new Mapper(config);
                    var booking = Mapper.Map<Database.Booking>(model);
                    booking.Status = status;
                    room.Bookings.Add(booking);
                    _dbContext.SaveChanges();
                    return "Successfully Booked!";
                }
                else
                {
                    return "Model Is Empty.";
                }
            }
            catch (Exception ex)
            {
                return ex.Message;
            }
        }

        public string CreateRoom(Room model)
        {
            try
            {
                if (model != null)
                {
                    Database.Room entity = new Database.Room();
                    var config = new MapperConfiguration(cfg => cfg.CreateMap<Room, Database.Room>());
                    var Mapper = new Mapper(config);
                    entity = Mapper.Map<Database.Room>(model);
                    entity.CreatedDate = DateTime.Now;
                    _dbContext.Rooms.Add(entity);
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

        public string DeleteRoom(int id)
        {
            try
            {
                var entity = _dbContext.Rooms.Find(id);
                if (entity != null)
                {
                    _dbContext.Rooms.Remove(entity);
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

        public List<Room> GetAllRooms()
        {
            var entities = _dbContext.Rooms.ToList();

            List<Room> roomList = new List<Room>();

            if (entities != null)
            {
                var config = new MapperConfiguration(cfg => cfg.CreateMap<Database.Room, Room>());
                var Mapper = new Mapper(config);
                roomList = (List<Room>)Mapper.Map<List<Room>>(entities);
            }
            return roomList;
        }

        public List<Room> GetAllRoomsBy(string city, int pincode, int price, string category)
        {
            var hotels = _dbContext.Hotels.ToList();
            var config = new MapperConfiguration(cfg => cfg.CreateMap<Database.Room, Room>());
            var Mapper = new Mapper(config);
            List<Room> rooms = null;
            if (city != null || pincode != -1 || price != -1 || category != null)
            {
                if (city != null)
                {
                    rooms = new List<Room>();
                    hotels = hotels.Where(x => x.City.Equals(city)).ToList();
                    foreach (var item in hotels)
                    {
                        var room = item.Rooms.ToList();
                        foreach (var items in room)
                        {
                            var temp = Mapper.Map<Room>(items);
                            rooms.Add(temp);
                        }
                    }
                }
                if (pincode != -1)
                {
                    if (rooms != null)
                    {
                        rooms.Clear();
                        hotels = hotels.Where(x => x.Pincode.Equals(pincode) && x.City.Equals(city)).ToList();
                        foreach (var item in hotels)
                        {
                            var room = item.Rooms.ToList();
                            foreach (var items in room)
                            {
                                var temp = Mapper.Map<Room>(items);
                                rooms.Add(temp);
                            }
                        }
                    }
                    else
                    {
                        hotels = hotels.Where(x => x.Pincode.Equals(pincode)).ToList();
                        rooms = new List<Room>();
                        foreach (var item in hotels)
                        {
                            var room = item.Rooms.ToList();
                            foreach (var items in room)
                            {
                                var temp = Mapper.Map<Room>(items);
                                rooms.Add(temp);
                            }
                        }
                    }
                }
                if (price != -1)
                {
                    if (rooms != null)
                    {
                        rooms = rooms.Where(x => x.Price.Equals(price)).ToList();
                    }
                    else
                    {
                        rooms = new List<Room>();
                        foreach (var item in hotels)
                        {
                            var room = item.Rooms.ToList().Where(x => x.Price.Equals(price));
                            foreach (var items in room)
                            {
                                var temp = Mapper.Map<Room>(items);
                                rooms.Add(temp);
                            }
                        }
                    }
                }
                if (category != null)
                {
                    if (rooms != null)
                    {
                        rooms = rooms.Where(x => x.Category.Equals(category)).ToList();
                    }
                    else
                    {
                        rooms = new List<Room>();
                        foreach (var item in hotels)
                        {
                            var room = item.Rooms.ToList().Where(x => x.Category.Equals(category));
                            foreach (var items in room)
                            {
                                var temp = Mapper.Map<Room>(items);
                                rooms.Add(temp);
                            }
                        }
                    }
                }
            }
            else
            {
                rooms = new List<Room>();
                foreach (var item in hotels)
                {
                    var room = item.Rooms.ToList();
                    foreach (var items in room)
                    {
                        var temp = Mapper.Map<Room>(items);
                        rooms.Add(temp);
                    }
                }
            }
            rooms.OrderBy(x => x.Price);
            return rooms;
        }

        public Room GetRoom(int id)
        {
            var entity = _dbContext.Rooms.Find(id);

            Room room = new Room();
            if (entity != null)
            {
                var config = new MapperConfiguration(cfg => cfg.CreateMap<Database.Room, Room>());
                var Mapper = new Mapper(config);
                room = Mapper.Map<Room>(entity);
            }
            return room;
        }

        public string UpdateBookingDateByRoom(DateTime date, int bookingId)
        {
            try
            {
                var entity = _dbContext.Bookings.ToList();
                if (entity != null)
                {
                    var Booking = entity.Where(x => x.Id.Equals(bookingId)).FirstOrDefault();
                    Booking.BookingDate = date;
                    _dbContext.SaveChanges();
                    return "Booking Date Updated!";
                }
                return "No data found";
            }
            catch (Exception ex)
            {
                return ex.Message;
            }
        }

        public string UpdateRoom(Room model)
        {
            try
            {
                var entity = _dbContext.Rooms.Find(model.Id);
                if (entity != null)
                {
                    if (model.Name != null)
                    {
                        entity.Name = model.Name;
                    }
                    if (model.Category != null)
                    {
                        entity.Category = model.Category;
                    }
                    if (model.Price != -1)
                    {
                        entity.Price = model.Price;
                    }
                    if (model.IsActive != -1)
                    {
                        entity.IsActive = model.IsActive;
                    }
                    entity.UpdatedBy = model.UpdatedBy;
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
