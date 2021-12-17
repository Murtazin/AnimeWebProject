using System;
using System.Threading.Tasks;
using AnimeWebProject.Interfaces;
using AnimeWebProject.Models;
using AnimeWebProject.Models.ViewModels;
using AnimeWebProject.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace AnimeWebProject.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class RegistrationLoginController : ControllerBase
    {
        ApplicationContext dataBase;

        private readonly IJWTAuthManager _jWTAuthManager;

        public RegistrationLoginController(ApplicationContext context, IJWTAuthManager jWTAuthManager)
        {
            dataBase = context;
            _jWTAuthManager = jWTAuthManager;
        }

        [HttpPost("register")]
        public async Task<IActionResult> Registration([FromForm] RegistrationViewModel model)
        {
            if (ModelState.IsValid && (model.ConfirmPassword == model.Password))
            {
                User user = await dataBase.Users.FirstOrDefaultAsync(x => x.Email == model.Email);
                if (user == null)
                {
                    var currentUser = new User
                    {
                        Id = Guid.NewGuid(),
                        Email = model.Email,
                        Username = model.Email,
                        Password = Encryption.EncryptString(model.Password),
                    };
                    dataBase.Users.Add(currentUser);
                    await dataBase.SaveChangesAsync();
                }
                else
                    ModelState.AddModelError("", "User already exists");
            }

            return Ok();
        }

        [AllowAnonymous]
        [HttpPost("login")]
        public async Task<IActionResult> Login([FromForm] LoginViewModel model)
        {
            User user = await dataBase.Users
                .FirstOrDefaultAsync(x => x.Email == model.Email && x.Password == Encryption
                    .EncryptString(model.Password));
            if (user != null)
            {
                var token = _jWTAuthManager.Authenticate(user);
                if (token != null)
                {
                    Response.Cookies.Append("token", token);
                    return Ok();
                }
            }
            return BadRequest();
        }

        [Authorize]
        [HttpGet("logout")]
        public IActionResult Logout()
        {
            var token = Request.Cookies["token"];
            if (token != null)
                Response.Cookies.Delete("token");
            return Ok();
        }
    }
}