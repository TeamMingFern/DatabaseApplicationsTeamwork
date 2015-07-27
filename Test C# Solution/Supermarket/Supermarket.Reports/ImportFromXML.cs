namespace Supermarket.Reports
{
    using System;
    using System.Linq;
    using System.Xml.Linq;
    using MS_SQL_Server;
    using Data;
    using System.Transactions;

    public static class ImportFromXML
    {
        public static void ImportVendorExpenses(SupermarketContext context)
        {

            XDocument xmlDoc = XDocument.Load(@"..\..\XML.xml");
            var vendors = xmlDoc.Elements("vendor");

            foreach (XElement vendor in vendors)
            {
                string vendorName = vendor.Attribute("name").Value;
                var wantedVendor = context.Vendors.FirstOrDefault(v => v.VendorName == vendorName);

                if (wantedVendor == null)
                {
                    wantedVendor = AddNewVendor(vendorName, context);
                }

                AddVendorExpenses(vendor, wantedVendor, context);
            }
        }

        private static Vendor AddNewVendor(string vendorName, SupermarketContext context)
        {
            var newVendor = new Vendor()
            {
                VendorName = vendorName
            };

            context.Vendors.Add(newVendor);
            return newVendor;
        }

        private static void AddVendorExpenses(XElement vendor, Vendor wantedVendor, SupermarketContext context)
        {
            var expenses = vendor.Descendants("expenses");

            foreach (var expense in expenses)
            {
                using (TransactionScope transaction = new TransactionScope())
                {
                    try
                    {
                        decimal expenseSum = decimal.Parse(expense.Value);
                        DateTime expenseDate = DateTime.Parse(expense.Attribute("month").Value);

                        wantedVendor.Expenses.Add(new VendorExpense()
                        {
                            Date = expenseDate,
                            Total = expenseSum
                        });
                        context.SaveChanges();
                        Console.WriteLine(wantedVendor.Id);
                        transaction.Complete();
                    }
                    catch (Exception e)
                    {
                        Console.WriteLine(e.Message);
                        transaction.Dispose();
                    }
                }
            }
        }
    }
}