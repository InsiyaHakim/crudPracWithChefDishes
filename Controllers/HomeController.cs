using Microsoft.AspNetCore.Mvc;
using CRUDpractice.Models;
using System;
using Microsoft.AspNetCore.Session;
using System.Linq;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Http;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;

namespace CRUDpractice.Controllers
{
    public class HomeController : Controller
    {
        private MyContext dbContext;
     
        // here we can "inject" our context service into the constructor
        public HomeController(MyContext context)
        {
            dbContext = context;
        }
        
        [HttpGet("")]
        public IActionResult index()
        {
            return View();
        }

        [HttpPost("/create")]
        public IActionResult create(User user)
        {
            if(ModelState.IsValid)
            {
                if(dbContext.user.Any(u_email => u_email.email == user.email))
                {
                    ModelState.AddModelError("email","Email already exist!!!");
                    return View("index");
                }
                PasswordHasher<User> Hasher = new PasswordHasher<User>();
                user.password = Hasher.HashPassword(user, user.password);
                Console.WriteLine(user.password);
                dbContext.Add(user);
                dbContext.SaveChanges();
                return View("login");
            }
            return View("index");
        }

        [HttpGet("/login_page")]
        public IActionResult login_page()
        {
            return View("login");
        }

        

        [HttpPost("/login")]
        public IActionResult login(Login user_login)
        {
            if(ModelState.IsValid)
            {
                var check_user = dbContext.user.FirstOrDefault(u_email=> u_email.email == user_login.email);
                if(check_user == null)
                {
                    ModelState.AddModelError("email","Email is not valid");
                    return View("login");
                }
                var hasher = new PasswordHasher<Login>();
                var result = hasher.VerifyHashedPassword(user_login, check_user.password, user_login.password);
                if(result == 0)
                {
                    ModelState.AddModelError("password","password is not valid");
                    return View("login");
                }
                HttpContext.Session.SetInt32("user_id",check_user.user_id);
                return RedirectToAction("allChefs");
            }
            return View("login");
        }

        [HttpGet("logout")]
        public IActionResult logout()
        {
            HttpContext.Session.Clear();
            return RedirectToAction("index");
        }

        [HttpPost("/addChef")]
        public IActionResult chefAdd(Chef chef)
        {
            if(ModelState.IsValid)
            {
                Console.WriteLine("everything is fine");
                dbContext.Add(chef);
                dbContext.SaveChanges();
                Console.WriteLine("chef is added");
                return RedirectToAction("allChefs");
            }
            return View("chefs");
        }

        [HttpGet("/addChef")]
        public IActionResult getAddChef()
        {
            return View("chefs");
        }

        [HttpGet("/allChefs")]
        public IActionResult allChefs()
        {
            List<Chef> allChef = dbContext.Chef.Include(dish => dish.Dishes).ToList();
            return View("allChef",allChef);
        }

        [HttpGet("/dishes")]
        public IActionResult Dishes()
        {
           List<Chef> chef = dbContext.Chef.ToList();
           ViewBag.chef = chef;
           return View("dishes");
        }


        [HttpPost("/addDishes")]
        public IActionResult AddDishes(Dishes dishes)
        {
            if(ModelState.IsValid)
            {
                dbContext.Add(dishes);
                dbContext.SaveChanges();
                Console.WriteLine("form was submitted");
                return RedirectToAction("AllDishes");
            }
            List<Chef> chef = dbContext.Chef.ToList();
            ViewBag.chef = chef;
            return View("dishes");
        }

        [HttpGet("/allDishes")]
        public IActionResult AllDishes()
        {
            List<Dishes> dishes = dbContext.Dishes.Include(dish => dish.Chef).ToList();
            return View("allDishes",dishes);
        }

        [HttpGet("/editChef/{chef_id}")]
        public IActionResult editChef(int chef_id)
        {
            Chef chef = dbContext.Chef.FirstOrDefault(editchef => editchef.chef_id == chef_id);
            if(chef == null)
                return RedirectToAction("/allChefs");
            
            return View("editChef",chef);
        }

        [HttpPost("/editChef/updateChef/{chef_id}")]
        public IActionResult updateChef(Chef updatedChef,int chef_id)
        {
            if(ModelState.IsValid)
            {
                Chef chef = dbContext.Chef.FirstOrDefault(updateChef => updateChef.chef_id == chef_id);
                if(chef == null)
                    return RedirectToAction("allChefs");
                chef.f_name = updatedChef.f_name;
                chef.l_name = updatedChef.l_name;
                chef.age = updatedChef.age;
                dbContext.SaveChanges();
                return RedirectToAction("allChefs"); 
            }
            return RedirectToAction("allChefs"); 
        }

        [HttpGet("/deleteDish/{dish_id}")]
        public IActionResult deleteDish(int dish_id)
        {
            Dishes Dish = dbContext.Dishes.FirstOrDefault(dish => dish.dish_id == dish_id);
            if(Dish == null)
                return RedirectToAction("AllDishes");
            dbContext.Dishes.Remove(Dish);
            dbContext.SaveChanges();
            return RedirectToAction("AllDishes");
        }

        [HttpGet("/editDish/{dish_id}")]
        public IActionResult editDish(int dish_id)
        {
            Dishes Dish = dbContext.Dishes.FirstOrDefault(dish => dish.dish_id == dish_id);
            List<Chef> chef = dbContext.Chef.ToList();
            ViewBag.chef = chef;
            return View("editDish",Dish);
        }

        [HttpPost("/editDish/UpdateDish/{dish_id}")]
        public IActionResult updateDish(Dishes dish,int dish_id)
        {
            if(ModelState.IsValid)
            {
                Dishes Dish = dbContext.Dishes.FirstOrDefault(dishUpdate => dishUpdate.dish_id == dish_id);
                if(Dish == null)
                    return RedirectToAction("AllDishes");
                Dish.name = dish.name;
                Dish.calories = dish.calories;
                Dish.chef_id = dish.chef_id;
                Dish.description = dish.description;
                dbContext.SaveChanges();
                return RedirectToAction("AllDishes");
            }
            return RedirectToAction("AllDishes");
        }

        [HttpGet("delete/{chef_id}")]
        public IActionResult deleteChef(int chef_id)
        {
            Chef chef = dbContext.Chef.FirstOrDefault(chefDelete => chefDelete.chef_id == chef_id);
            if(chef == null)
                return RedirectToAction("allChefs");
            dbContext.Chef.Remove(chef);
            dbContext.SaveChanges();
            return RedirectToAction("allChefs");
        }
    }
}
