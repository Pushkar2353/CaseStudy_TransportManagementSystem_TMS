using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TMS_Library.TMS.Entity;
using TMS_Library.TMS.Util;
using TMS_Library.TMS.Dao;
using System.Data.SqlClient;
using Microsoft.Extensions.Configuration;

namespace TMS_Library.TMS.Main
{
    class Program
    {
        static void Main(string[] args)
        {
            try
            {
                using (var connection = DBConnUtil.GetConnection())
                {
                    // Use the connection to interact with the database
                    Console.WriteLine("Connection established successfully!");
                    // Perform database operations here
                }
            }
            catch (ApplicationException ex)
            {
                Console.WriteLine($"An error occurred: {ex.Message}");
            }
        }
    }
}
