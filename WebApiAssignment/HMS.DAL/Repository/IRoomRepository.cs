using HMS.Models;
using System;
using System.Collections.Generic;

namespace HMS.DAL.Repository
{
    public interface IRoomRepository
    {
        Room GetRoom(int id);
        List<Room> GetAllRooms();
        string CreateRoom(Room model);
        string UpdateRoom(Room model);
        string DeleteRoom(int id);
        List<Room> GetAllRoomsBy(string city, int pincode, int price, string category);
        string BookRoom(Booking model, string status);
        Boolean AvailableRoom(int id, DateTime date);

        string UpdateBookingDateByRoom(DateTime date, int bookingId);
    }
}
