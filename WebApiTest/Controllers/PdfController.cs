using Microsoft.AspNetCore.Mvc;
using WebApiTest.Services;

namespace WebApiTest.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class PdfController : ControllerBase
    {
        private readonly PdfService _pdfService;

        public PdfController(PdfService pdfService)
        {
            _pdfService = pdfService;
        }

        [HttpPost("create")]
        public IActionResult CreatePdf([FromBody] string content)
        {
            var pdfBytes = _pdfService.CreatePdf(content);
            return File(pdfBytes, "application/pdf", "document.pdf");
        }
    }
}
