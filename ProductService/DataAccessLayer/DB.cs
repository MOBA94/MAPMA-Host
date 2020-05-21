using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Configuration;


namespace ProductService.DataAccessLayer {

    /// <summary>
    /// <author>
    /// Mick O. B. Andersen
    /// Anders S. Brygger
    /// Peter S. Clausen
    /// Anders B. Larsen
    /// Mads G. Ranzau
    /// </author>
    /// </summary>
    public class DB {
        /// <summary>
        /// This returns the ConnectionString from ip.config file 
        /// </summary>
        public static string DbConnectionString {
            get { return ConfigurationManager.ConnectionStrings["ConnectMsSqlString"].ToString(); }
            //get {
            //    return "Server=localhost; Integrated Security=true; Database=NotSoCoolShop";
            //}
        }
    }
}
