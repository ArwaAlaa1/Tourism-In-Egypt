using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Google;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;
using Tourism.Core.Entities;
using Tourism.Core.Helper;
using Tourism.Core.Helper.DTO;
using Tourism.Core.Repositories.Contract;
using System.ComponentModel.DataAnnotations;
using System.Text;
using Microsoft.AspNetCore.WebUtilities;
using Tourism.Repository.Data;

namespace Tourism_Egypt.Controllers
{
    public class AccountUserController : BaseApiController
    {
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly SignInManager<ApplicationUser> _signInManager;
        private readonly IAuthService _authService;
        private readonly IUnitOfWork<ResetPassword> _resetpassword;
        private readonly IConfiguration _configuration;
        private readonly TourismContext _context;
        private IEmailService _emailService;
        public AccountUserController(IEmailService emailService,UserManager<ApplicationUser> userManager 
            , SignInManager<ApplicationUser> signInManager
            ,IAuthService authService,IUnitOfWork<ResetPassword> resetpassword
            ,IConfiguration configuration)

        {
            _userManager = userManager;
            _signInManager = signInManager;
            _authService = authService;
            _resetpassword = resetpassword;
            _configuration = configuration;
           
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

        [HttpGet("ForgetPassword")]
        public async Task<IActionResult> ForgetPassword([Required] string email)
        {
            if (email == null) return BadRequest("Please enter Your email");
            var user = await _userManager.FindByEmailAsync(email);

            if (user == null) return BadRequest("Invalid Email");
            var token = await _userManager.GeneratePasswordResetTokenAsync(user);
            var encodedtoken = Encoding.UTF8.GetBytes(token);
            var validtoken = WebEncoders.Base64UrlEncode(encodedtoken);

            string url = $"{_configuration["ApiBaseUrl"]}/ResetPassword?email={email}&token={validtoken}";
            var OTP = RandomGenerator.Generate(1000, 9999);

            var Reset = new ResetPassword()
            {
                Email = email,
                OTP = OTP,
                Token = validtoken,
                UserId = user.Id,
                Date = DateTime.UtcNow
            };
             _resetpassword.generic.Add(Reset);
            _resetpassword.Complet();

            var SendEmail = new SendEmailDto()
            {
                Subject = "Here's Your Password Reset Link",
                To = email,
                
                Html = $"<h1>Hello {user.DisplayName}<h1>" +
                    $"<p>Looks Like you've forgotten your password .Don't worry ,we've got you!</p>" +
                    $"Code Verification :{OTP}",
               
            };
             _emailService.SendEmail(SendEmail);
            return Ok(Reset);
           }

        [HttpGet("CheckCode")]
        public async Task<IActionResult> CheckCode(int otp , string email)
        {
           var User = await _resetpassword.changePassword.GetPasswordofOTP(otp, email);
            if (User == null) return BadRequest("Code is invalid");
            return Ok(User);
        }


        //[HttpPost("ResetPassword")]
        //public async Task<IActionResult> ResetPassword()
        //{
            

        //}


        [HttpPost("SendEmail")]
        public void SendEmail(SendEmailDto emailDto)
        {
            _emailService.SendEmail(emailDto);
        }
    }
}
