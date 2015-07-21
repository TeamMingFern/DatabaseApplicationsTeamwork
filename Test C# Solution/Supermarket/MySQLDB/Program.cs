using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MySQLDB
{
    class Program
    {
        static void Main(string[] args)
        {
            var context = new salesEntities();
            var test = context.measures.First().measureName;
            Console.WriteLine(test);

        }
    }
}
