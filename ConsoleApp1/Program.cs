using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleApp1
{
    class Program
    {
        public static DataClases2DataContext context = new DataClases2DataContext();

        static void Main(string[] args)
        {
            Filtering();
            Console.Read();
        }
        static void IntroToLINQ()
        {
            int[] numbers = new int[7] { 0, 1, 2, 3, 4, 5, 6 };

            var numQuery =
                from num in numbers
                where (num % 2) == 0
                select num;

            foreach (int num in numQuery)
            {
                Console.Write("{0,1}", num);
            }

            Console.WriteLine("\n lambda INTROTOLINQ ");

            var list = numbers.Where(nu => nu % 2 == 0);

            foreach (var num in list)
            {
                Console.WriteLine(num);
            }
        }
        static void IntroToLINQLambda()
        {
        
        }
        static void DataSource()
        {
            var queryAllCustomers = from cust in context.clientes select cust;
            foreach (var item in queryAllCustomers)
            {
                Console.WriteLine(item.NombreCompañia);
            }

            Console.WriteLine("DataSource Lambda");
            var queryAllCustomers2 = context.clientes;
            foreach (var item in queryAllCustomers2)
            {
                Console.WriteLine(item.NombreCompañia);
            }

        }


        static void Filtering()
        {
            var queryLondonCustomers = from cust in context.clientes
                                       where cust.Ciudad == "Londres"
                                       select cust;
            foreach (var item in queryLondonCustomers)
            {
                Console.WriteLine(item.Ciudad);
            }
            Console.WriteLine("lambda FILTERING");
            var queryLondonCustomers2 = context.clientes.Where(cust => cust.Ciudad == "Londres");

            foreach (var item in queryLondonCustomers2)
            {
                Console.WriteLine(item.Ciudad);
            }

        }

        static void Ordering()
        {
            var queryLondonCustomers3 =
                from cust in context.clientes
                where cust.Ciudad == "Londres"
                orderby cust.NombreCompañia ascending
                select cust;

            foreach (var item in queryLondonCustomers3)
            {
                Console.WriteLine(item.NombreCompañia);
            }
            //lambda

            Console.WriteLine("LAMBDA ORDERING");
            var custLamb = context.clientes.Where(cust => cust.Ciudad == "Londres").
                OrderByDescending(cust => cust.NombreCompañia).ToList();
            foreach (var item in custLamb)
            {
                Console.WriteLine(item.NombreCompañia);
            
            }

        }

        static void Grouping()
        {
            var queryCustomersByCity = from cust in context.clientes
                                       group cust by cust.Ciudad;

            foreach (var customerGroup in queryCustomersByCity)
            {
                Console.WriteLine(customerGroup.Key);
                foreach (clientes customer in customerGroup)
                {
                    Console.WriteLine(" {0}", customer.NombreCompañia);
                }
            }

            Console.WriteLine("Lambda GRUPING");

            var queryCustomersByCity2 = context.clientes.GroupBy(cust => cust.Ciudad);

            foreach (var customerGroup in queryCustomersByCity2)
            {
                Console.WriteLine(customerGroup.Key);

                foreach (clientes customer in customerGroup)
                {
                    Console.WriteLine(" {0}", customer.NombreCompañia);
                }
            }
        }       

        static void Grouping2()
        {
            var custQuery = from cust in context.clientes
                            group cust by cust.Ciudad into custGroup
                            where custGroup.Count() > 2
                            orderby custGroup.Key
                            select custGroup;

            foreach (var item in custQuery)
            {
                Console.WriteLine(item.Key);
            }
            //LAMBDA
            Console.WriteLine("LAMBDA GRUPING2");
            var custQuery2 = context.clientes.GroupBy(cust => cust.Ciudad).Where(custGroup => 
            custGroup.Count() > 2)
            .OrderBy(custGroup => custGroup.Key);

            foreach (var item in custQuery2)
            {
                Console.WriteLine(item.Key);
            }

        }

        static void Grouping2Lambda()
        { 
         var custQuery = context.clientes.GroupBy(c => c.Ciudad).
                Where(c => c.Key.Count() > 2).OrderBy(l => l.Key);

            foreach (var item in custQuery)
            {
                Console.WriteLine(item.Key);
            }

        }

        static void Joining()
        {
            var innerJoinQuery = from cust in context.clientes
                                 join dist in context.Pedidos
                                 on cust.idCliente equals dist.IdCliente
                                 select new
                                 {
                                     CustomerName = cust.NombreCompañia,
                                     DistributorName = dist.PaisDestinatario
                                 };

            foreach (var item in innerJoinQuery)
            {
                Console.WriteLine(item.CustomerName);
            }
            //LAMBDA
            Console.WriteLine("lambda joinging");
            var innerJoinlmb = context.clientes.Join(context.Pedidos,
                a => a.idCliente,
                b => b.IdCliente,
                (a, b) => new { a.NombreCompañia, b.PaisDestinatario });

            foreach (var item in innerJoinlmb)
            {
                Console.WriteLine($"{item.NombreCompañia} y {item.PaisDestinatario}");
            }
        }
    }
}
