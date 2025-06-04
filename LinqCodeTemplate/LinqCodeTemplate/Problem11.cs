using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LinqCodeTemplate
{
    internal class Problem11
    {
        static void Main()
        {
            Product product = new Product();
            var products = product.GetProducts();
            var fmcgCount = products.Count(p => p.ProCategory == "FMCG");
            Console.WriteLine("Count of FMCG category :"+fmcgCount);

            Console.ReadLine();
        }
    }
}
