using _07_ServicioConVariasImplementaciones.Services.Utils;

namespace _07_ServicioConVariasImplementaciones.Services.Interfaces {
    public interface IMediaService {
        Task<IEnumerable<string>> GetMediaAsync();
        public TypeMultimediaService Type { get; }
    }
}