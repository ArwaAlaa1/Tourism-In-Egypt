using AutoMapper;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Tourism.Core.Entities;
using TourismMVC.ViewModels;

namespace TourismMVC.Controllers
{
    public class AccountController : Controller
    {
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly IMapper _mapper;
        private readonly SignInManager<ApplicationUser> _signInManager; 
        public AccountController(UserManager<ApplicationUser> userManager, IMapper mapper , SignInManager<ApplicationUser> signInManager)
        {
            _userManager = userManager;
            _mapper = mapper;
            _signInManager = signInManager;
        }

        public IActionResult Index()
        {
            var List = _userManager.Users.ToList();
            return View(List);
        }



        //Get : Open Register Form
        [HttpGet]
        public async Task<IActionResult> Register()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Register(RegisterUserModel registerUser)
        {
            if(ModelState.IsValid)
            {
                ApplicationUser RUM = new ApplicationUser()
                {
                    Id = registerUser.Id,
                    UserName = registerUser.UserName,
                    Email = registerUser.Email,
                    FName = registerUser.FName,
                    LName = registerUser.LName,
                    Location = registerUser.Location,
                    ProfilePhotoURL = registerUser.ProfilePhotoURL,
                    BirthDate = registerUser.BirthDate,
                    PhoneNumber = registerUser.PhoneNumber,
                    CreatedDate = DateTime.Now,
                    UpdatedDate = DateTime.Now,
                    DisplayName = registerUser.DisplayName,
                    PasswordHash = registerUser.Password
                };


               IdentityResult result = await _userManager.CreateAsync(RUM, registerUser.Password);

                if (result.Succeeded)
                {
                    await _signInManager.SignInAsync(RUM, false);
                    return RedirectToAction("Index");
                }
                else
                {
                    foreach (var ErrorItem in result.Errors)
                    {
                        ModelState.AddModelError("", ErrorItem.Description);
                    }
                }

            }
            return View(registerUser);
        }

        public IActionResult LogOut()
        {
            return View();
        }
        [HttpPost]
        public IActionResult SaveLogOut()
        {
            _signInManager.SignOutAsync();
            return RedirectToAction("Index", "Home");
        }
        [HttpGet]

        public IActionResult Login()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Login(LoginUserModel loginUserModel)
        {
            if (ModelState.IsValid)
            {
                
                    var username = await _userManager.FindByNameAsync(loginUserModel.UserName);

                if (username != null)
                {

                    bool Check = await _userManager.CheckPasswordAsync(username, loginUserModel.Password);

                    if (Check == true)
                    {
                        await _signInManager.SignInAsync(username, loginUserModel.RememberMe);
                        return RedirectToAction("Index");
                    }
                    else
                    {
                        ModelState.AddModelError("Password", "Invalid Password");
                    }
                }
                else
                    ModelState.AddModelError("UserName", "UserName Not Found");
                   
                
            }

            return View(loginUserModel);


        }



    } 
}
