using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using AnimeWebProject.Interfaces;
using AnimeWebProject.Models;
using AnimeWebProject.Models.ViewModels;
using AnimeWebProject.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace AnimeWebProject.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class HomeController : ControllerBase
    
    {
        private ApplicationContext dataBase;
        private JwtSecurityToken _token;
        public User CurrentUser { get => GetUser(); }
        
        public HomeController(ApplicationContext context)
        {
            dataBase = context;
        }
        
        [HttpGet("currentUser")]
        public User GetUser()
        {
            if (Request.Cookies["token"] == null || Request.Cookies["token"] == "")
            {
                return null;
            }
            var stream = Request.Cookies["token"];
            var handler = new JwtSecurityTokenHandler();
            var jsonToken = handler.ReadToken(stream);
            _token = jsonToken as JwtSecurityToken;
            var CurrentId = _token.Claims.First(claim => claim.Type == "nameid").Value;
            var user = dataBase.Users.FirstOrDefault(u => u.Id.ToString() == CurrentId);
            return user;
        }
        
        [HttpPost("settings")]
        public IActionResult ObtainSettings([FromForm] SettingsViewModel model)
        {
            if (model.Avatar != null)
            {
                byte[] imageData = null;
                using (var binaryReader = new BinaryReader(model.Avatar.OpenReadStream()))
                {
                    imageData = binaryReader.ReadBytes((int)model.Avatar.Length);
                }
                CurrentUser.Avatar = imageData;
            }
            if (model.Username != null)
            {
                CurrentUser.Username = model.Username;
            }
            if (model.CurrentPassword != null && model.NewPassword != null)
            {
                if (CurrentUser.Password == Encryption.EncryptString(model.CurrentPassword))
                {
                    CurrentUser.Password = Encryption.EncryptString(model.NewPassword);
                }
            }
            dataBase.SaveChanges();
            return Ok();
        }
        
        [HttpPost("AddNews")]
        public async Task<IActionResult> ObtainNewContent([FromForm] ContentViewModel model)
        {
            if (ModelState.IsValid)
            {
                Content content = await dataBase.Content.FirstOrDefaultAsync(x => x.Title == model.Title);
                byte[] img = null;
                if (model.ContentImage != null)
                {
                    byte[] imageData = null;
                    using (var binaryReader = new BinaryReader(model.ContentImage.OpenReadStream()))
                    {
                        imageData = binaryReader.ReadBytes((int)model.ContentImage.Length);
                    }
                    img = imageData;
                }

                if (content == null)
                {
                    var newContent = new Content
                    {
                        Id = Guid.NewGuid(),
                        Title = model.Title,
                        Genre = model.Genre,
                        Description = model.Description,
                        ContentImage = img,
                        Date = DateTime.Now.Second,
                    };
                    dataBase.Content.Add(newContent);
                    await dataBase.SaveChangesAsync();
                }
                else
                    return BadRequest();
            }
            return Ok();
        }
        
        [HttpGet("GetContent")]
        public List<Content> GetContent()
        {
            List<Content> con =  dataBase.Content.Select(x => x).ToList<Content>();
            return con;
        }
    }
}