using LostAndFound2.Data;
using LostAndFound2.Data.UnitOfWork;
using LostAndFound2.Models;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;

namespace LostAndFound2.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;

        public HomeController(ILogger<HomeController> logger)
        {
            _logger = logger;
        }

        public IActionResult Index(int id)
        {
            var context = DBContext.Instance;
            try
            {
                context.Database.EnsureCreated();
            }catch(Exception ex)
            {
                Debug.WriteLine(ex.Message);
            }
            UnitOfWork unitOfWork = new UnitOfWork(DBContext.Instance);
            List<Item> items = unitOfWork.ItemRepository.GetLimitedNumber(id, 9).ToList();
            return View(items);
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}