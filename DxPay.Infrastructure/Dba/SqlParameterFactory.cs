using System.Data.SqlClient;

namespace DxPay.Infrastructure.Dba
{
    public class SqlParameterFactory
    {
        public static SqlParameter GetParameter
        {
            get
            {
                return new SqlParameter();
            }
        }
    }
}
