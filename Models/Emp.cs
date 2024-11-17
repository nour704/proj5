using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.SqlClient;

namespace proj5.Models
{
    public class Emp
    {
        private readonly string connectionString;

        public int EmpID { get; set; }
        public string EmpName { get; set; }
        public string JobTitle { get; set; }
        public string EmpPassword { get; set; }
        public int PhoneNumber { get; set; }
        public int Id { get; set; }
        public string Name { get; set; }
        public string Role { get; set; }
        
        public Emp(string connectionString, int empID, string empName, string jobTitle, string empPassword, int phoneNumber)
        {
            this.connectionString = connectionString;
            EmpID = empID;
            EmpName = empName;
            JobTitle = jobTitle;
            EmpPassword = empPassword;
            PhoneNumber = phoneNumber;
        }

        public Emp()
        {
            connectionString = GetConnectionString("MyConnectionString");
        }

        private string GetConnectionString(string name)
        {
            return ConfigurationManager.ConnectionStrings[name]?.ConnectionString;
        }

        public List<Emp> GetEmployees()
        {
            List<Emp> employees = new List<Emp>();

            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                conn.Open();

                string query = "SELECT TOP (1000) [Emp_ID], [Emp_Name], [Emp_Password], [Job_Title], [Phone_Number] FROM [EMSystem].[dbo].[EMP]";

                using (SqlCommand cmd = new SqlCommand(query, conn))
                {
                    using (SqlDataReader reader = cmd.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            employees.Add(new Emp
                            {
                                EmpID = reader.GetInt32(reader.GetOrdinal("Emp_ID")),
                                EmpName = reader.IsDBNull(reader.GetOrdinal("Emp_Name")) ? null : reader.GetString(reader.GetOrdinal("Emp_Name")),
                                EmpPassword = reader.IsDBNull(reader.GetOrdinal("Emp_Password")) ? null : reader.GetString(reader.GetOrdinal("Emp_Password")),
                                JobTitle = reader.IsDBNull(reader.GetOrdinal("Job_Title")) ? null : reader.GetString(reader.GetOrdinal("Job_Title")),
                                PhoneNumber = reader.GetInt32(reader.GetOrdinal("Phone_Number"))
                            });
                        }
                    }
                }
            }

            return employees; 
        }
    }
}


