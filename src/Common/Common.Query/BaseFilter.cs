namespace Common.Query;

public class BaseFilter<TData, TParam> : BasePaginate where TData : BaseDto
{
    public required List<TData> Datas { get; set; }
    public required TParam FilterParams { get; set; }
}

public class BaseFilterParam
{
    public int PageId { get; set; } = 1;

    public int Take { get; set; } = 6;
}
public class BaseFilter<TData> : BasePaginate where TData : BaseDto
{
    public required List<TData> Datas { get; set; }
}