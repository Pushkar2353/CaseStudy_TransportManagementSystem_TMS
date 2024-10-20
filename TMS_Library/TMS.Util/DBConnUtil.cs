using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TMS_Library.TMS.Dao;
using TMS_Library.TMS.Entity;
using System.Data.SqlClient;
using Microsoft.Data.SqlClient;
using Microsoft.Extensions.Configuration;
using System.Configuration;

namespace TMS_Library.TMS.Util
{
    public static class DBConnUtil
    {
        private static string connectionString = "Server=.\\sqlexpress;Database=TMS;User Id=your_username;Password=your_password;Trusted_Connection=True;TrustServerCertificate=True;";

        public static SqlConnection GetConnection()
        {
            return new SqlConnection(connectionString);
        }
    }
}
