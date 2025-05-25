namespace Core.Specification;

public class ProductSpecParams
{
    private const int MaxPageSize = 25;
    public int PageIndex { get; set; } = 1; // default page index
    private int _pageSize = 6; // default page size
    public int PageSize
    {
        get => _pageSize;
        // if page size is greater than MaxPageSize, set it to MaxPageSize
        set => _pageSize = (value > MaxPageSize) ? MaxPageSize : value; 
    }
    private List<string> _brands = [];
    public List<string> Brands
    {
        get => _brands; // brand = boards,... so we can split it with ','
        set => _brands = value.SelectMany(x => x.Split(',', 
            StringSplitOptions.RemoveEmptyEntries)).ToList();
    }
    private List<string> _types = [];
    public List<string> Types
    {
        get => _types; // type = React,... so we can split it with ','
        set => _types = value.SelectMany(x => x.Split(',', 
            StringSplitOptions.RemoveEmptyEntries)).ToList();
    }

    private string? _search;
    public string Search
    {
        get => _search ?? "";
        set => _search = value.ToLower();
    }
    public string? Sort { get; set; }
}