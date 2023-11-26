using System;
using System.IO;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Azure.WebJobs;
using Microsoft.Azure.WebJobs.Extensions.Http;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using System.Data.SqlClient;
using System.Collections.Generic;
using Azure.Identity;
namespace mct.trigger
{
    public static class GetDagen
    {
        [FunctionName("GetDagen")]
        public static async Task<IActionResult> Run(
            [HttpTrigger(AuthorizationLevel.Anonymous, "get", Route = "days")] HttpRequest req,
            ILogger log)
        {
            try{
            

            List<string> days = new List<string>();
            string connectionString = Environment.GetEnvironmentVariable("ConnectionString"); 
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
               
                await connection.OpenAsync();
                using (SqlCommand command = new SqlCommand())
                {
                    command.Connection = connection;
                    command.CommandText = "SELECT DISTINCT DagVanDeWeek FROM Bezoekers";
                    SqlDataReader reader = await command.ExecuteReaderAsync();
                    while (await reader.ReadAsync())
                    {
                        days.Add(reader["DagVanDeWeek"].ToString());
                    }

                }
            }
            return new OkObjectResult(days);
            }   
            catch(Exception ex){
                log.LogError(ex.ToString());
                return new StatusCodeResult(500);
            }
            
        }
    }
}
