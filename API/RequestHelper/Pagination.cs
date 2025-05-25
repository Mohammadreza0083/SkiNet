namespace API.RequestHelper;

public class Pagination<T>(int pageIndex, int pageSize, int count, IReadOnlyList<T> date)
{
    //current page index
    public int PageIndex { get; set; } = pageIndex;
    public int PageSize { get; set; } = pageSize;
    public int Count { get; set; } = count;
    public IReadOnlyList<T> Date { get; set; } = date;
}