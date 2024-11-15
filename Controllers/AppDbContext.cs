using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;
using proj5.Models;

namespace proj5.Controllers
{
    public class CategoriesController : Controller
    {
        private readonly AppDbContext _context;

        public CategoriesController(AppDbContext context)
        {
            _context = context;
        }

        public ActionResult Index()
        {
          
            List<Category> categories = _context.Categories.ToList();
            var employees = new Emp().GetEmployees();

           
            categories = new List<Category>();
            employees = new List<Emp>();

            var model = new Tuple<List<Emp>, List<Category>>(employees, categories);
            return View(model);
        }
    }
}
