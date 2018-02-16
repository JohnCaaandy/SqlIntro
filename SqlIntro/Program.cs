using System;
using System.Configuration;
using MySql.Data.MySqlClient;

namespace SqlIntro
{
    class Program
    {
        static void Main(string[] args)
        {
            var connectionString = ConfigurationManager.ConnectionStrings["MySQLConnection"].ConnectionString;
            var connection = new MySqlConnection(connectionString);

            var repo = new ProductRepository(connection);

            Product product = null;

            foreach (var prod in repo.GetProducts())
            {
                if (product == null)
                {
                    product = prod;
                }

                Console.WriteLine("Product Name:" + prod.Name);
            }

            repo.DeleteProduct(3);

            if (product != null)
            {
                product.Name = "Cody's lame product";
                repo.UpdateProduct(product);
            }
            Console.ReadLine();
        }


    }
}
