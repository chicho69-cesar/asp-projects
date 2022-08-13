namespace _02FilesStorage.Models {
    public class FileServer {
        public int Id { get; set; }
        public string Name { get; set; }
        public IFormFile FileDB { get; set; }
        public string Extension { get; set; }
    }
}