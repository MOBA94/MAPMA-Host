using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ServiceModel;
using serviseToHoset = ProductService;

namespace ProductServiceHost
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("----------CosoleHosting-----------");
            using (ServiceHost serviceHost = new ServiceHost(typeof(serviseToHoset.EmplyeeServices)))
            {
                // open the host and start listening for incomming calls
                serviceHost.Open();
                DisplayHostInfo(serviceHost);

                // keep the service running intill the key pressed
                Console.WriteLine("the service is ready");
                Console.WriteLine("press a key to terminate");
                Console.ReadKey();
            }
        }

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

