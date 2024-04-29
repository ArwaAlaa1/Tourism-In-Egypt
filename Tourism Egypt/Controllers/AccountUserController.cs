using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
//using static Org.BouncyCastle.Crypto.Engines.SM2Engine;

using System.ComponentModel.DataAnnotations;
using System.Security.Claims;
using Tourism.Core.Entities;
using Tourism.Core.Helper;
using Tourism.Core.Helper.DTO;
using Tourism.Core.Repositories.Contract;

namespace Tourism_Egypt.Controllers
{
    public class AccountUserController : BaseApiController
    {
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly SignInManager<ApplicationUser> _signInManager;
        private readonly IAuthService _authService;
        private readonly IUnitOfWork<ResetPassword> _resetpassword;
        private readonly IConfiguration _configuration;

        private IEmailService _emailService;

        private static ApplicationUser? user;
        private static string? token;
        public AccountUserController(IEmailService emailService, UserManager<ApplicationUser> userManager
            , SignInManager<ApplicationUser> signInManager
            , IAuthService authService, IUnitOfWork<ResetPassword> resetpassword
            , IConfiguration configuration)

        {
            _userManager = userManager;
            _signInManager = signInManager;
            _authService = authService;
            _resetpassword = resetpassword;
            _configuration = configuration;

            _emailService = emailService;
        }


        [HttpPost("login")]
        public async Task<ActionResult<UserDTO>> Login(LoginDTO loginDto)
        {
            var user = await _userManager.FindByEmailAsync(loginDto.Email);
            if (user == null) return Unauthorized("This User Not Register");
            var result = await _signInManager.CheckPasswordSignInAsync(user, loginDto.Password, false);
            if (!result.Succeeded) return BadRequest("Enter Correct PassWord");


            return Ok(new UserDTO()
            {
                DisplayName = user.DisplayName,
                Email = user.Email,
                Username = user.UserName,
                Token = await _authService.CreateTokenAsync(user, _userManager)
            });
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

        [HttpGet("YourDetails")]
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



        [HttpGet("ForgetPassword")]
        public async Task<IActionResult> ForgetPassword([Required] string email)
        {
            try
            {
                if (email == null)
                    return BadRequest("Please enter Your email");

                user = await _userManager.FindByEmailAsync(email);

                if (user == null)
                    return BadRequest("Invalid Email");

                token = await _userManager.GeneratePasswordResetTokenAsync(user);
                //var encodedtoken = Encoding.UTF8.GetBytes(token);
                // var validtoken = WebEncoders.Base64UrlEncode(encodedtoken);

                //var RestPasswordUrl = Url.Action("ResetPassword", "AccountUser", new { email = user.Email, token = token }, Request.Scheme);
                // string url = $"{_configuration["ApiBaseUrl"]}/ResetPassword?email={email}&token={token}";
                var OTP = RandomGenerator.Generate(1000, 9999);

                var Reset = new ResetPassword()
                {
                    Email = email,
                    OTP = OTP,
                    Token = token,
                    UserId = user.Id,
                    Date = DateTime.UtcNow
                };
                _resetpassword.generic.Add(Reset);
                _resetpassword.Complet();

                var SendEmail = new SendEmailDto()
                {
                    Subject = "Here's Your Password Reset Link",
                    To = email,
                    Code = OTP.ToString(),
                    //ResetUrl= url,
                    Html = $"<h1>Hello {user.DisplayName}<h1>" +
                        $"<p>Looks Like you've forgotten your password .Don't worry ,we've got you!</p>" +
                        $"Code Verification :{OTP}"
                        ,

                };

                _emailService.SendEmail(SendEmail);
                return Ok();
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpGet("VerifyCode")]

        public async Task<IActionResult> VerifyCode(int otp)
        {
            var User = await _resetpassword.changePassword.GetPasswordofOTP(otp, user.Email);
            if (User == null) return BadRequest("Invalid Code");

            return Ok("Verification Done");
        }

        [HttpGet("ResendCode")]
        public async Task<IActionResult> ResendCode()
        {
            try
            {
                var user2 = await _userManager.FindByEmailAsync(user.Email);

                if (user2 == null)
                    return BadRequest("Invalid Email");

                token = await _userManager.GeneratePasswordResetTokenAsync(user2);
                var OTP = RandomGenerator.Generate(1000, 9999);

                var Reset = new ResetPassword()
                {
                    Email = user.Email,
                    OTP = OTP,
                    Token = token,
                    UserId = user.Id,
                    Date = DateTime.UtcNow
                };
                _resetpassword.generic.Add(Reset);
                _resetpassword.Complet();

                var SendEmail = new SendEmailDto()
                {
                    Subject = "Here's Your Password Reset Link",
                    To = user2.Email,
                    Code = OTP.ToString(),
                    //ResetUrl= url,
                    Html = $"Hello {user.DisplayName}" +
                        $"<p>Looks Like you've forgotten your password .Don't worry ,we've got you!</p>" +
                        $"Code Verification :{OTP}"
                        ,

                };

                _emailService.SendEmail(SendEmail);
                return Ok("Check Your Inbox");
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpPost("ResetPassword")]
        [Authorize]
        public async Task<IActionResult> ResetPassword(PasswordResetDto passwordDto)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    var user1 = await _userManager.FindByEmailAsync(user.Email);
                    var result = await _userManager.ResetPasswordAsync(user1, token, passwordDto.NewPassword);
                    if (result.Succeeded)
                        return Ok("PassWord Updated");

                    foreach (var error in result.Errors)
                    {
                        return BadRequest(error.Description);
                    }
                }
                catch (Exception ex)
                {
                    return BadRequest(ex.InnerException.Message);
                }
            }
            return BadRequest();
        }

        [HttpPost("ChangePassWord")]
        [Authorize]
        public async Task<IActionResult> ChangePassWord(ChangPasswordDto changPassword, string email)
        {
            try
            {

                if (changPassword.OldPassword == changPassword.NewPassword)
                    return BadRequest("We're sorry, but the new password you entered is the same as your current password.");
                else
                {
                    var user1 = await _userManager.FindByEmailAsync(email);
                    var result = await _userManager.ChangePasswordAsync(user1, changPassword.OldPassword, changPassword.NewPassword);
                    if (result.Succeeded)
                        return Ok("PassWord Updated");

                    foreach (var error in result.Errors)
                    {
                        return BadRequest(error.Description);
                    }
                }

            }
            catch (Exception ex)
            {
                return BadRequest(ex.InnerException.Message);
            }
            return BadRequest();
        }

        [HttpPost("LogOut")]
        public async Task<IActionResult> LogOut()
        {
           await _signInManager.SignOutAsync();
            return Ok("Loged out successfully");
        }
        
    }
}
