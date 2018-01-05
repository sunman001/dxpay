using System;
using JMP.MDL;
using DxPay.Infrastructure.Dba;

namespace DxPay.Repositories
{
    public class CoBusinessPersonnelRepository : GenericRepository<CoBusinessPersonnel>, ICoBusinessPersonnelRepository
    {
        public bool UpdatePwd(int id, string password)
        {
            string sql = " update CoBusinessPersonnel set Password = '" + password + "' where Id='" + id + "'";
            var rows = DbHelperSql.ExecuteSql(sql);
            if (rows > 0)
            {
                return true;
            }
            else
            {
                return false;
            }
        }

      
    }

}
