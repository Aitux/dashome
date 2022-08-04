namespace Dashome.Core.Models;

public class QueryParameters
{
    public string Search { get; set; }
    public int? Skip { get; set; }
    public int? Limit { get; set; }
    public string OrderBy { get; set; }
    public bool? OrderDesc { get; set; }
}
