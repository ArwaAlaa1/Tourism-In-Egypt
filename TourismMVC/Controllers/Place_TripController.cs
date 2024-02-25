using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Tourism.Core.Entities;
using Tourism.Core.Repositories.Contract;
using TourismMVC.ViewModels;

namespace TourismMVC.Controllers
{
    public class Place_TripController : Controller
    {
        private readonly IUnitOfWork<Place_Trip> _placetripunit;
        private readonly IMapper mapper;
        private readonly IUnitOfWork<Place> _placeUnit;
        private readonly IUnitOfWork<Trip> _tripUnit;

        public Place_TripController(IUnitOfWork<Place_Trip> placetripunit , IMapper mapper , IUnitOfWork<Place> placeUnit , IUnitOfWork<Trip> tripUnit)
        {
            _placetripunit = placetripunit;
            this.mapper = mapper;
            _placeUnit = placeUnit;
            _tripUnit = tripUnit;
        }
        //Get: Show all place_trips 
        public async Task<IActionResult> Index()
        {
            var all = _placetripunit.placeTrip.GetAllWithPlaceAndTrip();

            return View(all);
        }

        //Get : Get one 
        public async Task<IActionResult> Details(int id)
        {
            var one = await _placetripunit.placeTrip.GetOneWithPlaceAndTrip(id);

            if (one == null)
            {
                return NotFound();  
            }
            else 
                return View(one);
        }
        
        //Get : Open form of create
        public async Task<IActionResult> Create()
        {
            var PlaceList = await _placeUnit.generic.GetAllAsync();
            var TripList = await _tripUnit.generic.GetAllAsync();

            Place_TripModel tripModel = new Place_TripModel();

            tripModel.places = PlaceList;
            tripModel.trips = TripList;
            return View(tripModel);  
        }
    }
}
