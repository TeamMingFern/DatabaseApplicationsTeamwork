namespace Supermarket.ImportExel
{
    using System;
    using System.Globalization;
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
            var xmlDoc = XDocument.Load(this.ExpensesImportPath);
            var vendorNamesList = xmlDoc.Root.Elements("vendor");
 
            foreach (var vendorElement in vendorNamesList)
            {
                ///V/endor  v = null;
                //if (vendorElement.Attribute("name").Value != null)
                string vendorName = vendorElement.Attribute("name").Value;
                if (vendorName == null)
                {
                    throw new Exception("no vendor name found");
                }

                var VendorEntity = SupermarketContext.Vendors.Where(v => v.VendorName == vendorName).FirstOrDefault();

                if (VendorEntity == null)
                {
                    VendorEntity =  new Vendor()
                    {
                         VendorName = vendorName
                    };

                    SupermarketContext.Vendors.Add(VendorEntity);
                }

                var monthExpenses = vendorElement.Elements("expenses");
                foreach (var monthExpense in monthExpenses)
                {
                    var dt = monthExpense.Attribute("month").Value;
                    var parsedDatetime = DateTime.Parse(dt);
                    var expense = monthExpense.Value;      
         
                    var vendorExpense = new VendorExpense()
                    {
                        VendorId = VendorEntity.Id,
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