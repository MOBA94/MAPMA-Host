using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ServiceModel;
using serviseToHoset = ProductService;
using ProductService.ControlLayer;

namespace ProductServiceHost
{
    class Program
    {

        /// <summary>
        /// the method to run all the service and open all the connections
        /// </summary>
        /// <param name="args"></param>
        static void Main(string[] args)
        {
            
            
            Console.WriteLine("----------ConsoleHosting-----------");
            using (ServiceHost EmpServiceHost = new ServiceHost(typeof(serviseToHoset.EmplyeeServices)))
            using (ServiceHost CustomerServiceHost = new ServiceHost(typeof(serviseToHoset.CustomerServices)))
            using (ServiceHost EscapeRoomServiceHost = new ServiceHost(typeof(serviseToHoset.EscapeRoom_Services)))
            using (ServiceHost BookingServiceHost = new ServiceHost(typeof(serviseToHoset.BookingServices)))
                
                
                {
                // open the host and start listening for incomming calls
                EmpServiceHost.Open();
                CustomerServiceHost.Open();
                EscapeRoomServiceHost.Open();
                BookingServiceHost.Open();


                DisplayHostInfo(EmpServiceHost);
                DisplayHostInfo(CustomerServiceHost);
                DisplayHostInfo(EscapeRoomServiceHost);
                DisplayHostInfo(BookingServiceHost);


                // keep the service running intill the key pressed
                Console.WriteLine("the service is ready");
                Console.WriteLine("press a key to terminate");
                Console.ReadKey();
            }
        }

        /// <summary>
        /// the method to print the info for every connection there is
        /// </summary>
        /// <param name="host"></param>
        static void DisplayHostInfo(ServiceHost host)
        {

            Console.WriteLine();
            Console.WriteLine("--Host Info --");

            foreach (System.ServiceModel.Description.ServiceEndpoint service in host.Description.Endpoints)
            {
                Console.WriteLine($"Address: {service.Address}");
                Console.WriteLine($"Binding: {service.Binding}");
                Console.WriteLine($"Contract: {service.Contract}");
            }
            Console.WriteLine("--------------------");


        }
    }
}

