using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Tourism.Core.Entities;
using Tourism.Core.Repositories.Contract;

namespace TourismMVC.Controllers
{
    public class CategoryController : Controller
    {
        private readonly IUnitOfWork<Category> _unitOfWork;

        public CategoryController(IUnitOfWork<Category> unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }
        // GET: CategoryController
        public async Task<IActionResult> Index()
        {
            IEnumerable<Category> categories;
            categories= await _unitOfWork.generic.GetAllAsync();

            if (categories is not null)
                 return View(categories);
            else 
                return BadRequest();
            
          
        }

        // GET: CategoryController/Details/5
        public async  Task<IActionResult> Details(int? id,string viewname="Details")
        {
            if (id is null)
                return BadRequest();

            var category = await _unitOfWork.generic.GetAsync(id.Value);

            if (category is null)
                return NotFound();
            
            return View(viewname ,category);
        }

        // GET: CategoryController/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: CategoryController/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(Category category)
        {

            if (ModelState.IsValid)  //server side validation
            { 
                
                _unitOfWork.generic.Add(category);
                var count = _unitOfWork.Complet();

                if (count > 0)
                    TempData["message"] = "Category Created succesfully";
                else
                    TempData["message"] = "Category Failed Created";

                return RedirectToAction(nameof(Index));

            }
            return View(category);
            
        }

        // GET: CategoryController/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {

            //if (id is null)
            //    return BadRequest();

            //var category = await _unitOfWork.generic.GetAsync(id.Value);

            //if (category is null)
            //    return NotFound();

            //return View(category);

            return await Details(id,"Edit");
        }

        // POST: CategoryController/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([FromRoute]int id, Category category)
        {
            if (id != category.Id)
                return BadRequest();

            if (ModelState.IsValid)  //server side validation
            {
                try
                {

                  _unitOfWork.generic.Update(category);
                var count = _unitOfWork.Complet();

                return RedirectToAction(nameof(Index));
                }
                catch (Exception ex)
                {
                    ModelState.AddModelError(string.Empty, ex.Message);
                }
             

            }
            return View(category);
        }

        // GET: CategoryController/Delete/5
        public async  Task<IActionResult> Delete(int? id)
        {
            return await Details(id, "Delete");
        }

        // POST: CategoryController/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Delete([FromRoute]int id, Category category)
        {
            if (id != category.Id)
                return BadRequest();

            if (ModelState.IsValid)  //server side validation
            {
                try
                {

                     _unitOfWork.generic.Delete(category);
                    var count = _unitOfWork.Complet();

                    return RedirectToAction(nameof(Index));
                }
                catch (Exception ex)
                {
                    ModelState.AddModelError(string.Empty, ex.Message);
                }


            }
            return View(category);
        }
    }
}
