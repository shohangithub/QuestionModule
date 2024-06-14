using DotNetNuke.Web.Mvc.Routing;
namespace Dnn.Modules.YourModuleName

{

    public class RouteConfig : IMvcRouteMapper

    {

        public void RegisterRoutes(IMapRoute mapRouteManager)

        {

            mapRouteManager.MapRoute("QuestionModule", "QuestionModule", "{controller}/{action}", new[]

            {"Dnn.Modules.QuestionModule.Controllers"});

        }

    }

}