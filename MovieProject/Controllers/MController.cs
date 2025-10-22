using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Models.Dao;
using PagedList;
using Models.EF;
using System.Data.Entity;

namespace MovieProject.Controllers
{
    public class MController : Controller
    {
        // GET: Movie
        MovieProjectDbcontext db = new MovieProjectDbcontext();
        public ActionResult Index()
        {

            return View();
        }
        [ChildActionOnly]

        public PartialViewResult Category()
        {
            var model = new CategoryDao().ListAll();
            return PartialView(model);

        }
        [ChildActionOnly]

        public PartialViewResult Country()
        {
            var model = new CountryDao().ListAll();
            return PartialView(model);

        }
        [ChildActionOnly]

        public PartialViewResult MenuBottom()
        {
            var model = new CategoryDao().ListAll();
            return PartialView(model);
        }

        public ActionResult Search()
        {

            return View();
        }

        public ActionResult CategoryPage(long idcate, int page = 1)
        {
            var moviDao = new MovieDao();
            var cate = new CategoryDao().ViewDetail(idcate);
            ViewBag.cate = cate;
            ViewBag.ListMovieNew = moviDao.ListMovieNew(12);
            var model = moviDao.ListByCateId(idcate);
            return View(model.ToPagedList(page, 12));
        }
        public ActionResult CountryPage(long idc, int page = 1)
        {
            var movieDao = new MovieDao();
            var country = new CountryDao().ViewDetail(idc);
            ViewBag.country = country;
            ViewBag.ListMovieNew = movieDao.ListMovieNew(12);
            var model = movieDao.ListByCountryID(idc);
            return View(model.ToPagedList(page, 12));

        }
        public ActionResult MovieDetail(int? id)
        {
            if (id == null)
            {
                // Nếu không có id => chuyển hướng về trang chủ hoặc trang lỗi
                return RedirectToAction("Index", "Home");
            }

            var movie = new MovieDao().ViewDetail(id.Value);
            if (movie == null)
            {
                return HttpNotFound();
            }

            ViewBag.movie = movie;
            ViewBag.category = new CategoryDao().ViewDetail(movie.CategoryID.Value);
            ViewBag.ListMovieRelated = new MovieDao().ListMovieRelated(id.Value, 7);
            ViewBag.ListMovieNew1 = new MovieDao().ListMovieNew1(12);

            Movie upview = db.Movies.Find(id.Value);
            if (upview.Viewed == null)
            {
                upview.Viewed = 1;
            }
            else
            {
                upview.Viewed = upview.Viewed + 1;
            }

            db.Entry(upview).State = EntityState.Modified;
            db.SaveChanges();

            return View(upview);
        }


    }
}