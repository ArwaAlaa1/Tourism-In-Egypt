using AutoMapper;
using Microsoft.AspNetCore.Identity;
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
        UserManager<ApplicationUser> _unitOfWorkUser { get; set; }
        private readonly IMapper mapper;
        public NotificationController(IUnitOfWork<Notification> unitOfWork, IMapper mapper, UserManager<ApplicationUser> unitOfWorkUser )
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
                return View(list);
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
        public IActionResult Create()
        {
            var userlist = _unitOfWorkUser.Users;
            NotificationModel model = new NotificationModel()
            {
                users = userlist
            };

            return View(model);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create(NotificationModel model)
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
            var userlist =  _unitOfWorkUser.Users;
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
                var userlist =  _unitOfWorkUser.Users;
                
                var nomapper = mapper.Map<Notification, NotificationModel>(Choosen);
                nomapper.users = userlist;
                return View(nomapper);
            }
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id ,NotificationModel model)
        {

            if (ModelState.IsValid)
            {
                try
                {
                    var NotModel = mapper.Map<NotificationModel, Notification>(model);

                    _unitOfWorkNo.generic.Update(NotModel);
                  var count =  _unitOfWorkNo.Complet();
                    return RedirectToAction(nameof(Index));
                }
                catch (Exception ex)
                {
                    ModelState.AddModelError(string.Empty, ex.InnerException.Message);
                }
            }
            var userlist =  _unitOfWorkUser.Users;
            model.users = userlist;
            return View(model);
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
