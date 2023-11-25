using System;
using System.IO;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Azure.WebJobs;
using Microsoft.Azure.WebJobs.Extensions.Http;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using howest.models;
using mct.models;
namespace howest.trigger
{
    public static class CalculatorPostTrigger
    {
        [FunctionName("CalculatorPostTrigger")]
        public static async Task<IActionResult> Run(
            [HttpTrigger(AuthorizationLevel.Anonymous, "post", Route = "calculator")] HttpRequest req,
            ILogger log)
        {
            log.LogInformation("C# HTTP trigger function processed a request.");

            string name = req.Query["name"];

            string requestBody = await new StreamReader(req.Body).ReadToEndAsync();
            CalculationRequest data = JsonConvert.DeserializeObject<CalculationRequest>(requestBody);
            if (data.Operator == "+"){
                CalculationResult result = new CalculationResult(){
                    Result = data.Number1 + data.Number2,
                    Operator = data.Operator
                };
                var json = JsonConvert.SerializeObject(result);
                return new OkObjectResult(json);
            }
            else if (data.Operator == "-"){
                CalculationResult result = new CalculationResult(){
                    Result = data.Number1 - data.Number2,
                    Operator = data.Operator
                };
                var json = JsonConvert.SerializeObject(result);
                return new OkObjectResult(json);
            }
            else if (data.Operator == "*"){
                CalculationResult result = new CalculationResult(){
                    Result = data.Number1 * data.Number2,
                    Operator = data.Operator
                };
                var json = JsonConvert.SerializeObject(result);
                return new OkObjectResult(json);
            }
            else if (data.Operator == ":"){
                CalculationResult result = new CalculationResult(){
                    Result = data.Number1 / data.Number2,
                    Operator = data.Operator
                };
                var json = JsonConvert.SerializeObject(result);
                return new OkObjectResult(json);
            }
            else{
                return new BadRequestObjectResult("Operator not supported");
            }

        }
    }
}
