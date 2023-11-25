using Newtonsoft.Json;
namespace mct.models;
public class CalculationResult
{
    [JsonProperty("result")]
    public int Result { get; set; }
    [JsonProperty("operator")]
    public string Operator { get; set; }
}