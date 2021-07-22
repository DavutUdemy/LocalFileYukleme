using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Business.Abstract;
using Entities.Concrete;
using Microsoft.AspNetCore.Hosting;
using System.IO;
using System.Linq;
using System;

namespace WebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserImagesController : ControllerBase
    {
        public  IWebHostEnvironment _hostEnvironment;
        private IUserImageService _userImageService;

        public UserImagesController(IWebHostEnvironment hostEnvironment, IUserImageService userImageService)
        {
            _hostEnvironment = hostEnvironment;
            _userImageService = userImageService;
        }


     
        [HttpPost("add")]
        public IActionResult Add([FromForm(Name = ("Image"))] IFormFile file, [FromForm] UserImage images)
        {
            images.ImageName = SaveImage(file);
            images.ImagePath = String.Format("{0}://{1}{2}/Images/{3}", Request.Scheme, Request.Host, Request.PathBase,images.ImageName);
            var result = _userImageService.Add(file, images);
             if (result.Success)
            {
                return Ok(result);
            }


            return BadRequest(result);
        }
        [HttpPost("update")]
        public IActionResult Update([FromForm(Name = ("Image"))] IFormFile file, [FromForm] UserImage images)
        {
            var result = _userImageService.Update(file, images);
            if (result.Success)
            {
                return Ok(result);
            }

            return BadRequest(result);
        }
        [HttpPost("delete")]
        public IActionResult Delete(UserImage image)
        {
            var result = _userImageService.Delete(image);
            if (result.Success)
            {
                return Ok(result);
            }
            return BadRequest(result);
        }
        [HttpGet("getbyid")]
        public IActionResult GetById(int id)
        {
            var result = _userImageService.GetImagesByTId(id);
            if (result.Success)
            {
                return Ok(result.Data);
            }

            return BadRequest(result);
        }

        [HttpGet("getall")]
        public IActionResult GetAll()
        {
            var result = _userImageService.GetAll();
            if (result.Success)
            {
                return Ok(result);
            }

            return BadRequest(result);
        }

        [HttpGet("getimagesbyuserid")]
        public IActionResult GetImagesById([FromForm(Name = ("UserId"))] int userId)
        {
            var result = _userImageService.GetImagesByTId(userId);
            if (result.Success)
            {
                return Ok(result);
            }

            return BadRequest(result);
        }
        public string SaveImage(IFormFile formFile)
        {
            string imageName = new string(Path.GetFileNameWithoutExtension(formFile.FileName).Take(10).ToArray()).Replace(' ', '-');
            imageName = imageName + DateTime.Now.ToString("ymsmskjf") + Path.GetExtension(formFile.FileName);
            var imagePath = Path.Combine(_hostEnvironment.ContentRootPath, "Images", imageName);
            using (var filestream = new FileStream(imagePath, FileMode.Create))
            {
                formFile.CopyTo(filestream);
            };
            return imageName;


        }
    }
   

}
