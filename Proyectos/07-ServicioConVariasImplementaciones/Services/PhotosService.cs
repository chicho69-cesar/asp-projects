using _07_ServicioConVariasImplementaciones.Services.Interfaces;
using _07_ServicioConVariasImplementaciones.Services.Utils;

namespace _07_ServicioConVariasImplementaciones.Services {
    public class PhotosService : IMediaService {
        public TypeMultimediaService Type => TypeMultimediaService.Photos;

        public async Task<IEnumerable<string>> GetMediaAsync() {
            return await Task.FromResult(new List<string> {
                "https://facebook.com",
                "https://instagram.com",
                "https://twitter.com",
                "https://pinterest.com"
            });
        }
    }
}