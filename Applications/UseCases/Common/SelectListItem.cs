namespace Applications.UseCases.Common;

public class SelectListItem
{
    public IEnumerable<SelectItem> Assets { get; set; }
    public IEnumerable<SelectItem> Stations { get; set; }
}