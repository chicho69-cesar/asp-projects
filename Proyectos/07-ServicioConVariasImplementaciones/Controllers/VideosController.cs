using _07_ServicioConVariasImplementaciones.Services.Interfaces;
using _07_ServicioConVariasImplementaciones.Services.Utils;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace _07_ServicioConVariasImplementaciones.Controllers {
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class VideosController : ControllerBase {
        private readonly IMediaService _mediaService;

        public VideosController(IEnumerable<IMediaService> mediaServices) {
            _mediaService = mediaServices.First(m => m.Type == TypeMultimediaService.Videos);
        }

        [HttpGet]
        public async Task<IActionResult> GetVideos() {
            return Ok(await _mediaService.GetMediaAsync());
        }
    }
}