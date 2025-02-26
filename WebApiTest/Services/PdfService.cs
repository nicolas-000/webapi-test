using PdfSharp.Drawing;
using PdfSharp.Pdf;

namespace WebApiTest.Services
{
    public class PdfService
    {
        public byte[] CreatePdf(string content)
        {
            using (var memoryStream = new MemoryStream())
            {
                // Create a new PDF document
                PdfDocument document = new PdfDocument();
                document.Info.Title = "Created with PdfSharp";

                // Create an empty page
                PdfPage page = document.AddPage();

                // Get an XGraphics object for drawing
                XGraphics gfx = XGraphics.FromPdfPage(page);

                // Create a font
                XFont font = new XFont("Verdana", 20, XFontStyleEx.Bold);

                // Draw the text
                gfx.DrawString(content, font, XBrushes.Black, new XRect(0, 0, page.Width, page.Height),
                    XStringFormats.TopLeft);

                // Save the document to the memory stream
                document.Save(memoryStream, false);
                return memoryStream.ToArray();
            }
        }
    }
}
