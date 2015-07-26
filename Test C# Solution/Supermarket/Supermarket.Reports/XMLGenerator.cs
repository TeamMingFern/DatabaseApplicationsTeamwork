using Supermarket.Data;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml;

namespace Supermarket.Reports
{
    public class XMLGenerator
    {
        public static void generateXMLReport()
        {
            var msSQLcontext = new SupermarketContext();
            msSQLcontext.Vendors.FirstOrDefault();

            var data = msSQLcontext.SupermarketSalesProducts
                .Select(s => new
                {
                    vendor = s.Product.Vendor.VendorName,
                    date = s.SalesDate,
                    quontity = s.Quantity,
                    price = s.Price
                })
                .GroupBy(v => new
                {
                    v.vendor,
                }).ToList();


            XmlDocument doc = new XmlDocument();
            XmlNode docNode = doc.CreateXmlDeclaration("1.0", "UTF-8", null);
            doc.AppendChild(docNode);
            XmlElement sales = doc.CreateElement("sales");
            doc.AppendChild(sales);

            foreach (var vendorData in data)
            {
                XmlNode vendorTag = doc.LastChild;
                XmlElement sale = doc.CreateElement("sale");

                sale.SetAttribute("vendor", vendorData.Key.vendor);

                double totalSum = 0;

                var vendorByDate = vendorData
                        .GroupBy(g => g.date)
                        .Select(s => new
                        {
                            date = s.Key,
                            quantity = s.Select(q => q.quontity),
                            price = s.Select(p => p.price)
                        })
                        .ToList();

                foreach (var d in vendorByDate)
                {
                    XmlElement summary = doc.CreateElement("summary");
                    summary.SetAttribute("date", d.date.ToString("dd-MMM-yyyy", CultureInfo.InvariantCulture));

                    var q = d.quantity.ToList();
                    var p = d.price.ToList();
                    for (int i = 0; i < q.Count; i++)
                    {
                        totalSum += q[i] * p[i];
                    }

                    summary.SetAttribute("total-sum", String.Format("{0:0.00}", totalSum));
                    sale.AppendChild(summary);
                    totalSum = 0;
                }

                vendorTag.AppendChild(sale);
            }

            doc.Save(@"..\..\..\Reports\Sales-by-Vendors-Report.xml");
        }
    }
}
