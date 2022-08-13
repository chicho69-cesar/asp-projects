namespace _02FilesStorage.Models.ViewModels {
    public class IndexViewModel {
        public int Id { get; set; }
        public string Name { get; set; }
        public byte[] FileDB { get; set; }
        public string Extension { get; set; }
    }
}