using System.Threading.Tasks;

namespace ISA_Agent
{
    public class MainService
    {
        internal static async Task<SuccessModel> GetDataAsync()
        {
            return new SuccessModel { Success = true };
        }
    }
}