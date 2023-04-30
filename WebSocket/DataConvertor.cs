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
}