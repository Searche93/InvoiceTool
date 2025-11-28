using Microsoft.AspNetCore.Mvc.Rendering;

namespace InvoiceTool.Mvc.Helpers;

public class EnumHelper
{
    public static IEnumerable<SelectListItem> GetSelectListFromEnum<T>() where T : Enum
    {
        return Enum.GetValues(typeof(T))
            .Cast<T>()
            .Select(e => new SelectListItem
            {
                Text = e.ToString(),
                Value = Convert.ToInt32(e).ToString()
            });
    }
}
