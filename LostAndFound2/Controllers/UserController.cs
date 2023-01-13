using LostAndFound2.Data;
using LostAndFound2.Data.UnitOfWork;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Xml.Linq;

namespace LostAndFound2.Controllers
{
    public class UserController : Controller
    {
        [HttpGet]
        public IActionResult Login()
        {
            return View();
        }
        [HttpPost]
        public IActionResult Login(FormCollection form)
        {
            UnitOfWork unitOfWork = new UnitOfWork(DBContext.Instance);
            if(!unitOfWork.UserRepository.Find(u => u.Name == form["Name"]).Any())
            {
                HttpContext.Session.SetString("danger", "No user exists with this name");
                return RedirectToAction("Login", "User");
            }
            else
            {
                if(!(unitOfWork.UserRepository.Find(u => u.Name == form["Name"]).First().Password == form["Password"]))
                {
                    HttpContext.Session.SetString("danger", "Wrong credentials");
                    return RedirectToAction("Login", "User");
                }
                else
                {
                    HttpContext.Session.SetString("current user", unitOfWork.UserRepository.Find(u => u.Name == form["Name"]).First().Id.ToString());
                    return RedirectToAction("Index", "Home");
                }
            }
        }
        [HttpGet]
        public IActionResult SignUp() { return View(); }
        [HttpPost]
        public IActionResult SignUp(FormCollection form)
        {
            UnitOfWork unitOfWork = new UnitOfWork(DBContext.Instance);
            if(unitOfWork.UserRepository.Find(u => (u.Name == form["Name"])).Any()){
                HttpContext.Session.SetString("danger", "UserName already exists");
                return RedirectToAction("SignUp", "User");
            }
            if (form["Password"] != form["Repeted_Password"]){
                HttpContext.Session.SetString("danger", "Password and repeted password does not match");
                return RedirectToAction("SignUp", "User");
            }
            if (form["Name"]== "" || form["Password"] == "" || form["Phone"] == ""){
                HttpContext.Session.SetString("danger", "Please fill all fields");
                return RedirectToAction("SignUp", "User");
            }
            unitOfWork.UserRepository.Add(new Models.User(form["Name"], form["Password"], long.Parse(form["Phone"])));
            return RedirectToAction("Index" , "Home");
        }
        [HttpGet]
        public IActionResult LogOut()
        {

        }
    }
}
