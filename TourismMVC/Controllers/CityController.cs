using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Tourism.Core.Entities;
using Tourism.Core.Repositories.Contract;

namespace TourismMVC.Controllers
{
    public class CityController : Controller
    {
        private readonly IUnitOfWork<City> _unitOfWork;

        public CityController(IUnitOfWork<City> unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        // GET: CityController
        public async Task<IActionResult> Index()
        {
            IEnumerable<City> cities;
             cities = await _unitOfWork.generic.GetAllAsync();

            if (cities is not null)
                return View(cities);
            else
                return BadRequest();

        }

        // GET: CityController/Details/5
        public async Task<IActionResult> Details(int? id, string viewname = "Details")
        {
            if (id is null)
                return BadRequest();

            var city = await _unitOfWork.generic.GetAsync(id.Value);

            if (city is null)
                return NotFound();  

            return View(viewname,city);
        }

        // GET: CityController/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: CityController/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create(City city)
        {
            if (ModelState.IsValid)
            {
                _unitOfWork.generic.Add(city);
                var count = _unitOfWork.Complet();

                if (count > 0)
                    TempData["message"] = "City Created succesfully";
                else
                    TempData["message"] = "City Failed Created";

                return RedirectToAction("Index");
            }
            return View(city);
        }

        // GET: CityController/Edit/5
        public async Task<IActionResult> Edit(int id)
        {
            return await Details(id,"Edit");
        }

        // POST: CityController/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([FromRoute]int id, City city)
        {
            if (id != city.Id)
                return BadRequest();

            if (ModelState.IsValid)  //server side validation
            {
                try
                {

                    _unitOfWork.generic.Update(city);
                    var count = _unitOfWork.Complet();

                    return RedirectToAction(nameof(Index));
                }
                catch (Exception ex)
                {
                    ModelState.AddModelError(string.Empty, ex.Message);
                }


            }
            return View(city);
        }

        // GET: CityController/Delete/5
        public async  Task<IActionResult> Delete(int id)
        {
            return await Details(id,"Delete");
        }

        // POST: CityController/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Delete([FromRoute]int id, City city)
        {
            if (id != city.Id)
                return BadRequest();

            if (ModelState.IsValid)  //server side validation
            {
                try
                {

                    _unitOfWork.generic.Delete(city);
                    var count = _unitOfWork.Complet();

                    return RedirectToAction(nameof(Index));
                }
                catch (Exception ex)
                {
                    ModelState.AddModelError(string.Empty, ex.Message);
                }


            }
            return View(city);
        }
    }
}
