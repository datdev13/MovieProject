using System.Linq;
using System.Web.Mvc;
using Models.EF;

namespace MovieProject.Areas.Admin.Controllers
{
    public class AdminPaymentController : Controller
    {
        MovieProjectDbcontext db = new MovieProjectDbcontext();

        public ActionResult Index()
        {
            var list = db.Payments.OrderByDescending(x => x.PaymentDate).ToList();
            return View(list);
        }

        public ActionResult Approve(int id)
        {
            var payment = db.Payments.Find(id);
            if (payment != null)
            {
                payment.Status = "Approved";
                var user = db.Users.Find(payment.UserID);
                if (user != null)
                    user.IsPaid = true;
                db.SaveChanges();
            }
            return RedirectToAction("Index");
        }
    }
}
