using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LinqCodeTemplate
{
    internal class Problem9
    {
        static void Main(string[] args)
        {
            Product p = new Product();
            var products = p.GetProducts();
            var result = products.Where(pr => pr.ProCategory == "FMCG").OrderByDescending(pr=>pr.ProMrp).FirstOrDefault();

            //foreach (var item in result)
            //{
                
                    Console.WriteLine(result.ProCode + " " + result.ProName);
 

            //}
            Console.ReadKey();
        }
    }
}
