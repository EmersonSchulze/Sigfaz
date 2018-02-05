namespace Sigfaz.Infra.Data.Extension.Paginacao
{
    public interface IQueryPaginacao
    {
        string Count(string query);
        string ApplySqlPagination(string query);
    }
}