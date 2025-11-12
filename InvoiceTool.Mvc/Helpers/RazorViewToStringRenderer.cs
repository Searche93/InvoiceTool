using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using Microsoft.AspNetCore.Mvc.Razor;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.AspNetCore.Mvc.ViewFeatures;

namespace InvoiceTool.Mvc.Helpers;

public interface IRazorViewToStringRenderer
{
    Task<string> RenderViewToStringAsync(Controller controller, string viewName, object model = null);
}

public class RazorViewToStringRenderer : IRazorViewToStringRenderer
{
    private readonly IRazorViewEngine _viewEngine;
    private readonly ITempDataProvider _tempDataProvider;

    public RazorViewToStringRenderer(IRazorViewEngine viewEngine, ITempDataProvider tempDataProvider)
    {
        _viewEngine = viewEngine;
        _tempDataProvider = tempDataProvider;
    }

    public async Task<string> RenderViewToStringAsync(Controller controller, string viewName, object model = null)
    {
        var actionContext = new ActionContext(controller.HttpContext, controller.RouteData, controller.ControllerContext.ActionDescriptor);

        using var sw = new StringWriter();

        var viewResult = _viewEngine.FindView(actionContext, viewName, false);

        if (viewResult.View == null)
            throw new ArgumentNullException($"View '{viewName}' not found.");

        var viewDictionary = new ViewDataDictionary(new EmptyModelMetadataProvider(), controller.ModelState)
        {
            Model = model
        };

        var viewContext = new ViewContext(
            actionContext,
            viewResult.View,
            viewDictionary,
            new TempDataDictionary(controller.HttpContext, _tempDataProvider),
            sw,
            new HtmlHelperOptions()
        );

        await viewResult.View.RenderAsync(viewContext);

        return sw.ToString();
    }
}
