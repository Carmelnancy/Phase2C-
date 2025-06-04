using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LinqCodeTemplate
{
    internal class Problem3
    {
        static void Main(string[] args)
        {
            Product p=new Product();
            var products=p.GetProducts();
            var result = products.OrderBy(pro => pro.ProCode);

            foreach (var item in result)
            {
                Console.WriteLine(item.ProCode + " " + item.ProName);

            }
            Console.ReadKey();
        }
    }
}
