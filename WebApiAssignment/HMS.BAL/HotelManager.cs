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
    public class HotelManager : IHotelManager
    {
        private readonly IHotelRepository _hotelRepository;

        public HotelManager(IHotelRepository hotelRepository)
        {
            _hotelRepository = hotelRepository;
        }

        public string CreateHotel(Hotel model)
        {
            return _hotelRepository.CreateHotel(model);
        }

        public string DeleteHotel(int id)
        {
            return _hotelRepository.DeleteHotel(id);
        }

        public List<Hotel> GetAllHotels()
        {
            return _hotelRepository.GetAllHotels();
        }

        public Hotel GetHotel(int id)
        {
            return _hotelRepository.GetHotel(id);
        }

        public string UpdateHotel(Hotel model)
        {
            return _hotelRepository.UpdateHotel(model);
        }
    }
}
