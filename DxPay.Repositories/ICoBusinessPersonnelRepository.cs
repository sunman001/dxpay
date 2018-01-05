using JMP.MDL;
using System;
namespace DxPay.Repositories
{
    public interface ICoBusinessPersonnelRepository : IRepository<CoBusinessPersonnel>
    {

        bool UpdatePwd(int id , string password);

    }
}
