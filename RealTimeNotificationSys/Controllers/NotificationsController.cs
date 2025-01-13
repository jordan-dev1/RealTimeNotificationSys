using Microsoft.AspNetCore.Mvc;

namespace RealTimeNotificationSys.Controllers
{
    public class NotificationsController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
