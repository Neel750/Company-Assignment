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
    public class RoomController : ApiController
    {
        private readonly IRoomManager _roomManager;

        public RoomController(IRoomManager roomManager)
        {
            _roomManager = roomManager;
        }

        [Route("api/Room/By/{city?}/{pincode:int?}/{price:int?}/{category?}/")]
        public IHttpActionResult GetAllRoomsBy(string city = null, int pincode = -1, int price = -1, string category = null)
        {
            return Ok(_roomManager.GetAllRoomsBy(city, pincode, price, category));
        }

        public IHttpActionResult GetCheckAvailableRoom(int roomId, string date)
        {
            return Ok(_roomManager.AvailableRoom(roomId, DateTime.Parse(date)));
        }

        [Route("api/Room/{id}/Id")]
        public IHttpActionResult GetRoom(int id)
        {
            return Ok(_roomManager.GetRoom(id));
        }

        [Route("api/Room/Book/")]
        public IHttpActionResult PostBookRoom(Booking booking, string status = "Optional")
        {
            return Ok(_roomManager.BookRoom(booking, status));
        }

        [Route("api/Room/")]
        public IHttpActionResult PostCreateRoom([FromBody] Room model)
        {
            return Ok(_roomManager.CreateRoom(model));
        }

        [Route("api/Room/Update")]
        public IHttpActionResult PutUpdateRoom([FromBody] Room model)
        {
            return Ok(_roomManager.UpdateRoom(model));
        }

        [Route("api/Room/{id}/Delete")]
        public IHttpActionResult DeleteRoom(int id)
        {
            return Ok(_roomManager.DeleteRoom(id));
        }

        public IHttpActionResult PutUpdateBookingDateByRoom([FromBody] Booking book)
        {
            return Ok(_roomManager.UpdateBookingDateByRoom((DateTime)book.BookingDate, book.Id));
        }
    }
}
