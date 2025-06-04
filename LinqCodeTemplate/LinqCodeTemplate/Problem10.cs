using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LinqCodeTemplate
{
    internal class Problem10
    {
        static void Main()
        {
            Product product = new Product();
            var products = product.GetProducts();
            var result = products.Count();
            //foreach (var item in result)
            //{
            //    Console.WriteLine($"{item.ProCode}\t{item.ProName}\t{item.ProMrp}");
            //}
            Console.WriteLine("Count of products : "+result);
            Console.ReadLine();
        }
    }
}
