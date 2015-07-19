using System;
using System.IO;
using System.IO.Compression;
using Supermarket.Data;
using System;
using System.Data;
using System.Data.OleDb;
using System.Globalization;
using System.IO;
using System.IO.Compression;
using System.Linq;

namespace ZippedExecReportsReader
{
    public class ZipedExelSqlImporter
    {
        private const string ExcelConnectionString = "Provider=Microsoft.ACE.OLEDB.12.0; Data Source = {0}; Extended Properties=\"Excel 12.0;HDR=YES\"";
        //Provider=Microsoft.Jet.OLEDB.4.0;Data Source=Sample1.xls;Extended Properties=Excel 8.0";
        //"Provider=Microsoft.ACE.OLEDB.12.0; Data Source = {0}; Extended Properties=\"Excel 12.0;HDR=YES\"";
        private string filepath;
        public ZipedExelSqlImporter(string filepath = null)
        {
            if (filepath != null)
            {
                this.FilePath = filepath;
            }
            else
            {
                this.FilePath = "Sample-Sales-Reports.zip"; // zaradi x86 patq na faila se preacka i sega e v bin x86
            }
        }
        public string FilePath { get; private set; }

        public void LoadExelReports()
        {
            string TempFileName = "clinicImport.xls";
            string TempFolderName = @"\extracted\";
            string tempFolder = string.Format("{0}{1}", Directory.GetCurrentDirectory(), TempFolderName);

            string currentReportDate = string.Empty;

            using (ZipArchive archive = ZipFile.OpenRead(this.FilePath))
            {
                foreach (ZipArchiveEntry entry in archive.Entries)
                {
                    if (entry.FullName.EndsWith("/", StringComparison.OrdinalIgnoreCase))
                    {
                        currentReportDate = entry.FullName.TrimEnd('/');
                    }
                    else
                    {
                        if (!Directory.Exists(tempFolder))
                        {
                            Directory.CreateDirectory(tempFolder);
                        }

                        entry.ExtractToFile(Path.Combine(tempFolder, TempFileName), true);

                        DataTable excelData = this.ReadExcelData(string.Format("{0}{1}", tempFolder, TempFileName));

                        foreach (DataRow row in excelData.Rows)
                        {
                            
                            foreach (var r in row.ItemArray)
                            {
                                Console.WriteLine(r);
                                string a = r.ToString();
                            }
                        }
                    }
                }
            }
        }

        private DataTable ReadExcelData(string filePath)
        {
            OleDbConnection excelConnection = new OleDbConnection(string.Format(ExcelConnectionString, filePath));
            DataTable dt = new DataTable();

            excelConnection.Open();
            OleDbDataAdapter da = new System.Data.OleDb.OleDbDataAdapter("select * from [Sales$]", excelConnection);
            da.Fill(dt);
            excelConnection.Close();

            return dt;
        }
    }
}
