using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LinqCodeTemplate
{
    internal class Problem7
    {
        static void Main(string[] args)
        {
            Product p = new Product();
            var products = p.GetProducts();
            var result = products.GroupBy(pro => pro.ProCategory);

            foreach (var item in result)
            {
                Console.WriteLine(item.Key);
                foreach (var item1 in item)
                {
                    Console.WriteLine(item1.ProCode + " " + item1.ProName);
                }

            }
            Console.ReadKey();
        }
    }
}
