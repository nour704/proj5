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
   
    public class Feature

    {
        private readonly string connectionString;
        public int FeatureID { get; set; }
        public int CategoryID { get; set; }
        [Required]
        public string FeatureName { get; set; }

        public string FeatureDescription { get; set; }
        public Feature()
        {
            connectionString = GetConnectionString("MyConnectionString");
        }

        private string GetConnectionString(string name)
        {
            return ConfigurationManager.ConnectionStrings[name]?.ConnectionString;
        }

        public List<Feature> GetFeatures()
        {
            List<Feature> features = new List<Feature>();

            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                conn.Open();

                string query = "SELECT TOP (1000) [FeatureID]      ,[FeatureName],[CategoryID]      ,[FeatureDescription]  FROM [EMSystem].[dbo].[Features]";

                using (SqlCommand cmd = new SqlCommand(query, conn))
                {
                    using (SqlDataReader reader = cmd.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            features.Add(new Feature
                            {
                                FeatureID = reader.GetInt32(reader.GetOrdinal("FeatureID")),
                                CategoryID = reader.GetInt32(reader.GetOrdinal("CategoryID")),
                                FeatureName = reader.IsDBNull(reader.GetOrdinal("FeatureName")) ? null : reader.GetString(reader.GetOrdinal("FeatureName")),
                                FeatureDescription = reader.IsDBNull(reader.GetOrdinal("FeatureDescription")) ? null : reader.GetString(reader.GetOrdinal("FeatureDescription")),
                                
                            });
                        }
                    }
                }
            }

            return features;
        }
    }

    

   

}



