using LostAndFound2.Data;
using LostAndFound2.Data.UnitOfWork;
using LostAndFound2.Models;
using Microsoft.AspNetCore.Hosting.Server;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;

namespace LostAndFound2.Controllers
{
    public class ItemController : Controller
    {
        public IActionResult Index(int id)
        {
            UnitOfWork unitOfWork = new UnitOfWork(DBContext.Instance);
            List<Item> items = unitOfWork.ItemRepository.GetAll().ToList();
            return View(items);
        }

        [HttpGet]
        public IActionResult AddItem()
        {
            Debug.WriteLine("hi" + HttpContext.Session.GetString("id"));
            if(HttpContext.Session.GetString("id") == "")
            {
                return RedirectToAction("Login", "User");
            }
            return View();
        }
        [HttpPost]
        public IActionResult AddItem(IFormCollection form ,IFormFile formFile )
        {
            UnitOfWork unitOfWork = new UnitOfWork(DBContext.Instance);
            User user = unitOfWork.UserRepository.GetById(int.Parse(HttpContext.Session.GetString("id") ?? "0"));
            try
            {
                Item item = new Item(form["Name"].ToString() , form["Description"].ToString() , form["Color"].ToString() , form["Image_Link"].ToString(), form["Category"].ToString());
                user.Items.Add(item);
                unitOfWork.Complete();
            }catch(Exception ex)
            {
                Debug.WriteLine(ex.Message);
            }
            return RedirectToAction("Index", "Item");
        }
        public IActionResult ModifyItem(int id)
        {
            Item item = new UnitOfWork(DBContext.Instance).ItemRepository.GetById(id);
            if(HttpContext.Session.GetString("id") == item.Owner.Id.ToString())
            {
                HttpContext.Session.SetString("item", id.ToString());
                return View();
            }
            return RedirectToAction("Login", "User");
        }
        [HttpPost]
        public IActionResult ModifyItem(IFormCollection form)
        {
            UnitOfWork unitOfWork = new UnitOfWork(DBContext.Instance);
            Item item = unitOfWork.ItemRepository.GetById(int.Parse(HttpContext.Session.GetString("item")));
            if (form["Name"] != "")
                item.Name = form["Name"];
            if (form["Color"] != "")
                item.Color = form["Color"];
            if (form["Category"] != "")
                item.Category = form["Category"];
            if (form["Description"] != "")
                item.Category = form["Description"];
            unitOfWork.Complete();
            return RedirectToAction("MyItems", "Item");
        }
        public IActionResult DeleteItem(int id)
        {
            UnitOfWork unitOfWork = new UnitOfWork(DBContext.Instance);
            unitOfWork.ItemRepository.Delete(unitOfWork.ItemRepository.GetById(id));
            return RedirectToAction("MyItems", "Item");
        }
        public IActionResult MyItems()
        {
            if (HttpContext.Session.GetString("id") == "")
                return RedirectToAction("Login", "User");
            UnitOfWork unitOfWork = new UnitOfWork(DBContext.Instance);
            List<Item> items = unitOfWork.ItemRepository.Find(i => i.Owner.Id.ToString() == HttpContext.Session.GetString("id")).ToList();
            return View(items);
        }
        public IActionResult FindItem()
        {
            return View();
        }
        [HttpPost]
        public IActionResult FiltredItem(IFormCollection form)
        {
            UnitOfWork unitOfWork = new UnitOfWork(DBContext.Instance);
            List<Item> items = unitOfWork.ItemRepository.Find(i => i.Color == form["Color"].ToString() && i.Category == form["Category"].ToString()).ToList();
            return View(items);
        }
    }
}
