namespace SQLiteDB
{
    using MySqlModel;
    using System.Collections.Generic;
    using System.Linq;

    public class SqLiteManager
    {
        public static HashSet<FinancialReport> GetFinancialReportData()
        {
            var context = new marketsystemEntities();
            var vendors = context.vendors.OrderBy(v => v.vendorName).Select(v => v.vendorName).ToList();
            var allSales = context.sales
                .Select(s => new
                {
                    Vendor = s.product.vendor.vendorName,
                    Product = s.product.productName,
                    Sum = s.quantity * s.price
                })
                .ToList();

            var vendorsTaxes = allSales
                .Select(s => new
                {
                    s.Vendor,
                    Tax = GetTaxByProduct(s.Product) * s.Sum
                })
                .GroupBy(v => v.Vendor, (k, v) => new
                {
                    Vendor = k,
                    Taxes = v.Sum(t => t.Tax)
                })
                .ToDictionary(t => t.Vendor, t => t.Taxes);

            var vendorsIncomes = allSales
                .GroupBy(s => s.Vendor, (k, v) => new
                {
                    Vendor = k,
                    Incomes = v.Sum(a => a.Sum)
                })
                .ToDictionary(t => t.Vendor, t => t.Incomes);

            var vendorsExpenses = GetVendorsExpenses(context);

            var vendorsReport = GenerateReportData(vendors, vendorsIncomes, vendorsExpenses, vendorsTaxes);

            return vendorsReport;
        }


        //Return processed data to be input un xls file
        private static HashSet<FinancialReport> GenerateReportData(
            List<string> vendors,
            Dictionary<string, decimal> vendorsIncomes,
            Dictionary<string, decimal> vendorsExpenses,
            Dictionary<string, decimal> vendorsTaxes)
        {
            var vendorsReport = new HashSet<FinancialReport>();

            foreach (var vendor in vendors)
            {
                var incomes = vendorsIncomes.FirstOrDefault(v => v.Key == vendor);
                var expenses = vendorsExpenses.FirstOrDefault(v => v.Key == vendor);
                var taxes = vendorsTaxes.FirstOrDefault(v => v.Key == vendor);
                var vendorReport = new FinancialReport
                {
                    Vendor = vendor,
                    Incomes = incomes.Value,
                    Expenses = expenses.Value,
                    TotalTaxes = taxes.Value
                };

                vendorsReport.Add(vendorReport);
            }

            return vendorsReport;
        }


        //Return tax by product name from SQLite context
        public static decimal GetTaxByProduct(string productName)
        {
            using (var context = new SQLitetEntities())
            {
                var tax = context.TaxInformations.FirstOrDefault(t => t.ProductName == productName);

                return tax != null ? (tax.Tax/100M) : 0;
            }
        }

        //Return vendors expenses from main DB (MSSQL) that have been previously imported from markets seles reports
        private static Dictionary<string, decimal> GetVendorsExpenses(marketsystemEntities context)
        {
            var vendorExpenses = context.vendor_expenses
                .GroupBy(e => e.vendor.vendorName, (k, v) => new
                {
                    Vendor = k,
                    Expenses = v.Sum(s => s.total)
                })
                .ToDictionary(t => t.Vendor, t => t.Expenses);

            return vendorExpenses;
        }
    }
}