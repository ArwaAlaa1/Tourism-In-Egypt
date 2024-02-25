using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Tourism.Core.Entities;
using Tourism.Core.Repositories.Contract;
using Tourism.Repository;
using TourismMVC.ViewModels;

namespace TourismMVC.Controllers
{
    public class NotificationController : Controller
    {
        IUnitOfWork<Notification> _unitOfWorkNo { get; set; }
        IUnitOfWork<User> _unitOfWorkUser { get; set; }
        private readonly IMapper mapper;
        public NotificationController(IUnitOfWork<Notification> unitOfWork, IUnitOfWork<User> unitOfWorkUser , IMapper mapper)
        {
            _unitOfWorkNo = unitOfWork;
            _unitOfWorkUser = unitOfWorkUser;
            this.mapper = mapper;
        }

        //Get : All Notification
        public async Task<IActionResult> Index()
        {

            var list = await _unitOfWorkNo.notification.GetAll();
            if (list != null)
            {
                return PartialView(list);
            }
            else
                return BadRequest();
        }

        //get : One Notification

        public async Task<IActionResult> Details(int id)
        {
            var notification = await _unitOfWorkNo.notification.GetNotificationwithUser(id);
            if (notification == null)
            {
                return RedirectToAction(nameof(Index));
            }
            else
                return View(notification);
        }

        //Get: Open Create form
        public async Task<IActionResult> Create()
        {
            var userlist = await _unitOfWorkUser.generic.GetAllAsync();
            NotificationModel model = new NotificationModel()
            {
                users = userlist
            };

            return View(model);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(NotificationModel model)
        {
                   var NotModel = mapper.Map<NotificationModel , Notification>(model);

                    if (ModelState.IsValid)
                    {
                        try
                        {
                            _unitOfWorkNo.generic.Add(NotModel);
                            _unitOfWorkNo.Complet();

                            return RedirectToAction(nameof(Index));
                        }
                        catch (Exception ex)
                        {
                            ModelState.AddModelError(string.Empty , ex.InnerException.Message);
                        }
                    }
            var userlist = await _unitOfWorkUser.generic.GetAllAsync();
            model.users = userlist;

            return View(model);
        }


        public async Task<IActionResult> Edit(int id)
        {
            var Choosen = await _unitOfWorkNo.generic.GetAsync(id);

            if (Choosen == null)
            {

                return RedirectToAction("Index");
            }
            else
            {
                var userlist = await _unitOfWorkUser.generic.GetAllAsync();
                NotificationModel model = new NotificationModel()
                {
                    users = userlist
                };
                return View(model);
            }
        }


        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id ,NotificationModel model)
        {
            var NotModel = mapper.Map<NotificationModel , Notification>(model);

            if (ModelState.IsValid)
            {
                try
                {
                    _unitOfWorkNo.generic.Update(NotModel);
                    _unitOfWorkNo.Complet();
                    return RedirectToAction(nameof(Index));
                }
                catch (Exception ex)
                {
                    ModelState.AddModelError(string.Empty, ex.InnerException.Message);
                }
            }
            var userlist = await _unitOfWorkUser.generic.GetAllAsync();
            NotificationModel notificationModel = new NotificationModel()
            {
                users = userlist
            };
            return View(notificationModel);
        }

        public async Task<IActionResult> Delete (int id)
        {
            var Deleted = await _unitOfWorkNo.notification.GetNotificationwithUser(id);
            if (Deleted == null)
            {
                return BadRequest();
            }
            return View(Deleted);
        }



        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Delete(Notification notification)
        {
            if (notification == null)
            {
                return BadRequest();
            }
            else
                try
                {
                    _unitOfWorkNo.generic.Delete(notification);
                    _unitOfWorkNo.Complet();
                    return RedirectToAction(nameof(Index));
                }catch
                {
                    return View(notification);
                }
        }
    }
}
