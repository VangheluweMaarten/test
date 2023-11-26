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
using mct.models;
namespace mct.trigger
{
    public static class GetVisitsByDay
    {
        [FunctionName("GetVisitsByDay")]
        public static async Task<IActionResult> Run(
            [HttpTrigger(AuthorizationLevel.Anonymous, "get", Route = "visitors/{day}")]  HttpRequest req,
            ILogger log,string day)
        {
            List<Visit> visits = new List<Visit>();
           string connectionString = Environment.GetEnvironmentVariable("ConnectionString");
           using (SqlConnection connection = new SqlConnection(connectionString)){
                await connection.OpenAsync();
                SqlCommand command = new SqlCommand("SELECT * FROM Bezoekers WHERE DagVanDeWeek = @day",connection);
                command.Parameters.AddWithValue("@day",day);
                SqlDataReader reader = await command.ExecuteReaderAsync();
                while(await reader.ReadAsync()){
                     Visit visit = new Visit();
                     if(int.TryParse(reader["TijdstipDag"].ToString(),out int result))
                     {
                         visit.Tijdstip = result;
                     }
                     if(int.TryParse(reader["AantalBezoekers"].ToString(),out int result2))
                     {
                         visit.AantalBezoekers = result2;
                     }
                     
                     
                     visit.Dag = reader["DagVanDeWeek"].ToString();
                     visits.Add(visit);
                } 
            }
            return new OkObjectResult(visits);
        }
    }
}
