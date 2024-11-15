using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data.Entity;
using System.Web.Services.Description;
using proj5.Models;
using System.ComponentModel.DataAnnotations;
using System.Data.SqlClient;
using System.Configuration;

namespace proj5.Models
{
   
    public class Category

    {
        private readonly string connectionString;
        public int CategoryID { get; set; }

        [Required]
        public string CategoryName { get; set; }

        public string CategoryDescription { get; set; }
        public Category()
        {
            connectionString = GetConnectionString("MyConnectionString");
        }

        private string GetConnectionString(string name)
        {
            return ConfigurationManager.ConnectionStrings[name]?.ConnectionString;
        }

        public List<Category> GetCategories()
        {
            List<Category> categories = new List<Category>();

            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                conn.Open();

                string query = "SELECT TOP (1000) [CategoryID]      ,[CategoryName]      ,[CategoryDescription]  FROM [EMSystem].[dbo].[Categories]";

                using (SqlCommand cmd = new SqlCommand(query, conn))
                {
                    using (SqlDataReader reader = cmd.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            categories.Add(new Category
                            {
                                CategoryID = reader.GetInt32(reader.GetOrdinal("CategoryID")),
                                CategoryName = reader.IsDBNull(reader.GetOrdinal("CategoryName")) ? null : reader.GetString(reader.GetOrdinal("CategoryName")),
                                CategoryDescription = reader.IsDBNull(reader.GetOrdinal("CategoryDescription")) ? null : reader.GetString(reader.GetOrdinal("CategoryDescription")),
                                
                            });
                        }
                    }
                }
            }

            return categories;
        }
    }

    

    public class AppDbContext : DbContext
    {
        public DbSet<Category> Categories { get; set; }
    }

}



