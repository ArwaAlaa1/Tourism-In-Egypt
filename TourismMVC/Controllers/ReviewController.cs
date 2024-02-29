using Microsoft.AspNetCore.Mvc;
using Tourism.Core.Entities;
using Tourism.Core.Repositories.Contract;

namespace TourismMVC.Controllers
{
    public class ReviewController : Controller
    {
        IUnitOfWork<Review> _unitOfWork;
        public ReviewController(IUnitOfWork<Review> _unitOfWork)
        {
            this._unitOfWork = _unitOfWork;
        }

        //Get all reviews
        public async Task<IActionResult> Index()
        {
            var rw = await _unitOfWork.review.GetAll();
            return View(rw);
        }

        //Get one review
        public async Task<IActionResult> Details(int id)
        {
            var review = await _unitOfWork.review.GetIdIncludeUser(id);
            if (review == null)
            {
                return RedirectToAction("Index");
            }
            return View(review);
        }


        //Get : open form of Delete
        public async Task<IActionResult> Delete(int id)
        {
            var review = await _unitOfWork.review.GetIdIncludeUser(id);

            if (review == null)
            {
                return RedirectToAction("Index");
            }
            else
                return View(review);
        }

        //post : Delete
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Delete(int id, Review review)
        {
            try
            {
                _unitOfWork.generic.Delete(review);
                _unitOfWork.Complet();
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View(review);
            }
        }

        //     //Get: Delete All Reviews

        //     public async Task<IActionResult> DeleteAll()
        //     {
        //         try
        //         {
        //            var list = await _unitOfWork.generic.GetAllAsync();

        //              _unitOfWork.review.DeleteAll(list);
        //             _unitOfWork.Complet();
        //             return RedirectToAction(nameof(Index));

        //         }catch(Exception ex)
        //         {

        //             return RedirectToAction(nameof(Index));
        //}
        //     }
    }
}
