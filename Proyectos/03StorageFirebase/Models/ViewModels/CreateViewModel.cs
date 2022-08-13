namespace _03StorageFirebase.Models {
    public class CreateViewModel {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Phone { get; set; }
        public string URLImage { get; set; }
        public IFormFile Image { get; set; }
    }
}