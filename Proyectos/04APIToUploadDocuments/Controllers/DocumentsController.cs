using _04APIToUploadDocuments.Models;
using _04APIToUploadDocuments.Services.Interfaces;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace _04APIToUploadDocuments.Controllers {
    [Route("api/[controller]")]
    [ApiController]
    public class DocumentsController : ControllerBase {
        private readonly IDocumentService _documentService;

        public DocumentsController(IDocumentService documentService) {
            _documentService = documentService;
        }

        [HttpPost]
        [Route("upload")]
        [DisableRequestSizeLimit, RequestFormLimits(MultipartBodyLengthLimit = int.MaxValue, ValueLengthLimit = int.MaxValue)]
        public async Task<IActionResult> Upload([FromForm] DocumentAPI document) {
            var band = await _documentService.Upload(document);
            return band
                ? StatusCode(StatusCodes.Status200OK, new { message = "Document was saved with exit !!!" })
                : StatusCode(StatusCodes.Status400BadRequest, new { message = "An error was ocurred" });
        }
    }
}