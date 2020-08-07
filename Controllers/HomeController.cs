using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using ltweb.Models;
using Microsoft.AspNet.Identity.Owin;

namespace ltweb.Controllers
{
    public class HomeController : Controller
    {
        private ApplicationDbContext _Context;

        public ApplicationDbContext Context
        {
            get { return _Context ?? Request.GetOwinContext().Get<ApplicationDbContext>(); }
            set { _Context = value; }
        }

        public async Task<ActionResult> Index()
        {
            //ViewBag.Categories = await Context.Categories.ToListAsync();
            return View(await Context.News.ToListAsync());
        }

        public ActionResult About()
        {
            ViewBag.Message = "Your application description page.";

            return View();
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }
    }
}