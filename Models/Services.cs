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
   
    public class Service
    {
        private readonly string connectionString;
        public int ServiceID { get; set; }
        public int FeatureID { get; set; }

        [Required]
        public string ServiceName { get; set; }

        public string ServiceDescription { get; set; }
        public Service()
        {
            connectionString = GetConnectionString("MyConnectionString");
        }

        private string GetConnectionString(string name)
        {
            return ConfigurationManager.ConnectionStrings[name]?.ConnectionString;
        }

        public List<Service> GetServices()
        {
            List<Service> Services = new List<Service>();

            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                conn.Open();

                string query = "SELECT TOP (1000) [ServiceID]     ,[ServiceName]      ,[ServiceDescription]      ,[Feature ID]  FROM [EMSystem].[dbo].[Services]";

                using (SqlCommand cmd = new SqlCommand(query, conn))
                {
                    using (SqlDataReader reader = cmd.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            Services.Add(new Service
                            {
                                ServiceID = reader.GetInt32(reader.GetOrdinal("ServiceID")),
                                ServiceName = reader.IsDBNull(reader.GetOrdinal("ServiceName")) ? null : reader.GetString(reader.GetOrdinal("ServiceName")),
                                ServiceDescription = reader.IsDBNull(reader.GetOrdinal("ServiceDescription")) ? null : reader.GetString(reader.GetOrdinal("ServiceDescription")),
                                FeatureID = reader.GetInt32(reader.GetOrdinal("Feature ID")),
                            });
                        }
                    }
                }
            }

            return Services;
        }
    }

    

   

}



