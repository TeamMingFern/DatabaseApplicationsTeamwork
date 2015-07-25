namespace SupermarketChain.ConsoleClient.Engines
{
    using System;
    using System.Data.Entity.Migrations;
    using System.Linq;
    using System.Xml;
    using MS_SQL_Server;
    using Supermarket.Data;
 
    public static class ImportFromXml
    {


        public static void ImportXML()
        {
            Console.WriteLine(@"        Importing XML To MsSqlDB
------------------------------------------------");

            var context = new SupermarketContext();
            XmlDocument xml = new XmlDocument();
            xml.Load(@"..\..\..\..\Import\Sample-Vendor-Expenses.xml");
            XmlNodeList vendors = xml.DocumentElement.SelectNodes("/expenses-by-month/vendor");

            if (vendors != null)
            {
                foreach (XmlNode item in vendors)
                {
                    if (item.Attributes != null)
                    {
                        string vendorName = item.Attributes["name"].Value;

                        foreach (XmlNode expens in item.ChildNodes)
                        {
                            var vendorId = MsSqlManager.GetVendorIdByName(vendorName);
                            DateTime expenseMonth = DateTime.Parse(expens.Attributes["month"].Value);
                            decimal expensePrice = Convert.ToDecimal(expens.InnerText);

                            Console.WriteLine("Vendor: " + vendorName + "       Expense: " + expensePrice + ",  " + "Expense Month: " + expenseMonth);

                            context.VendorExpenses.AddOrUpdate(
                                v => new
                                {
                                    v.VendorId,
                                    v.Date
                                }
                                ,new VendorExpense
                                {
                                    VendorId = (int)vendorId,
                                    Date = expenseMonth,
                                    Total = expensePrice
                                });
                        }
                    }

                    context.SaveChanges();
                }
            }
        }       
    }
}