using HMS.BAL.Interface;
using HMS.Models;
using Microsoft.AspNetCore.Cors;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Web.Http.Cors;
using EnableCorsAttribute = System.Web.Http.Cors.EnableCorsAttribute;

namespace HMS.WebApi.Controllers
{
    [Authorize]
    [EnableCors("*", "*", "*")]
    public class HotelController : ApiController
    {
        private readonly IHotelManager _hotelManager;

        public HotelController(IHotelManager hotelManager)
        {
            _hotelManager = hotelManager;
        }
        [Route("api/Hotel/List")]
        public IHttpActionResult GetHotels()
        {
            return Ok(_hotelManager.GetAllHotels());
        }

        [Route("api/Hotel/{id}")]
        public IHttpActionResult GetHotelById(int id)
        {
            return Ok(_hotelManager.GetHotel(id));
        }
        [Route("api/Hotel/")]
        public IHttpActionResult PostCreateHotel([FromBody] Hotel model)
        {
            return Ok(_hotelManager.CreateHotel(model));
        }

        [Route("api/Hotel/Update")]
        public IHttpActionResult PutUpdateHotel([FromBody] Hotel model)
        {
            return Ok(_hotelManager.UpdateHotel(model));
        }

        [Route("api/Hotel/{id}/Delete")]
        public IHttpActionResult DeleteHotel(int id)
        {
            return Ok(_hotelManager.DeleteHotel(id));
        }
    }
}
