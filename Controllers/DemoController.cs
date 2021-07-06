using HtmlAgilityPack;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Data.Sqlite;
using SinavOlusturmaWeb.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SinavOlusturmaWeb.Controllers
{
    public class DemoController : Controller
    {
        DatabaseContext c = new DatabaseContext();

        private readonly DatabaseContext _context;
        public DemoController(DatabaseContext context)
        {
            _context = context;
        }

        public IActionResult Index()
        {
            
            return View();
        }
    }
}
