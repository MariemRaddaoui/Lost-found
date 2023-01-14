using LostAndFound2.Data;
using LostAndFound2.Data.UnitOfWork;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;
using System.Xml.Linq;

namespace LostAndFound2.Controllers
{
    public class UserController : Controller
    {
        UnitOfWork unitOfWork = new UnitOfWork(DBContext.Instance);
        [HttpGet]
        public IActionResult Login()
        {
            return View();
        }
        [HttpPost]
        public IActionResult Login(IFormCollection form)
        {
                if (unitOfWork.UserRepository.Find(u => u.Name == form["Name"].ToString()).Count() == 0)
                {
                    HttpContext.Session.SetString("danger", "No user exists with this name");
                    return RedirectToAction("Login", "User");
                }
                else
                {
                    if (!(unitOfWork.UserRepository.Find(u => u.Name == form["Name"].ToString() ).First().Password == form["Password"].ToString() ))
                    {
                        HttpContext.Session.SetString("danger", "Wrong credentials");
                        return RedirectToAction("Login", "User");
                    }
                    else
                    {
                        HttpContext.Session.SetString("id", unitOfWork.UserRepository.Find(u => u.Name == form["Name"].ToString()).First().Id.ToString());
                        return RedirectToAction("Index", "Home");
                    }
                }
            
        }
        [HttpGet]
        public IActionResult SignUp() { return View(); }
        [HttpPost]
        public IActionResult SignUp(IFormCollection form)
        {
                    if (unitOfWork.UserRepository.Find(u => u.Name == (form["Name"].ToString())).FirstOrDefault() != null)
                    {
                ViewBag.danger = "UserName already exists";
                Debug.WriteLine(1);
                        return RedirectToAction("SignUp", "User");
                    }
                    if (form["Password"].ToString() != form["Repeted_Password"].ToString())
                    {
                ViewBag.danger = "Password and repeted password does not match";
                Debug.WriteLine(2);
                return RedirectToAction("SignUp", "User");
                    }
                    if (form["Name"].ToString() == "" || form["Password"].ToString() == "" || form["Phone"].ToString() == "")
                    {
                ViewBag.danger = "Please fill all fields";
                Debug.WriteLine(3);
                return RedirectToAction("SignUp", "User");
                    }
                    unitOfWork.UserRepository.Add(new Models.User(form["Name"].ToString(), form["Password"].ToString(), long.Parse(form["Phone"])));
                    unitOfWork.Complete();
                    return RedirectToAction("Index", "Home");
                
        }
        [HttpGet]
        public IActionResult LogOut()
        {
            HttpContext.Session.SetString("id" , "");
            return RedirectToAction("LogIn", "User");
        }
    }
}
