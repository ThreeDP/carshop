namespace CarShopView.Models;

public interface IPaginationHeader
{
    public int          TotalCount { get; set; }
    public int          PageSize { get; set; }
    public int          CurrentPage { get; set; }
    public int          TotalPages { get; set; }
    public bool         HasNext { get; set; }
    public bool         HasPrevious { get; set; }
}

public class PaginationHeader : IPaginationHeader
{
    public int          TotalCount { get; set; }
    public int          PageSize { get; set; }
    public int          CurrentPage { get; set; }
    public int          TotalPages { get; set; }
    public bool         HasNext { get; set; }
    public bool         HasPrevious { get; set; }
}