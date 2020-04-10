using System;
using System.IO;
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

        [Route("{name}", Name = "GetImageByName")]
        public IActionResult Get(string name)
        {
            string imagePath = GetImagePath();
            string fileName = $"{imagePath}\\{name}";

            if (!System.IO.File.Exists(fileName))
                return NotFound();

            try
            {
                var image = System.IO.File.ReadAllBytes(fileName);

                string extension = new FileInfo(fileName).Extension.Substring(1);

                return File(image, $"image/{extension}");
            }
            catch (ArgumentException ex)
            {
                return BadRequest(ex.Message);
            }
            catch (PathTooLongException ex)
            {
                return StatusCode(414, ex.Message);
            }
            catch (DirectoryNotFoundException ex)
            {
                return NotFound(ex.Message);
            }
            catch (UnauthorizedAccessException)
            {
                return Unauthorized();
            }
            catch (FileNotFoundException ex)
            {
                return NotFound(ex.Message);
            }
            catch (NotSupportedException ex)
            {
                return StatusCode(415, ex.Message);
            }
            catch(Exception ex)
            {
                return StatusCode(500, ex.Message);
            }
        }

        [HttpPost]
        public async Task<IActionResult> Post()
        {
            var file = HttpContext.Request.Form.Files.Count == 1 ? HttpContext.Request.Form.Files[0] : null;

            if (file != null && file.Length > 0)
            {
                try
                {
                    string fileName = Path.GetFileName(file.FileName);
                    string imagePath = GetImagePath();

                    using (var outStream = System.IO.File.Create($"{imagePath}\\{fileName}"))
                        await file.CopyToAsync(outStream);

                    return CreatedAtRoute("GetImageByName", new { name = fileName }, null);
                }
                catch (ArgumentException ex)
                {
                    return BadRequest(ex.Message);
                }
                catch (UnauthorizedAccessException)
                {
                    return Unauthorized();
                }
                catch (PathTooLongException ex)
                {
                    return StatusCode(414, ex.Message);
                }
                catch (DirectoryNotFoundException ex)
                {
                    return NotFound(ex.Message);
                }
                catch (NotSupportedException ex)
                {
                    return StatusCode(415, ex.Message);
                }
                catch (Exception ex)
                {
                    return StatusCode(500, ex.Message);
                }
                
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

            try
            {
                System.IO.File.Delete($"{fileName}");

                return Ok();
            }
            catch (ArgumentException ex)
            {
                return BadRequest(ex.Message);
            }
            catch (PathTooLongException ex)
            {
                return StatusCode(414, ex.Message);
            }
            catch (DirectoryNotFoundException ex)
            {
                return NotFound(ex.Message);
            }
            catch (UnauthorizedAccessException)
            {
                return Unauthorized();
            }
            catch (NotSupportedException ex)
            {
                return StatusCode(415, ex.Message);
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message);
            }
        }

        private string GetImagePath()
        {
            var path = $"{host.WebRootPath}\\uploads";

            if (!Directory.Exists(path))
                Directory.CreateDirectory(path);

            return path;
        }
    }
}