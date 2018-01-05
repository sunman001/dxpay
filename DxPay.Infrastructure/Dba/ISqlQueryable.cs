namespace DxPay.Infrastructure.Dba
{
    public interface ISqlQueryable<T>
    {
        ISqlQueryable<T> Where(string @where, object para = null);
        ISqlQueryable<T> OrderBy(string orderBy);

        int Count();
    }
}
