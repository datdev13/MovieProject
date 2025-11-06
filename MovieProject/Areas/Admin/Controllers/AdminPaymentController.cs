using System;
using System.Linq;
using System.Web.Mvc;
using Models.EF;
using System.Data.Entity;
namespace MovieProject.Areas.Admin.Controllers
{
    public class AdminPaymentController : Controller
    {
        private MovieProjectDbcontext db = new MovieProjectDbcontext();

        // Danh sách các yêu cầu thanh toán
        public ActionResult Index()
        {
            var payments = db.Payments
                .Include(p => p.User) // nạp thêm dữ liệu User
                .ToList();

            return View(payments);
        }

        // Duyệt thanh toán (Admin xác nhận)
        public ActionResult Approve(int id)
        {
            var payment = db.Payments.Find(id);
            if (payment != null)
            {
                payment.Status = "Approved";
                payment.ApprovedDate = DateTime.Now;

                // Cập nhật trạng thái IsPaid và ngày hết hạn
                var user = db.Users.Find(payment.UserID);
                if (user != null)
                {
                    user.IsPaid = true;
                    switch (payment.Package)
                    {
                        case "1 tháng":
                            user.PaidUntil = DateTime.Now.AddMonths(1);
                            break;
                        case "3 tháng":
                            user.PaidUntil = DateTime.Now.AddMonths(3);
                            break;
                        case "12 tháng":
                            user.PaidUntil = DateTime.Now.AddMonths(12);
                            break;
                    }
                }

                db.SaveChanges();
            }
            return RedirectToAction("Index");
        }
    }
}
