using System.Web.Mvc;

namespace PhotoLife.Area
{
    public class AreaAdminRegistration : AreaRegistration
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
                new { action = "Index", controller = "Admin", id = UrlParameter.Optional }
            );
        }
    }
}