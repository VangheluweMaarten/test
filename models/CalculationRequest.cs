using Newtonsoft.Json;
namespace howest.models;
public class CalculationRequest
{
    [JsonProperty("number1")]
    public int Number1 { get; set; }
    [JsonProperty("number2")]
    public int Number2 { get; set; }
    [JsonProperty("operator")]
    public string Operator { get; set; }
}