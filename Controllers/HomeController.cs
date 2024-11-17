using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;
using proj5.Models;
using System.Data.SqlClient;
namespace proj5.Controllers
{
    public class HomeController : Controller
    {
        public ActionResult Home()
        {
            ViewBag.Message = "Welcome to our home page!";
            return View();
        }

        public ActionResult About()
        {
            return View();
        }

        public ActionResult EmployeeList()
        {
            List<Emp> empModel = new Emp().GetEmployees();
            return View(empModel);
        }

        public ActionResult Team()
        {
            List<Emp> empModel = new Emp().GetEmployees();
            return View(empModel);
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";
            return View();
        }

        public ActionResult Index()
        {
            using (var context = new AppDbContext())
            {


                List<Category> categories = new Category().GetCategories();
                var employees = new Emp().GetEmployees();

                var features = new Feature().GetFeatures();

                var model = new Tuple<List<Emp>, List<Category>, List<Feature>>(employees, categories, features);

                return View(model);
            }
        }
        public ActionResult TestDatabaseConnection()
        {
            using (var context = new AppDbContext())
            {
                var categories = context.Categories.ToList();
                if (categories.Any())
                {
                    return Content("تم الاتصال بقاعدة البيانات بنجاح.");
                }
                else
                {
                    return Content("قاعدة البيانات متصلة، ولكن لا توجد بيانات.");
                }
            }
        }
    }
}




