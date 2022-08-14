namespace _04APIToUploadDocuments.Models {
    public class DocumentAPI {
        public int Id { get; set; }
        public string Description { get; set; }
        public string Route { get; set; }
        public IFormFile DocumentFile { get; set; }
    }
}