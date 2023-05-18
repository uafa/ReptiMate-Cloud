using Model;
using Newtonsoft.Json.Linq;

namespace WebSocket;

public class DataConvertor
{
    public string GetData(string iotData)
    {
        var jObject = JObject.Parse(iotData);

        if (jObject["data"] == null)
        {
            throw new InvalidDataException();
        }

        return jObject["data"].Value<string>();
    }
    
    public string ConvertTemperatureLimitsToHex(TerrariumLimits terrariumLimits)
    {
        double minTemp = terrariumLimits.TemperatureLimitMin * 10D; // Multiply by 10 to match the expected format
        
        string minTempHexa = ((int)minTemp).ToString("X4"); // Convert to hexadecimal string with 4 digits
        
        double maxTemp = terrariumLimits.TemperatureLimitMax * 10D;
        
        string maxTempHexa = ((int)maxTemp).ToString("X4");
        return minTempHexa + maxTempHexa;
    }
}