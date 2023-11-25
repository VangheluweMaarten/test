using System;
using System.IO;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Azure.WebJobs;
using Microsoft.Azure.WebJobs.Extensions.Http;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using mct.models;
using howest.models;
namespace mct.functions
{
    public static class CalculatorTrigger
    {
        [FunctionName("CalculatorTrigger")]
        public static async Task<IActionResult> Run(
            [HttpTrigger(AuthorizationLevel.Anonymous, "get", Route = "calculator/{a}/{sign}/{b}")] HttpRequest req, int a, int b, string sign,
            ILogger log)
        {
            log.LogInformation("C# HTTP trigger function processed a request.");
            
            // string name = req.Query["name"];

            // string requestBody = await new StreamReader(req.Body).ReadToEndAsync();
            // dynamic data = JsonConvert.DeserializeObject(requestBody);
            // name = name ?? data?.name;

            // string responseMessage = string.IsNullOrEmpty(name)
            //     ? "This HTTP triggered function executed successfully. Pass a name in the query string or in the request body for a personalized response."
            //     : $"Hello, {name}. This HTTP triggered function executed successfully.";
            if(sign == "+"){
                CalculationResult result = new CalculationResult(){
                Result = a+b,
                Operator = sign
                };
                var json =JsonConvert.SerializeObject(result);
                return new OkObjectResult(json);
            }
        
            else if(sign == "-"){
                CalculationResult result = new CalculationResult(){
                Result = a-b,
                Operator = sign
            };
                return new OkObjectResult(a-b);
            }
            else if(sign == "*"){
                  CalculationResult result = new CalculationResult(){
                Result = a*b,
                Operator = sign
            };
            
                var json =JsonConvert.SerializeObject(result);
                return new OkObjectResult(json);
            }
            else if(sign == ":"){
                  CalculationResult result = new CalculationResult(){
                Result = a/b,
                Operator = sign
            };
            
                var json =JsonConvert.SerializeObject(result);
                return new OkObjectResult(json);
            }
            else{
                return new OkObjectResult("Invalid sign");
            }
            
        }
    }
}
