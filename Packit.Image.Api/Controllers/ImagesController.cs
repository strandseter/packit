using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;

namespace Packit.Image.Api.Controllers
{
    [Route("api/[controller]")]
    public class ImagesController : ControllerBase
    {
        private readonly IHostingEnvironment host;

        public ImagesController(IHostingEnvironment host)
        {
            this.host = host;
        }

        //Code from Øyvinds example.
        [Route("{name}", Name = "GetImageByName")]
        public ActionResult Get(string name)
        {
            string imagePath = GetImagePath();
            string fileName = $"{imagePath}\\{name}";

            if (!System.IO.File.Exists(fileName))
                return NotFound();

            var image = System.IO.File.ReadAllBytes(fileName);

            string extension = new System.IO.FileInfo(fileName).Extension.Substring(1);
            return File(image, $"image/{extension}");
        }

        //Code from Øyvinds example.
        [HttpPost]
        public async Task<IActionResult> Post()
        {
            var file = HttpContext.Request.Form.Files.Count == 1 ? HttpContext.Request.Form.Files[0] : null;

            if (file != null && file.Length > 0)
            {
                string fileName = System.IO.Path.GetFileName(file.FileName);
                string imagePath = GetImagePath();

                using (var outStream = System.IO.File.Create($"{imagePath}\\{fileName}"))
                    await file.CopyToAsync(outStream);

                return CreatedAtRoute("GetImageByName", new { name = fileName }, null);
            }
            else
                return BadRequest();
        }

        [Route("{name}", Name = "GetImageByName")]
        [HttpDelete]
        public IActionResult Delete(string name)
        {
            string imagePath = GetImagePath();
            string fileName = $"{imagePath}\\{name}";

            if (!System.IO.File.Exists(fileName))
                return NotFound();

            System.IO.File.Delete($"{fileName}");

            return Ok();
        }

        private string GetImagePath()
        {
            var path = $"{host.WebRootPath}\\uploads";

            if (!System.IO.Directory.Exists(path))
                System.IO.Directory.CreateDirectory(path);

            return path;
        }
    }
}