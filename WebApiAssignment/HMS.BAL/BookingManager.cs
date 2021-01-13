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
    public class BookingManager : IBookingManager
    {
        private readonly IBookingRepository _bookingRepository;

        public BookingManager(IBookingRepository bookingRepository)
        {
            _bookingRepository = bookingRepository;
        }

        public string CreateBooking(Booking model)
        {
            return _bookingRepository.CreateBooking(model);
        }

        public string DeleteBooking(int id)
        {
            return _bookingRepository.DeleteBooking(id);
        }

        public List<Booking> GetAllBookings()
        {
            return _bookingRepository.GetAllBookings();
        }

        public Booking GetBooking(int id)
        {
            return _bookingRepository.GetBooking(id);
        }

        public string UpdateBooking(Booking model)
        {
            return _bookingRepository.UpdateBooking(model);
        }
    }
}
