using Microsoft.Extensions.Configuration;
using System.IO;
using System.Data.SqlClient;
using System.Collections.Generic;
using Eryaz_ProductSearch.Models;
using System.Data;

namespace Eryaz_ProductSearch.DataLayer
{
    public class ProductDAL
    {
        public string cnn = "";

        public ProductDAL()
        {
            var builder = new ConfigurationBuilder().SetBasePath
                (Directory.GetCurrentDirectory()).
                AddJsonFile("appsettings.json").Build();

            cnn = builder.GetSection("ConnectionStrings:DefaultConnection").Value;
        }

        public List<Products> GetAllProducts()
        {
            try
            {
                List<Products> ListOfProducts = new List<Products>();
                using (SqlConnection cn = new SqlConnection(cnn))
                {
                    using (SqlCommand cmd = new SqlCommand("GetAllProducts", cn))
                    {
                        cmd.CommandType = CommandType.StoredProcedure;

                        if (cn.State != ConnectionState.Open)
                            cn.Open();
                        IDataReader reader = cmd.ExecuteReader();
                        while (reader.Read())
                        {
                            ListOfProducts.Add(new Products()
                            {
                                ProductId = int.Parse(reader["ProductId"].ToString()),
                                ProductName = reader["ProductName"].ToString(),
                                Brand = reader["Brand"].ToString(),
                                Price = int.Parse(reader["Price"].ToString()),
                                Stock = int.Parse(reader["Stock"].ToString())
                            });
                        }
                    }
                }
                return ListOfProducts;
            }
            catch
            {

                throw;
            }
        }

        public int CreateProduct(Products addProduct)
        {
            try
            {
                using (SqlConnection cn = new SqlConnection(cnn))
                {
                    using (SqlCommand cmd = new SqlCommand("CreateProduct", cn))
                    {
                        cmd.Parameters.Add(new SqlParameter("@ProductName", SqlDbType.NVarChar));
                        cmd.Parameters["@ProductName"].Value = addProduct.ProductName;
                        cmd.Parameters.Add(new SqlParameter("@Brand", SqlDbType.NVarChar));
                        cmd.Parameters["@Brand"].Value = addProduct.Brand;
                        cmd.Parameters.Add(new SqlParameter("@Price", SqlDbType.Decimal));
                        cmd.Parameters["@Price"].Value = addProduct.Price;
                        cmd.Parameters.Add(new SqlParameter("@Stock", SqlDbType.Int));
                        cmd.Parameters["@Stock"].Value = addProduct.Stock;
                        cmd.CommandType = CommandType.StoredProcedure;

                        if (cn.State != ConnectionState.Open)
                            cn.Open();
                        cmd.ExecuteNonQuery();
                    }
                }
                return 1;
            }
            catch
            {

                throw;
            }
        }

        public int UpdateProduct(Products product)
        {
            try
            {
                using (SqlConnection cn = new SqlConnection(cnn))
                {
                    using (SqlCommand cmd = new SqlCommand("UpdateProduct", cn))
                    {
                        cmd.Parameters.AddWithValue("@Id", product.ProductId);
                        cmd.Parameters.AddWithValue("@ProductName", product.ProductName);
                        cmd.Parameters.AddWithValue("@Brand", product.Brand);
                        cmd.Parameters.AddWithValue("@Price", product.Price);
                        cmd.Parameters.AddWithValue("@Stock", product.Stock);
                        cmd.CommandType = CommandType.StoredProcedure;

                        if (cn.State != ConnectionState.Open)
                            cn.Open();
                        cmd.ExecuteNonQuery();
                    }
                }
                return 1;
            }
            catch
            {

                throw;
            }
        }

        public Products GetProductById(int ProductId)
        {
            try
            {
                Products product = new Products();
                using (SqlConnection cn = new SqlConnection(cnn))
                {
                    using (SqlCommand cmd = new SqlCommand("GetProductDetailsById", cn))
                    {
                        cmd.Parameters.Add("@ProductId", SqlDbType.Int);
                        cmd.Parameters["@ProductId"].Value = ProductId;
                        cmd.CommandType = CommandType.StoredProcedure;

                        if (cn.State != ConnectionState.Open)
                            cn.Open();
                        IDataReader reader = cmd.ExecuteReader();
                        while (reader.Read())
                        {
                            product.ProductId = int.Parse(reader["ProductId"].ToString());
                            product.ProductName = reader["ProductName"].ToString();
                            product.Brand = reader["Brand"].ToString();
                            product.Price = int.Parse(reader["Price"].ToString());
                            product.Stock = int.Parse(reader["Stock"].ToString());
                        }
                    }
                }
                return product;
            }
            catch
            {

                throw;
            }
        }

        public int DeleteProduct(int ProductId)
        {
            try
            {
                using (SqlConnection cn = new SqlConnection(cnn))
                {
                    using (SqlCommand cmd = new SqlCommand("DeleteProduct", cn))
                    {
                        cmd.Parameters.AddWithValue("@ProductId", ProductId);
                        cmd.CommandType = CommandType.StoredProcedure;

                        if (cn.State != ConnectionState.Open)
                            cn.Open();
                        cmd.ExecuteNonQuery();
                    }
                }
                return 1;
            }
            catch
            {

                throw;
            }
        }
    }
}
