using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.IO;
using Newtonsoft.Json;
using Models.EF;
using PagedList.Mvc;
using PagedList;
using Models.Dao;

namespace MovieProject.Areas.Admin.Controllers
{
    public class GroupUserController : BaseController
    {
        // GET: Admin/GroupUser
        MovieProjectDbcontext db = new MovieProjectDbcontext();
        [HashCredential(RoleID = "VIEW_USER")]
        public ActionResult Index(int page = 1, int pageSize = 5)
        {


            var dao = new GroupUserDao();
            var model = dao.ListPg(page, pageSize);
            return View(model);
        }
        public List<UserGroup> GetData()
        {
            return db.UserGroups.ToList();


        }

    }
}