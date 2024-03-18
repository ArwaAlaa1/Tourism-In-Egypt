using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;
using Tourism.Core.Entities;
using Tourism.Core.Helper.DTO;
using Tourism.Core.Repositories.Contract;

namespace Tourism_Egypt.Controllers
{
    public class AccountUserController : BaseApiController
    {
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly SignInManager<ApplicationUser> _signInManager;
        private readonly IAuthService _authService;



        public AccountUserController(UserManager<ApplicationUser> userManager , SignInManager<ApplicationUser> signInManager,IAuthService authService)

        {
            _userManager = userManager;
            _signInManager = signInManager;
            _authService = authService;
        }


        [HttpPost("login")]
        public async Task<IActionResult> Login(LoginUserDTO loginUser)
        {
            
                var email = await _userManager.FindByEmailAsync(loginUser.Email);
                var username = await _userManager.FindByNameAsync(loginUser.Username);
                if (email == null && username == null)
                {
                    return NotFound();
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
                                    Token = await _authService.CreateTokenAsync(email, _userManager)
                                });
                            }
                        return BadRequest();
                    }

                        if (username != null)
                        {
                            var result = await _signInManager.CheckPasswordSignInAsync(username, loginUser.Password, false);
                            if (result.Succeeded is true)
                            {
                                return Ok(new UserDTO()
                                {
                                    DisplayName = username.DisplayName,
                                    Username = username.UserName,
                                    Token = await _authService.CreateTokenAsync(username, _userManager)
                                });
                            }
                        return BadRequest();
                    }

                    
                }
                   
                }
            return BadRequest();


        }

        [HttpPost("register")]
        public async Task<IActionResult> Register(RegisterUserDTO registerUser)
        { 
            if (ModelState.IsValid)
            {
                if(CheckEmail(registerUser.Email).Result.Value)
                    return BadRequest("This email already exist");

                var username = _userManager.FindByNameAsync(registerUser.Username);

                ApplicationUser user = new ApplicationUser();
                    user.Email= registerUser.Email;
                user.FName = registerUser.Name.Split(" ")[0];
                user.Id = registerUser.Id;
                user.PhoneNumber = registerUser.Phone;
                user.UserName = registerUser.Username;
                user.DisplayName = registerUser.Name;
                try
                {
                    user.LName= registerUser.Name.Split(" ")[1];
                }catch
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
                    return BadRequest("Password must have 1 non alphanumeric and at least 6 characters and 1 number  or username is already tooken");
 
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
    }
}
