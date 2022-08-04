namespace Dashome.Core.Models;

public class PagedResult<TRow>
{
    public int TotalItems { get; set; }
    public IEnumerable<TRow> Result { get; set; }
}
