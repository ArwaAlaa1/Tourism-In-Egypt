using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Tourism.Core.Entities;
using Tourism.Core.Repositories.Contract;
using Tourism.Repository;
using Tourism.Repository.Data;
using TourismMVC.ViewModels;

namespace TourismMVC.Controllers
{
    public class CityPhotosController : Controller
    {
		private readonly IUnitOfWork<CityPhotos> unitOfWork;
		
		private readonly IMapper mapper;
        private readonly TourismContext context;

        public CityPhotosController(IUnitOfWork<CityPhotos> unitOfWork,
		 IMapper mapper)
		{
			this.unitOfWork = unitOfWork;
			
			this.mapper = mapper;
            
        }

		// GET: CityPhotosController
		public async Task<IActionResult> Index()
		{
			IEnumerable<CityPhotos> cityPhotos;
			cityPhotos = await unitOfWork.generic.GetAllAsync();

			var cityPhviewModelList = mapper.Map<IEnumerable<CityPhotos>, IEnumerable<CityPhotosViewModel>>(cityPhotos);

			if (cityPhviewModelList is not null)
				return View(cityPhviewModelList);
			else
				return BadRequest();

		}

		// GET: CityPhotosController/Details/5
		public async Task<IActionResult> Details(int? id, string viewname = "Details")
		{
			if (id is null)
				return BadRequest();

			var cityPh = await unitOfWork.generic.GetAsync(id.Value);

			if (cityPh is null)
				return NotFound();

			var cityPhmapped = mapper.Map<CityPhotos, CityPhotosViewModel>(cityPh);

			return View(viewname,cityPhmapped);
		}

		// GET: CityPhotosController/Create
		public async Task<IActionResult> Create()
		{
			return View();
		}

		// POST: CityPhotosController/Create
		[HttpPost]
		[ValidateAntiForgeryToken]
		public async Task<IActionResult> Create(CityPhotosViewModel cityPhotos)
		{
			//var AllCities = await _genericRepository.GetAllAsync();
			//cityPhotos.cities = AllCities;
			
			if (ModelState.IsValid)
			{
                var Citymapped = mapper.Map<CityPhotosViewModel, CityPhotos>(cityPhotos);

                try
                {
					unitOfWork.generic.Add(Citymapped);
					unitOfWork.Complet();
					return RedirectToAction(nameof(Index));
				}
				catch (Exception ex)
				{
					ModelState.AddModelError(string.Empty, ex.InnerException.Message);
				}


			}

			return View(cityPhotos);

		}

		// GET: CityPhotosController/Edit/5
		public async Task<IActionResult> Edit(int? id)
        {
            return await Details(id, "Edit");
        }

        // POST: CityPhotosController/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(int id,CityPhotosViewModel photosViewModel)
        {
            if (id != photosViewModel.Id)
                return BadRequest();

          
            if (ModelState.IsValid)  //server side validation
            {
                try
                {
                    var cityphmapped = mapper.Map<CityPhotosViewModel, CityPhotos>(photosViewModel);
                    unitOfWork.generic.Update(cityphmapped);
                    var count = unitOfWork.Complet();

                    return RedirectToAction(nameof(Index));
                }
                catch (Exception ex)
                {
                    ModelState.AddModelError(string.Empty, ex.Message);
                }


            }
            return View(photosViewModel);
        }

        // GET: CityPhotosController/Delete/5
        public async Task<IActionResult> Delete(int id)
        {
            return await Details(id, "Delete");
        }

        // POST: CityPhotosController/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Delete([FromRoute]int? id, CityPhotosViewModel cityPhotosViewModel)
        {
            if (id != cityPhotosViewModel.Id)
                return BadRequest();
			
                try
                {
                    var Citymapped = mapper.Map<CityPhotosViewModel, CityPhotos>(cityPhotosViewModel);

                    unitOfWork.generic.Delete(Citymapped);
                    unitOfWork.Complet();
                    return RedirectToAction(nameof(Index));
                }
                catch (Exception ex)
                {
                    ModelState.AddModelError(string.Empty, ex.InnerException.Message);
                }

                  return View(cityPhotosViewModel);
        }
    }
}
