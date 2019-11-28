using EmbedIO;
using EmbedIO.Routing;
using EmbedIO.WebApi;
using System.Threading.Tasks;

namespace ISA_Agent
{
    public sealed class MyApiController : WebApiController
    {
        [Route(HttpVerbs.Post, "/main")]
        public Task<SuccessModel> GetMain() => MainService.GetDataAsync();

        [Route(HttpVerbs.Post, "/assign")]
        public Task<SuccessModel> InitAssign() => Task.Run(() =>
        {
            return new SuccessModel { Success = true };
        });

        [Route(HttpVerbs.Get, "/assign")]
        public Task<AssignAjaxModel> GetAssign() => AssignService.GetDataAsync();

        [Route(HttpVerbs.Get, "/assign/{asNumber}")]
        public Task<AssignAjaxModel> GetAssignByAsNumber(string asNumber) => AssignService.GetDataAsync(asNumber);

        [Route(HttpVerbs.Post, "/bundle")]
        public Task<BundleAjaxModel> GetBundle() => BundleService.GetDataAsync();

        [Route(HttpVerbs.Post, "/notice")]
        public Task<NoticeAjaxModel> GetNotice() => NoticeService.GetDataAsync();

        [Route(HttpVerbs.Get, "/bundle/del/{name}")]
        public Task<SuccessModel> DelBundle(string name) => BundleService.DoUninstall(name);

        [Route(HttpVerbs.Get, "/device")]
        public Task<DeviceAjaxModel> GetDevice() => DeviceService.GetDataAsync();
        
        [Route(HttpVerbs.Post, "/license")]
        public Task<LicenseAjaxModel> GetLicense() => LicenseService.GetDataAsync();

        [Route(HttpVerbs.Post, "/license-form")]
        public Task<SuccessModel> FormLicense() => Task.Run(() =>
        {
            return new SuccessModel { Success = true };
        });

        [Route(HttpVerbs.Get, "/license-attach")]
        public Task<SuccessModel> AttachLicense() => LicenseService.AddDataAsync();
    }
}
