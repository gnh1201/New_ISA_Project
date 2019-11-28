using LiteDB;
using RestSharp;
using RestSharp.Serialization.Json;
using System.Threading.Tasks;

namespace ISA_Agent
{
    class AssignService
    {
        private static LiteDatabase db = new LiteDatabase(@"ISA.db");

        private static AssignAjaxModel GetData()
        {
            var assigns = db.GetCollection<AssignModel>("assigns");
            var results = assigns.FindAll();
            return new AssignAjaxModel { Data = results };
        }

        internal static async Task<AssignAjaxModel> GetDataAsync()
        {
            return GetData();
        }

        internal static async Task<AssignAjaxModel> GetDataAsync(string asNumber)
        {
            var assigns = db.GetCollection<AssignModel>("assigns");

            var client = new RestClient("https://catswords.re.kr/ep/");
            var request = new RestRequest("/", Method.GET);
            request.AddParameter("route", "isa.assign");
            request.AddParameter("assign_number", asNumber);
            IRestResponse response = client.Execute(request);
            JsonDeserializer deserial = new JsonDeserializer();
            AssignAjaxModel res = deserial.Deserialize<AssignAjaxModel>(response);

            foreach(AssignModel assign in res.Data)
            {
                assigns.Insert(assign);
            }

            return GetData();
        }
    }
}
