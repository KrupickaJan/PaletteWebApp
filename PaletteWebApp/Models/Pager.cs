namespace PaletteWebApp.Models;

public class Pager
{
    public int _totalItems;
    public int _currentPage;
    public int _pageSize;
    public int _totalPages;
    public int _startPage;
    public int _endPage;

    public Pager()
    {

    }

    public Pager(int totalItems, int page, int pageSize = 10)
    {
        int totalPages = (int)Math.Ceiling((float)totalItems / (float)pageSize);
        int currentPage = page;
        int startPage = currentPage - 5;
        int endPage = currentPage + 4;
        
        if(startPage <= 0 )
        {
            endPage = endPage - (startPage - 1);
            startPage = 1;
        }

        if(endPage > totalPages)
        {
            endPage = totalPages;
            if(endPage > 10)
            {
                startPage = endPage - 9;
            }
        }

        _totalItems = totalItems;
        _currentPage = currentPage;
        _pageSize = pageSize;
        _totalPages = totalPages;
        _startPage = startPage;
        _endPage = endPage;
    }

    public int TotalItems { get => _totalPages; }
    public int CurrentPage { get => _currentPage; }
    public int PageSize { get => _pageSize; }
    public int TotalPages { get => _totalPages; }
    public int StartPage { get => _startPage; }
    public int EndPage { get => _endPage; }

}
