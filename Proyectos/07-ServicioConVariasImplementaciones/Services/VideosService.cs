using _07_ServicioConVariasImplementaciones.Services.Interfaces;
using _07_ServicioConVariasImplementaciones.Services.Utils;

namespace _07_ServicioConVariasImplementaciones.Services {
    public class VideosService : IMediaService {
        public TypeMultimediaService Type => TypeMultimediaService.Videos;

        public async Task<IEnumerable<string>> GetMediaAsync() {
            return await Task.FromResult(new List<string> {
                "https://youtube.com",
                "https://reddit.com",
                "https://twitch.com"
            });
        }
    }
}