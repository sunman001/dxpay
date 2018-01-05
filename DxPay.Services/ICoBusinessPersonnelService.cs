using JMP.MDL;

namespace DxPay.Services
{
    public interface ICoBusinessPersonnelService : IService<CoBusinessPersonnel>
    {
        bool UpdatePwd(int id, string password);
    }
}
