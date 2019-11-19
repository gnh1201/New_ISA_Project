using RestSharp;
using RestSharp.Serialization.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ISA_Agent
{
    class NoticeService
    {
        internal static async Task<NoticeAjaxModel> GetDataAsync()
        {
            var client = new RestClient("https://catswords.re.kr/ep/");
            var request = new RestRequest("/", Method.GET);
            request.AddParameter("route", "isa.notice");
            IRestResponse response = client.Execute(request);
            JsonDeserializer deserial = new JsonDeserializer();
            NoticeAjaxModel res = deserial.Deserialize<NoticeAjaxModel>(response);
            return res;
        }
    }
}
