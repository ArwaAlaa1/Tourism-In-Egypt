using Microsoft.AspNetCore.Mvc;
using Tourism.Core.Entities;
using Tourism.Core.Repositories.Contract;

namespace TourismMVC.Controllers
{
    public class TripController : Controller
    {
        IUnitOfWork<Trip> _unitOfWork;

        public TripController(IUnitOfWork<Trip> _unitOfWork)
        {
            this._unitOfWork = _unitOfWork;
        }
        //Get Trip

        public async Task<IActionResult> Details(int id)
        {

            Trip trip = await _unitOfWork.generic.GetAsync(id);

            if (trip == null)
            {
                return RedirectToAction(nameof(Index));
            }
            else
                return View(trip);
        }


        //Get AllTrips
        public async Task<IActionResult> Index()
        {
            IEnumerable<Trip> trips;
            trips = await _unitOfWork.generic.GetAllAsync();

            return PartialView(trips);
        }

        //Open view of create trip

        public IActionResult Create()
        {
            return View();
        }
        //post : save add
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create(Trip trip)
        {
            if (ModelState.IsValid)
            {
                try
                {



                    _unitOfWork.generic.Add(trip);
                    _unitOfWork.Complet();


                    return RedirectToAction("Index");
                }

                catch (Exception ex)
                {
                    ModelState.AddModelError(string.Empty, ex.InnerException.Message);
                }
            }

            return View("Create");
        }

        //Get : open form of edit
        public async Task<IActionResult> Edit(int id)
        {

            var trip = await _unitOfWork.generic.GetAsync(id);

            if (trip == null)
            {
                return RedirectToAction("Index");
            }
            else
                return View(trip);
        }

        //post : save edit
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Edit(int id, Trip trip)
        {
            if (ModelState.IsValid)
            {
                try
                {

                    _unitOfWork.generic.Update(trip);
                    _unitOfWork.Complet();

                    return RedirectToAction("Index");
                }

                catch (Exception ex)
                {
                    ModelState.AddModelError(string.Empty, ex.Message);
                }

            }



            return View("Edit", trip);
        }

        //Get : open form of delete

        public async Task<IActionResult> Delete(int id)
        {

            var trip = await _unitOfWork.generic.GetAsync(id);
            if (trip == null)
            {
                return RedirectToAction(nameof(Index));
            }
            else
                return View(trip);
        }

        // post : Delete 
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Delete(int id, Trip trip)
        {
            try
            {
                _unitOfWork.generic.Delete(trip);
                _unitOfWork.Complet();
                return RedirectToAction(nameof(Index));


            }
            catch
            {
                return View("Delete", trip);
            }
        }
    }
}
