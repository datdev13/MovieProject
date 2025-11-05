using System;
using System.Linq;
using System.Web.Mvc;
using Models.EF;


namespace MovieProject.Controllers
{
    public class PaymentController : Controller
    {
        MovieProjectDbcontext db = new MovieProjectDbcontext();

        // Chọn gói cước
        public ActionResult Buy(int id)
        {
            ViewBag.MovieId = id;
            return View();
        }

        // Gửi yêu cầu thanh toán
        [HttpPost]
        public ActionResult Confirm(int movieId, string package)
        {
            // Lấy thông tin session đăng nhập
            var session = (MovieProject.Common.UserLogin)Session["USER_SESSION"];
            if (Session["USER_SESSION"] == null)
            {
                //Nếu chưa đăng nhập thì quay về trang đăng nhập 
                return Redirect("/dangnhap/");
            }

            // Lấy thông tin người dùng trong database
            var user = db.Users.Find(session.UserID);
            if (user == null)
            {
                // Nếu không tìm thấy người dùng trong database thì đăng nhập lại
                return RedirectToAction("Login", "User");
            }

            // Xác định số tiền của từng gói cước
            decimal amount = 0;
            switch (package)
            {
                case "1 tháng": amount = 50000; break;
                case "3 tháng": amount = 120000; break;
                case "12 tháng": amount = 400000; break;
                default: amount = 0; break;
            }

            // Tạo bản ghi thanh toán mới
            var payment = new Payment
            {
                UserID = user.UserID,
                MovieID = movieId,
                Package = package,
                Amount = amount,
                PaymentDate = DateTime.Now,
                Status = "Pending" // chờ admin duyệt
            };

            db.Payments.Add(payment);
            db.SaveChanges();

            // Gửi dữ liệu sang View (trang QR)
            ViewBag.Amount = amount;
            ViewBag.Package = package;
            ViewBag.MovieId = movieId;
            ViewBag.UserName = user.Name;

            // Chuyển sang trang hiển thị mã QR để người dùng thanh toán
            return View("QR");
        }

    }
}
