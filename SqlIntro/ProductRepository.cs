using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using MySql.Data.MySqlClient;

namespace SqlIntro
{
    public class ProductRepository : IProductRepository
    {
        private readonly IDbConnection _conn;

        public ProductRepository(IDbConnection conn)
        {
            _conn = conn;
        }

        /// <summary>
        /// Reads all the products from the products table
        /// </summary>
        /// <returns></returns>
        public IEnumerable<Product> GetProducts()
        {
            using (var conn = _conn)
            {
                conn.Open();

                var cmd = conn.CreateCommand();
                cmd.CommandText = "SELECT Name, ProductId FROM product";

                var dr = cmd.ExecuteReader();
                while (dr.Read())
                {
                    yield return new Product
                    {
                        Name = dr["Name"].ToString(),
                        Id = int.Parse(dr["ProductId"].ToString())
                    };
                }
            }
        }

        /// <summary>
        /// Deletes a Product from the database
        /// </summary>
        /// <param name="id"></param>
        public void DeleteProduct(int id)
        {
            using (var conn = _conn)
            {
                conn.Open();

                var cmd = conn.CreateCommand();
                cmd.CommandText = "DELETE FROM product WHERE ProductId= @id";
                cmd.AddParamWithValue("@id", id);
                cmd.ExecuteNonQuery();
            }
        }

        /// <summary>
        /// Updates the Product in the database
        /// </summary>
        /// <param name="prod"></param>
        public void UpdateProduct(Product prod)
        {

            using (var conn = _conn)
            {

                conn.Open();

                var cmd = conn.CreateCommand();
                cmd.CommandText = "update product set name = @name where ProductId = @id";

                cmd.AddParamWithValue("@name", prod.Name);
                cmd.AddParamWithValue("@id", prod.Id);
                cmd.ExecuteNonQuery();
            }
        }

        /// <summary>
        /// Inserts a new Product into the database
        /// </summary>
        /// <param name="prod"></param>
        public void InsertProduct(Product prod)
        {
            using (var conn = _conn)
            {
                var cmd = conn.CreateCommand();
                cmd.CommandText = "INSERT into product (name) values(@name)";
                cmd.AddParamWithValue("@name", prod.Name);
                cmd.ExecuteNonQuery();
            }
        }

        public IEnumerable<Product> GetProductsWithReview()
        {
            using (var conn = _conn)
            {
                conn.Open();

                var cmd = conn.CreateCommand();
                cmd.CommandText = "SELECT product.Name, productreview.Rating FROM product INNER JOIN productreview ON product.ProductID=productreview.ProductID ";

                var dr = cmd.ExecuteReader();
                while (dr.Read())
                {
                    yield return new Product
                    {
                        Name = dr["Name"].ToString(),
                        Id = int.Parse(dr["ProductId"].ToString())
                    };
                }
            }
        }
        public IEnumerable<Product> GetProductsAndReviews()
        {
            using (var conn = _conn)
            {
                conn.Open();

                var cmd = conn.CreateCommand();
                cmd.CommandText = "SELECT product.Name, productreview.Rating FROM product LEFT JOIN productreview ON product.ProductID=productreview.ProductID ";

                var dr = cmd.ExecuteReader();
                while (dr.Read())
                {
                    yield return new Product
                    {
                        Name = dr["Name"].ToString(),
                        Id = int.Parse(dr["ProductId"].ToString())
                    };
                }
            }
        }
    }
}
