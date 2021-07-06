using HtmlAgilityPack;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.Data.Sqlite;
using SinavOlusturmaWeb.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;

namespace SinavOlusturmaWeb.Controllers
{
    public class HomeController : Controller
    {
        
        [Authorize]
        public IActionResult Index()
        {            
            return View();
        }


        //---------------------------------
        //sqlite'ten veriler geliyor mu kontrol

        DatabaseContext db = new DatabaseContext();
        public IActionResult Index2()
        {
            var degerler = db.Admin.ToList();
            return View(degerler);
        }

        //---------------------------------



        public IActionResult BaslikGoruntuleme()
        {
            string url = "https://www.wired.com/most-recent/";
            var web = new HtmlAgilityPack.HtmlWeb();
            HtmlDocument doc = web.Load(url);

            //---------------------------------

            var connectionStringBuilder = new SqliteConnectionStringBuilder();
            
            connectionStringBuilder.DataSource = "./SqliteDB.db";

            //---------------------------------
            using (var connection = new SqliteConnection(connectionStringBuilder.ConnectionString))
            {
                connection.Open();

                int num = 1;
                string baslik = "";
                string metin = "";
                List<SelectListItem> basliklar = new List<SelectListItem>();
                List<string> metinler = new List<string>();
                for (int i = 0; i < 5; i++)
                {
                    baslik = doc.DocumentNode.SelectNodes($"//*[@id=\"app-root\"]/div/div[3]/div/div[2]/div/div[1]/div/div/ul/li[{num}]/div/a/h2")[0].InnerText;
                    metin = doc.DocumentNode.SelectNodes($"//*[@id=\"app-root\"]/div/div[3]/div/div[2]/div/div[1]/div/div/ul/li[{num}]/div/a/p")[0].InnerText;

                    using (var transaction = connection.BeginTransaction())
                    {
                        var insertCmd = connection.CreateCommand();

                        SqliteCommand komutIslet = new SqliteCommand(connectionStringBuilder.ConnectionString, connection);

                        komutIslet.Parameters.AddWithValue("@ID", num);
                        komutIslet.Parameters.AddWithValue("@Baslik", baslik);
                        komutIslet.Parameters.AddWithValue("@Yazi", metin);
                        transaction.Commit();

                    }


                    basliklar.Add(new SelectListItem { Text = baslik });
                    metinler.Add(new SelectListItem { Text = metin  }.ToString());
                  
                    num++;
                }

                //foreach (var m in metinler)
                //{
                //    ViewBag.Metinler = metinler;
                //}
                ViewBag.indis = metin;

                ViewBag.Basliklar = basliklar;
                
                return View();

            }
           
            //Sinav c = new Sinav();
            //[HttpPost]
            //public JsonResult metinList(string metin)
            //{
            //    List <metinList> = c.ID.where(f => f.num1 == num2).ToList();

            //    List<SelectListItem> itemList = (from i in metinler
            //                                     select new SelectListItem
            //                                     {
            //                                         Value = i.IlceID.ToString(),
            //                                         Text = i.IlceAdi
            //                                     }).ToList();

            //    return Json(itemList, JsonRequestBehavior.AllowGet);
            //}
        }
    }
}
