using HMS.BAL.Interface;
using HMS.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Web.Http.Cors;

namespace HMS.WebApi.Controllers
{
    [Authorize]
    [EnableCors("*", "*", "*")]
    public class BookingController : ApiController
    {
        private readonly IBookingManager _bookingManager;

        public BookingController(IBookingManager bookingManager)
        {
            _bookingManager = bookingManager;
        }
        [Route("api/Booking/")]
        public IHttpActionResult GetBooking()
        {
            return Ok(_bookingManager.GetAllBookings());
        }

        [Route("api/Booking/{id}")]
        public IHttpActionResult GetBookingById(int id)
        {
            return Ok(_bookingManager.GetBooking(id));
        }

        [Route("api/Booking/MakeBooking")]
        public IHttpActionResult PostMakeBooking([FromBody] Booking model)
        {
            return Ok(_bookingManager.CreateBooking(model));
        }

        [Route("api/Booking/UpdateBooking")]
        public IHttpActionResult PutUpdateBooking([FromBody] Booking model)
        {
            return Ok(_bookingManager.UpdateBooking(model));
        }

        [Route("api/Booking/{id}/Delete")]
        public IHttpActionResult DeleteBookingById(int id)
        {
            return Ok(_bookingManager.DeleteBooking(id));
        }
    }
}
