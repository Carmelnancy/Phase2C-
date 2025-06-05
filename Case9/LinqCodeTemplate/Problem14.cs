using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LinqCodeTemplate
{
    internal class Problem14
    {
        static void Main()
        {
            Product product = new Product();
            var products = product.GetProducts();
            var result = products.All(p => p.ProMrp<30);
            Console.WriteLine(result ? "All are available below Rs.30" : "Not all are available below Rs.30");
            //foreach (var item in result)
            //{
            //    Console.WriteLine(item.ProCode + " " + item.ProName);

            //}
            Console.ReadLine();
        }
    }
}
