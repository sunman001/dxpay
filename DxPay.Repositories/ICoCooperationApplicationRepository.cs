using JMP.MDL;
using System;
namespace DxPay.Repositories
{
    public interface ICoCooperationApplicationRepository : IRepository<CoCooperationApplication>
    {
        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        bool UpdateState(int id,int state, DateTime GrabbedDate, string GrabbedByName, int GrabbedById);
        
    }
}
