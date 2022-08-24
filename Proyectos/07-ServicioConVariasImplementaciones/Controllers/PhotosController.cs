using _07_ServicioConVariasImplementaciones.Services.Interfaces;
using _07_ServicioConVariasImplementaciones.Services.Utils;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace _07_ServicioConVariasImplementaciones.Controllers {
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class PhotosController : ControllerBase {
        private readonly IMediaService _mediaService;

        public PhotosController(IEnumerable<IMediaService> mediaServices) {
            _mediaService = mediaServices.First(m => m.Type == TypeMultimediaService.Photos);
        }

        [HttpGet]
        public async Task<IActionResult> GetPhotos() {
            return Ok(await _mediaService.GetMediaAsync());
        }
    }
}