using Newtonsoft.Json;
using System.Net.Http;
using System.Text;

namespace Assignment.Extensions
{
    public static class AppExtensions
    {
        public static StringContent ConvertToSerlizedString(this object obj)
        {
            var json = JsonConvert.SerializeObject(obj);
            var data = new StringContent(json, Encoding.UTF8, "application/json");
            return data;
        }
    }
}
