using System;
using System.IO;
using System.Xml;
using iText.Kernel.Geom;
using iText.Kernel.Pdf;
using iText.Kernel.Pdf.Canvas;
using iText.Svg.Converter;

class Program
{
    static void Main(string[] args)
    {
        string svgFile = "C:\\Users\\TomaszFlorczyk\\GitClone\\SimpleConsoleApp\\SimpleApp\\SimpleApp\\input.svg";
        FileInfo pdfFile = new FileInfo("C:\\Users\\TomaszFlorczyk\\GitClone\\SimpleConsoleApp\\SimpleApp\\SimpleApp\\output.pdf"); // Replace with the desired PDF file path

        try
        {
            XmlDocument xmlDocument = new XmlDocument();
            xmlDocument.Load(svgFile);

            using (FileStream pdfStream = pdfFile.OpenWrite())
            {
                // Create a PDF document
                PdfDocument doc = new PdfDocument(new PdfWriter(pdfStream));

                doc.AddNewPage();

                var pageSize = PageSize.A4.Rotate();
                doc.SetDefaultPageSize(pageSize);
                doc.AddNewPage();

                var canvas = new PdfCanvas(doc.GetLastPage());

                // Convert and draw the SVG content onto the PDF canvas
                SvgConverter.DrawOnDocument(xmlDocument.OuterXml, doc, 1);

                // Close the PDF document
                doc.Close();

                Console.WriteLine($"PDF generation completed. PDF saved as '{pdfFile.FullName}'");
            }
        }
        catch (Exception ex)
        {
            Console.WriteLine($"An error occurred: {ex.Message}");
            Console.WriteLine(ex.StackTrace); // Print the stack trace for debugging
        }
    }
}
