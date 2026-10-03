namespace Common.Query;

using System;
using System.Linq;
public class BasePaginate
{
    public int EntityCount { get; set; }
    public int CurrentPage { get; set; }
    public int PageCount { get; set; }
    public int StartPage { get; set; }
    public int EndPage { get; set; }
    public int Take { get; private set; }

    public void GeneratePaging(IQueryable<Object> data, int take, int currentPage)
    {
        var entityCount = data.Count();
        var pageCount = (int)Math.Ceiling(entityCount / (double)take);
        this.PageCount = pageCount;
        this.CurrentPage = currentPage;
        this.EndPage = (currentPage + 5 > pageCount) ? pageCount : currentPage + 5;
        this.EntityCount = entityCount;
        this.Take = take;
        this.StartPage = (currentPage - 4 <= 0) ? 1 : currentPage - 4;
    }
    public void GeneratePaging(int data, int take, int currentPage)
    {
        var entityCount = data;
        var pageCount = (int)Math.Ceiling(entityCount / (double)take);
        this.PageCount = pageCount;
        this.CurrentPage = currentPage;
        this.EndPage = (currentPage + 5 > pageCount) ? pageCount : currentPage + 5;
        this.EntityCount = entityCount;
        this.Take = take;
        this.StartPage = (currentPage - 4 <= 0) ? 1 : currentPage - 4;
    }
}
