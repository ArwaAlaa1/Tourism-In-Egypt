using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Tourism.Core.Entities;
using Tourism.Core.Repositories.Contract;
using TourismMVC.ViewModels;

namespace TourismMVC.Controllers
{
    public class PlaceController : Controller
    {
        private readonly IUnitOfWork<Place> _unitOfWork;
		private readonly IMapper mapper;
		private readonly IGenericRepository<City> city;
		private readonly IGenericRepository<Category> category;

		public PlaceController(IUnitOfWork<Place> unitOfWork,IMapper mapper,
            IGenericRepository<City> city , IGenericRepository<Category> Category)
        {
            _unitOfWork = unitOfWork;
			this.mapper = mapper;
			this.city = city;
			category = Category;
		}

        // GET: CityController
        public async Task<IActionResult> Index()
        {
            IEnumerable<Place> places;
            places = await _unitOfWork.generic.GetAllAsync();

			var placeviewModelList = mapper.Map<IEnumerable<Place>, IEnumerable<PlaceViewModel> >(places);

			if (placeviewModelList is not null)
                return View(placeviewModelList);
            else
                return BadRequest();

        }

        // GET: CityController/Details/5
        public async Task<IActionResult> Details(int? id, string viewname = "Details")
        {
            if (id is null)
                return BadRequest();

            var place = await _unitOfWork.generic.GetAsync(id.Value);

            var placemapped = mapper.Map<Place,PlaceViewModel >(place);

            if (placemapped is null)
                return NotFound();

            return View(viewname, placemapped);
        }

        // GET: CityController/Create
        public async Task<IActionResult> Create()
        {
            PlaceViewModel place = new PlaceViewModel();

            var cities =await  city.GetAllAsync();
			var categories = await category.GetAllAsync();

			place.Cities_List = cities;
			place.Categories_List = categories;
			return View(place);
		}

        // POST: CityController/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(PlaceViewModel place)
        {
            //var CityGet=await city.GetAsync(place.CityId);
            //place.City = CityGet;

            //var CategoryGet = await category.GetAsync(place.CategoryId);
            //place.Category = CategoryGet;
            
            var placemapped=mapper.Map< PlaceViewModel,Place>(place);
            
			if (ModelState.IsValid)
            { 
              
               
                _unitOfWork.generic.Add(placemapped);
                var count = _unitOfWork.Complet();

                if (count > 0)
                    TempData["message"] = "place Created succesfully";
                else
                    TempData["message"] = "place Failed Created";

                return RedirectToAction("Index");
            }
            var cities = await city.GetAllAsync();
            var categories = await category.GetAllAsync();

            place.Cities_List = cities;
            place.Categories_List = categories;
            return View(place);
        }

        // GET: CityController/Edit/5
        public async Task<IActionResult> Edit(int id)
        {
            return await Details(id, "Edit");
        }

        // POST: CityController/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([FromRoute] int id, PlaceViewModel placeVM)
        {
            if (id != placeVM.Id)
                return BadRequest();

            if (ModelState.IsValid)  //server side validation
            {
                try
                {
                    var placemapped = mapper.Map<PlaceViewModel, Place>(placeVM);
                    _unitOfWork.generic.Update(placemapped);
                    var count = _unitOfWork.Complet();

                    return RedirectToAction(nameof(Index));
                }
                catch (Exception ex)
                {
                    ModelState.AddModelError(string.Empty, ex.Message);
                }


            }
            return View(placeVM);
        }

        // GET: CityController/Delete/5
        public async Task<IActionResult> Delete(int id)
        {
            return await Details(id, "Delete");
        }

        // POST: CityController/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Delete([FromRoute] int id, PlaceViewModel placeVM)
        {
            if (id != placeVM.Id)
                return BadRequest();


            var placemapped = mapper.Map<PlaceViewModel, Place>(placeVM);
            if (ModelState.IsValid)  //server side validation
            {
                try
                {
                    
                    _unitOfWork.generic.Delete(placemapped);
                    var count = _unitOfWork.Complet();

                    return RedirectToAction(nameof(Index));
                }
                catch (Exception ex)
                {
                    ModelState.AddModelError(string.Empty, ex.Message);
                }


            }
            return View(placeVM);
        }
    }
}
