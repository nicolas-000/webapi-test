using ClosedXML.Excel;
using Microsoft.AspNetCore.Mvc;
using WebApiTest.Models;

namespace WebApiTest.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class ExcelController : ControllerBase
    {
        [HttpPost("create")]
        public IActionResult CreateExcelFile([FromBody] ExcelData excelData, string filename)
        {
            if (excelData == null || excelData.Rows == null || !excelData.Rows.Any())
            {
                return BadRequest(new { Message = "Invalid data." });
            }

            var filePath = Path.Combine(Directory.GetCurrentDirectory(), "Files", filename + ".xlsx");

            // Crear un nuevo archivo xlsx
            using (var workbook = new XLWorkbook())
            {
                var worksheet = workbook.Worksheets.Add("Hoja1"); // Agregar una hoja

                // Agregar headers basado en la primera fila
                var firstRow = excelData.Rows.First();
                int columnCount = firstRow.Count;

                int colIndex = 1;
                foreach (var columnName in firstRow.Keys)
                {
                    worksheet.Cell(1, colIndex++).Value = columnName; // Ingresando los headers a la fila 1
                }

                // Agregar los demás datos al archivo
                for (int i = 0; i < excelData.Rows.Count; i++)
                {
                    var row = excelData.Rows[i];
                    colIndex = 1;

                    foreach (var value in row.Values)
                    {
                        worksheet.Cell(i + 2, colIndex++).Value = value; // Ingresando los datos a su casilla correspondiente
                    }
                }

                // Asegurandose que el directorio exista
                Directory.CreateDirectory(Path.GetDirectoryName(filePath));

                workbook.SaveAs(filePath); // Guardando archivo
            }

            return Ok(new { Message = "Excel file created successfully.", FilePath = filePath });
        }

        [HttpGet("read")]
        public IActionResult ReadExcelFile(string filename)
        {
            var filePath = Path.Combine(Directory.GetCurrentDirectory(), "Files", filename + ".xlsx");  // Obteniendo archivo

            if (!System.IO.File.Exists(filePath))
            {
                return NotFound(new { Message = "Excel file not found." });
            }

            // Leer el archivo
            using (var workbook = new XLWorkbook(filePath))
            {
                var worksheet = workbook.Worksheet(1); // Obtener la primera hoja
                var rows = new List<Dictionary<string, string>>();  // Creando filas donde se guardará la información a mostrar

                // Leyendo headers
                var headers = new List<string>();
                for (int col = 1; col <= worksheet.LastColumnUsed().ColumnNumber(); col++)
                {
                    headers.Add(worksheet.Cell(1, col).GetString());
                }

                // Leyendo los datos desde el archivo
                for (int row = 2; row <= worksheet.LastRowUsed().RowNumber(); row++)
                {
                    var rowData = new Dictionary<string, string>();

                    for (int col = 1; col <= headers.Count; col++)
                    {
                        var cellValue = worksheet.Cell(row, col).GetString();
                        rowData[headers[col - 1]] = cellValue;
                    }

                    rows.Add(rowData);  // Agregando los datos a las filas
                }

                return Ok(rows);
            }
        }
    }
}
