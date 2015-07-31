namespace Supermarket.ImportExel
{
    using System;
    using System.Globalization;
    using System.IO;
    using System.Linq;
    using System.Xml.Linq;
    using Data;
    using MS_SQL_Server;

    public class ImportFromXML
    {
   
        public ImportFromXML(string expensesImportPath, SupermarketContext context)
        {
            this.ExpensesImportPath = expensesImportPath;
            this.SupermarketContext = context;
        }
 
        public string ExpensesImportPath { get; set; }
 
        public SupermarketContext SupermarketContext { get; set; }
 
        public void TransferData()
        {
            Console.WriteLine(@"        Importing XML To MsSqlDB
------------------------------------------------");
            DirectoryInfo directoryInfo = new DirectoryInfo(this.ExpensesImportPath);
            Console.WriteLine("From directory:  {0}", directoryInfo.FullName);
            Console.WriteLine("Extracting data...");
            Console.WriteLine("Sending data...");
            Console.WriteLine("Sales report was imported.");
            var xmlDoc = XDocument.Load(this.ExpensesImportPath);
            var vendorNamesList = xmlDoc.Root.Elements("vendor");
 
            foreach (var vendorElement in vendorNamesList)
            {
                ///Vendor  v = null;
                //if (vendorElement.Attribute("name").Value != null)
                string vendorName = vendorElement.Attribute("name").Value;

                var vendorEntity = SupermarketContext.Vendors.Where(v => v.VendorName == vendorName).FirstOrDefault();

                if (vendorEntity == null)
                {
                    vendorEntity =  new Vendor()
                    {
                         VendorName = vendorName
                    };

                    this.SupermarketContext.Vendors.Add(vendorEntity);
                }

                var monthExpenses = vendorElement.Elements("expenses");
                foreach (var monthExpense in monthExpenses)
                {
                    var dt = monthExpense.Attribute("month").Value;
                    var parsedDatetime = DateTime.Parse(dt);
                    var expense = monthExpense.Value;      
         
                    var vendorExpense = new VendorExpense()
                    {
                        VendorId = vendorEntity.Id,
                        Date = parsedDatetime,
                        Total = decimal.Parse(expense, CultureInfo.InvariantCulture)
                    };
 
                    this.SupermarketContext.VendorExpenses.Add(vendorExpense);
                    this.SupermarketContext.SaveChanges();
                }
            }
        }
    }           
}