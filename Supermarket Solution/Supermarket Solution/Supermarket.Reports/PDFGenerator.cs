using System;
using System.Linq;
using System.IO;
using iTextSharp.text;
using iTextSharp.text.pdf;
using Supermarket.Data;
using System.Globalization;

namespace Supermarket.Reports
{
    public class PDFReportGenerator
    {
        // to generate the report call the GeneratePDFReport static method.
        // The pdf file will be generated in the SupermarketChain.ConsoleClient folder
        
        public static void GeneratePDFReport()
        {
            Document doc = new Document(iTextSharp.text.PageSize.A4, 10, 10, 40, 35);
            string filePath = @"..\..\..\..\Reports\salesReports.pdf";
            PdfWriter writer = PdfWriter.GetInstance(doc, new FileStream(filePath, FileMode.Create));

            doc.Open();

            PdfPTable table = new PdfPTable(5);
            Font verdana = FontFactory.GetFont("Verdana", 16, Font.BOLD);
            Font verdana2 = FontFactory.GetFont("Verdana", 12, Font.BOLD);
            PdfPCell header = new PdfPCell(new Phrase("Aggregated Sales Report", verdana));

            header.Colspan = 5;
            header.HorizontalAlignment = 1;
            table.AddCell(header);
           
            double totalSales = PourReportData(table);
            PdfPCell totalSum = new PdfPCell(new Phrase("Grand total:"));

            totalSum.Colspan = 4;
            totalSum.HorizontalAlignment = 2;
            totalSum.BackgroundColor = new BaseColor(161, 212, 224);
            table.AddCell(totalSum);

            PdfPCell totalSumNumber = new PdfPCell(new Phrase(String.Format("{0:0.00}", totalSales), verdana));
            totalSumNumber.BackgroundColor = new BaseColor(161, 212, 224);
            table.AddCell(totalSumNumber);

            doc.Add(table);
            doc.Close();
            
            DirectoryInfo directoryInfo = new DirectoryInfo(filePath);

            Console.WriteLine("Pdf report was generated.");
            Console.WriteLine("File:  {0}", directoryInfo.FullName);
        }

        private static void FooterGeneration(PdfPTable table, double sum, DateTime d)
        {
            string date = d.Date.ToString("dd-MMM-yyyy", CultureInfo.InvariantCulture);
            PdfPCell footerDate = new PdfPCell(new Phrase("Total sum for " + date));

            footerDate.Colspan = 4;
            footerDate.HorizontalAlignment = 2;
            table.AddCell(footerDate);

            Font verdana2 = FontFactory.GetFont("Verdana", 12, Font.BOLD);
            PdfPCell fullSum = new PdfPCell(new Phrase(sum.ToString("N2"), verdana2));
            table.AddCell(fullSum);
        }

        private static void HeaderGeneration(PdfPTable table, DateTime d)
        {
            string date = d.Date.ToString("dd-MMM-yyyy", CultureInfo.InvariantCulture);
            Font verdana2 = FontFactory.GetFont("Verdana", 12, Font.BOLD);
            d = d.Date;

            PdfPCell dateCell = new PdfPCell(new Phrase("Date: " + date));
            dateCell.Colspan = 5;
            dateCell.BackgroundColor = new BaseColor(242, 242, 242);
            table.AddCell(dateCell);

            BaseColor color = new BaseColor(217, 217, 217);
            PdfPCell product = new PdfPCell(new Phrase("Product", verdana2));
            product.BackgroundColor = color;
            table.AddCell(product);

            PdfPCell quantity = new PdfPCell(new Phrase("Quantity", verdana2));
            quantity.BackgroundColor = color;
            table.AddCell(quantity);

            PdfPCell unitPrice = new PdfPCell(new Phrase("Unit Price", verdana2));
            unitPrice.BackgroundColor = color;
            table.AddCell(unitPrice);

            PdfPCell location = new PdfPCell(new Phrase("Location", verdana2));
            location.BackgroundColor = color;
            table.AddCell(location);

            PdfPCell sum = new PdfPCell(new Phrase("Sum", verdana2));
            sum.BackgroundColor = color;
            table.AddCell(sum);
        }

        private static double PourReportData(PdfPTable table)
        {
            double totalSum = 0;
            double grandTotal = 0;
            var msSQLcontext = new SupermarketContext();

            msSQLcontext.Vendors.FirstOrDefault();

            var salesReport = msSQLcontext.SupermarketSalesProducts
                .Select(p => new
                {
                    date = p.SalesDate,
                    location = p.Supermarket.Name,
                    product = p.Product.ProductName,
                    quantity = p.Quantity,
                    price = p.Price,
                    measure = p.Product.Measure.MeasureName
                })
                .GroupBy(d => d.date);          

            foreach (var item in salesReport)
            {
                HeaderGeneration(table, item.Key);
                double sum = 0;

                foreach (var item2 in item)
                {
                    sum = (double) (item2.quantity * item2.price);
                    table.AddCell(item2.product);
                    table.AddCell(item2.quantity.ToString() + " " + item2.measure);                   
                    table.AddCell(String.Format("{0:0.00}", item2.price));
                    table.AddCell(item2.location);
                    table.AddCell(String.Format("{0:0.00}", sum));

                    totalSum += sum;
                }
              
                FooterGeneration(table, totalSum, item.Key);
                grandTotal += totalSum;
                totalSum = 0;
            }

            return grandTotal;
        }
    }
}
