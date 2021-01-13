using HMS.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HMS.BAL.Interface
{
    public interface IRoomManager
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
