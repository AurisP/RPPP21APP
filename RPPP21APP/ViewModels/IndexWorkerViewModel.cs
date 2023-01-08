using RPPP21APP.Models;

namespace RPPP21APP.ViewModels;
public class IndexWorkerViewModel
{
    public IEnumerable<Worker> Workers { get; set; }
    public int PageSize { get; set; }
    public int Page { get; set; }
    public int TotalPages { get; set; }
    public int TotalWorkers { get; set; }
    //public int Category { get; set; }
    public bool HasPreviousPage => Page > 1;

    public bool HasNextPage => Page < TotalPages;
}