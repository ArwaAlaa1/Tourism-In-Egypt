using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Google;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore.Query.SqlExpressions;
using MimeKit.Text;
using MimeKit;
using System.Security.Claims;
using Tourism.Core.Entities;
using Tourism.Core.Helper;
using Tourism.Core.Helper.DTO;
using Tourism.Core.Repositories.Contract;
using static System.Net.Mime.MediaTypeNames;
using System.Net.Mail;
using MailKit.Security;

namespace Tourism_Egypt.Controllers
{
    public class AccountUserController : BaseApiController
    {
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly SignInManager<ApplicationUser> _signInManager;
        private readonly IAuthService _authService;

        private IEmailService _emailService;
        public AccountUserController(IEmailService emailService,UserManager<ApplicationUser> userManager , SignInManager<ApplicationUser> signInManager,IAuthService authService)

        {
            _userManager = userManager;
            _signInManager = signInManager;
            _authService = authService;
            _emailService = emailService;
        }


        [HttpPost("login")]
        public async Task<IActionResult> Login(LoginUserDTO loginUser)
        {

            var email = await _userManager.FindByEmailAsync(loginUser.Email);
            var username = await _userManager.FindByNameAsync(loginUser.Username);
            if (email == null && username == null)
            {
                return NotFound("Incorrect UserName Or Email ");
            }
            else
            {
                if (email != null || username != null)
                {
                    if (email != null)
                    {
                        var result = await _signInManager.CheckPasswordSignInAsync(email, loginUser.Password, false);

                        if (result.Succeeded is true)
                        {
                            return Ok(new UserDTO()
                            {
                                DisplayName = email.DisplayName,
                                Email = email.Email,
                                Username = email.UserName,
                                Token = await _authService.CreateTokenAsync(email, _userManager)
                            });
                        }
                    }

                    if (username != null)
                    {
                        var result = await _signInManager.CheckPasswordSignInAsync(username, loginUser.Password, false);
                        if (result.Succeeded is true)
                        {
                            return Ok(new UserDTO()
                            {
                                DisplayName = username.DisplayName,
                                Email = username.Email,
                                Username = username.UserName,
                                Token = await _authService.CreateTokenAsync(username, _userManager)
                            });
                        }

                    }

                    return BadRequest("Incorrect Password");
                }

            }
            return BadRequest();


        }

        [HttpPost("register")]
        public async Task<IActionResult> Register(RegisterUserDTO registerUser)
        {
            if (ModelState.IsValid)
            {
                if (CheckEmail(registerUser.Email).Result.Value)
                    return BadRequest("This email already exist");

                ApplicationUser user = new ApplicationUser();
                user.Email = registerUser.Email;
                user.FName = registerUser.Name.Split(" ")[0];
                user.Id = registerUser.Id;
                user.PhoneNumber = registerUser.Phone;
                user.UserName = registerUser.Username;
                user.DisplayName = registerUser.Name;
                try
                {
                    user.LName = registerUser.Name.Split(" ")[1];
                }
                catch
                {
                    return BadRequest("Enter Full Name");
                }
                var result = await _userManager.CreateAsync(user, registerUser.Password);

                if (result.Succeeded)
                {

                    await _signInManager.SignInAsync(user, isPersistent: false);
                    return Ok(new UserDTO
                    {
                        DisplayName = user.DisplayName,
                        Email = user.Email,
                        Username = user.UserName,
                        Token = await _authService.CreateTokenAsync(user, _userManager)
                    });
                }
                else
                {
                    foreach (var error in result.Errors)
                    {
                        return BadRequest(error.Description);
                    }
                }

            }


            return BadRequest();
        }

        [HttpGet]
        [Authorize]
        public async Task<IActionResult> GetUser()
        {
            var email = User.FindFirstValue(ClaimTypes.Email);
            var user = await _userManager.FindByEmailAsync(email);

            return Ok(new UserDTO
            {
                DisplayName = user.DisplayName,
                Email = user.Email,
                Username = user.UserName,
                Token = await _authService.CreateTokenAsync(user, _userManager)
            });
        }

        [HttpGet("EmailExists")]
        public async Task<ActionResult<bool>> CheckEmail(string email)
        {
            return await _userManager.FindByEmailAsync(email) != null;
        }

        //[HttpGet("google")]
        //public IActionResult GoogleLogin()
        //{
        //    var authenticationProperties = new AuthenticationProperties
        //    {
        //        RedirectUri = Url.Action("GoogleResponse")
        //    };

        //    return Challenge(authenticationProperties, GoogleDefaults.AuthenticationScheme);
        //}

        //[HttpGet("google-response")]
        //public async Task<IActionResult> GoogleResponse()
        //{
        //    var authenticateResult = await HttpContext.AuthenticateAsync(GoogleDefaults.AuthenticationScheme);

        //    if (!authenticateResult.Succeeded)
        //    {
        //        // Handle authentication failure
        //        return BadRequest();
        //    }

        //    // Here you can get user information from authenticateResult.Principal
        //    var userInfo = new
        //    {
        //        Email = authenticateResult.Principal.FindFirstValue(ClaimTypes.Email),
        //        Name = authenticateResult.Principal.FindFirstValue(ClaimTypes.Name)
        //        // Add more fields as needed
        //    };

        //    return Ok(userInfo);
        //}

        //[HttpGet("ForgetPass")]
        //public async Task<IActionResult> ForgetPassWord(string email)
        //{
        //    var user = await _userManager.FindByEmailAsync(email);
        //    if (user is not null)
        //    {
        //        var token =await _userManager.GeneratePasswordResetTokenAsync(user);
        //        var RestPasswordUrl = Url.Action("RestPssword", "AccountUser", new { email = email, token=token}, "https", "lacalhost:44317");
        //        var emaill = new Email()
        //        {
        //            Subject="Reset Your PassWord",
        //            Recipients= email,
        //            Body=RestPasswordUrl
        //        };
        //        EmailSetting.SendEmail(emaill);
        //        // return RedirectToAction("CheckYourInbox");
        //        return Ok(new 
        //        {
        //           email=email,
        //        }) ;
        //    }
        //    ModelState.AddModelError(string.Empty, "Invalid Email");
        //    return BadRequest();
        //}


        //public IActionResult CheckYourInbox()
        //{

        //}
     
        [HttpPost("SendEmail")]
        public void SendEmail(SendEmailDto emailDto)
        {
            _emailService.SendEmail(emailDto);
        }
    }
}
