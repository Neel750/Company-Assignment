using HMS.BAL.Interface;
using HMS.DAL.Repository;
using HMS.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HMS.BAL
{
    public class RoomManager : IRoomManager
    {
        private readonly IRoomRepository _roomRepository;

        public RoomManager(IRoomRepository roomRepository)
        {
            _roomRepository = roomRepository;
        }

        public bool AvailableRoom(int id, DateTime date)
        {
            return _roomRepository.AvailableRoom(id, date);
        }

        public string BookRoom(Booking model, string status)
        {
            return _roomRepository.BookRoom(model, status);
        }

        public string CreateRoom(Room model)
        {
            return _roomRepository.CreateRoom(model);
        }

        public string DeleteRoom(int id)
        {
            return _roomRepository.DeleteRoom(id);
        }

        public List<Room> GetAllRooms()
        {
            return _roomRepository.GetAllRooms();
        }

        public List<Room> GetAllRoomsBy(string city, int pincode, int price, string category)
        {
            return _roomRepository.GetAllRoomsBy(city, pincode, price, category);
        }

        public Room GetRoom(int id)
        {
            return _roomRepository.GetRoom(id);
        }

        public string UpdateBookingDateByRoom(DateTime date, int bookingId)
        {
            return _roomRepository.UpdateBookingDateByRoom(date, bookingId);
        }

        public string UpdateRoom(Room model)
        {
            return _roomRepository.UpdateRoom(model);
        }
    }
}
