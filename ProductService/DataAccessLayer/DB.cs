using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Configuration;


namespace ProductService.DataAccessLayer {
    public class DB {
        public static string DbConnectionString {
            get { return ConfigurationManager.ConnectionStrings["ConnectMsSqlString"].ToString(); }
            //get {
            //    return "Server=localhost; Integrated Security=true; Database=NotSoCoolShop";
            //}
        }
    }
}
