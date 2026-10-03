namespace Common.Query;

public class QueryFilter<TResponse, TParam>(TParam filterParams) : IQuery<TResponse>
    where TResponse : BasePaginate where TParam : BaseFilterParam
{
    public TParam FilterParams { get; set; } = filterParams;
}
