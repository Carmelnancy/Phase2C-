using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Extensions.Configuration;

namespace DAL.Model
{
    public static class DatabaseHelper
    {
        public static string GetConnectionString()
        {
            var builder = new ConfigurationBuilder().SetBasePath(Directory.GetCurrentDirectory()).AddJsonFile("appSettings.json", optional: true);
            string connectionString=builder.Build().GetConnectionString("EventCon");
            return connectionString;
        }
    }
}
