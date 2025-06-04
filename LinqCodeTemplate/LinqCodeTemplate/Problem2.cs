using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LinqCodeTemplate
{
    internal class Problem2
    {
        static void Main(string[] args)
        {
            Product product = new Product();
            var products=product.GetProducts();
            var result = products.Where(p => p.ProCategory == "Grain");
            foreach (var item in result)
            {
                Console.WriteLine(item.ProCode +" "+item.ProName );
                
            }
            Console.ReadKey();
        }
    }
}
