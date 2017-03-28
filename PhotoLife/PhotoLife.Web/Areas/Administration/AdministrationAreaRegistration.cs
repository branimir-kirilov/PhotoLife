using System.Web.Mvc;

namespace PhotoLife.Areas.Administration
{
    public class AdministrationAreaRegistration : AreaRegistration 
    {
        public override string AreaName 
        {
            get 
            {
                return "Administration";
            }
        }

        public override void RegisterArea(AreaRegistrationContext context) 
        {
            context.MapRoute(
                "Administration_default",
                "Administration/{controller}/{action}/{id}",
                new { action = "Index", controller = "Administration", id = UrlParameter.Optional },
                namespaces: new[] { "PhotoLife.Areas.Administration.Controllers" }
            );
        }
    }
}