﻿using System;
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
    public class ProductRepository : IDisposable, IProductRepository
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
            var cmd = _conn.CreateCommand();
            cmd.CommandText = @"select Name, ListPrice 
                                from product ORDER BY Name";
            var dr = cmd.ExecuteReader();
            while (dr.Read())
            {
                yield return new Product
                {
                    Name = dr["Name"].ToString(),
                    ListPrice = (double)dr["ListPrice"]
                };
            }
        }

        /// <summary>
        /// Deletes a Product from the database
        /// </summary>
        /// <param name="id"></param>
        public void DeleteProduct(int id)
        {
            var cmd = _conn.CreateCommand();
            cmd.CommandText = ""; //Write a delete statement that deletes by id
            cmd.ExecuteNonQuery();

        }
        /// <summary>
        /// Updates the Product in the database
        /// </summary>
        /// <param name="prod"></param>
        public void UpdateProduct(Product prod)
        {
            //This is annoying and unnecessarily tedious for large objects.
            //More on this in the future...  Nothing to do here..

            var cmd = _conn.CreateCommand();
            cmd.CommandText = "update product set name = @name where id = @id";
            var param = cmd.CreateParameter();
            param.ParameterName = "name";
            param.Value = prod.Name;
            cmd.Parameters.Add(param);
            var param2 = cmd.CreateParameter();
            param2.ParameterName = "id";
            param2.Value = prod.Id;
            cmd.Parameters.Add(param2);
            cmd.ExecuteNonQuery();

        }
        ///// <summary>
        ///// Inserts a new Product into the database
        ///// </summary>
        ///// <param name="prod"></param>
        //public void InsertProduct(Product prod)
        //{

        //    var cmd = _conn.CreateCommand();
        //    cmd.CommandText = "INSERT into product (name) values(@name)";
        //    cmd.Parameters.("@name", prod.Name);
        //    cmd.ExecuteNonQuery();

        //}

        public void Dispose()
        {
            _conn.Dispose();
        }
    }
}