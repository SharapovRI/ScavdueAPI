using System.Net;

namespace Scavdue.OpenStreetMaps.Adapter.Adapters;

public abstract class BaseAdapter
{
    public async static Task<string> DoRequest(string url)
    {
        await Task.Delay(1);
        string result = "";

        HttpWebRequest request = (HttpWebRequest)HttpWebRequest.Create(url);
        request.UserAgent = "Mozilla / 5.0(Windows NT 10.0; Win64; x64; rv: 91.0) Gecko / 20100101 Firefox / 91.0";
        HttpWebResponse response = (HttpWebResponse)request.GetResponse();
        using (Stream stream = response.GetResponseStream())
        {
            using (StreamReader reader = new StreamReader(stream))
            {
                result = reader.ReadToEnd();
            }
        }
        response.Close();

        return result;
    }
}