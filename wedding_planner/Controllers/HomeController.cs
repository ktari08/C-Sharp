using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using System.Linq;
using Microsoft.AspNetCore.Mvc;
using wedding_planner.Models;
using Microsoft.AspNetCore.Http;
using System.Diagnostics;
using Microsoft.AspNetCore.Identity;


namespace wedding_planner.Controllers
{
    public class HomeController : Controller
    {
        private MyContext dbContext;
        public HomeController(MyContext context)
        {
            dbContext = context;
        }

        // USER LOGIN/REG // USER LOGIN/REG // USER LOGIN/REG // USER LOGIN/REG //
////////////////////////////////////////////////////////////////////////////////////////////
        [HttpGet]
        [Route("/")]
        public IActionResult LoginReg()
        {
            return View("LoginReg");
        }
//////////////////////////////////////////////////////////////////////////////////////////////////
        [HttpPost("Register")]
        public IActionResult Register(User newUser)
        {
            if (ModelState.IsValid)
            {
                if (dbContext.Users.Any(u => u.email == newUser.email))
                {
                    ModelState.AddModelError("Email", "Email is already in use!");
                    return View ("LoginReg");
                }
                PasswordHasher<User> hasher = new PasswordHasher<User>();
                newUser.password = hasher.HashPassword(newUser, newUser.password);

                dbContext.Add(newUser);
                dbContext.SaveChanges();

                HttpContext.Session.SetInt32("UserId", newUser.UserId);

                return RedirectToAction("Dashboard");
            }
            else
            {
                return View("LoginReg");
            }
        }
////////////////////////////////////////////////////////////////////////////////////////////
        [HttpPost]
        public IActionResult Login(string email, string password)
        {
            User ValidateEmail = dbContext.Users.SingleOrDefault(user => user.email == email);
            if (ValidateEmail != null)
            {
                var Hasher = new PasswordHasher<User>();
                if(0 != Hasher.VerifyHashedPassword(ValidateEmail, ValidateEmail.password, password))
                {
                   HttpContext.Session.SetInt32("UserId", ValidateEmail.UserId);

                   return RedirectToAction("Dashboard"); 
                }
                else
                {
                    ViewBag.Error = "Incorrect password or email";

                    return View("LoginReg");
                }
            }
            else
            {
                ViewBag.Error = "Incorrect password or email";

                return View("LoginReg");
            }
        }
/////////////////////////////////////////////////////////////////////////////////////////////// 
        [HttpGet]
        [Route("logout")]
        public IActionResult Logout()
        {
            HttpContext.Session.Clear();
            return RedirectToAction("LoginReg");
        }

        // WEDDING CONTROLLER // WEDDING CONTROLLER // WEDDING CONTROLLER // WEDDING CONTROLLER //
///////////////////////////////////////////////////////////////////////////////////////////////////
        [HttpGet]
        [Route("new")]
        public IActionResult WeddingForm()
        {
            if(HttpContext.Session.GetInt32("UserId")==null)
            {
                return RedirectToAction("LoginReg");
            }
            ViewBag.UserId = HttpContext.Session.GetInt32("UserId");
            
            return View("New");
        }
//////////////////////////////////////////////////////////////////////////////// 
        [HttpGet]
        [Route("dashboard")]
        public IActionResult Dashboard()
        {
            if(HttpContext.Session.GetInt32("UserId")==null)
            {
                return RedirectToAction("LoginReg");
            }
            User LoggedUser = dbContext.Users.SingleOrDefault(user => user.UserId == HttpContext.Session.GetInt32("UserId"));
            List<Wedding> AllWeddings = dbContext.Weddings
                .Include(wedding => wedding.Attending)
                .ThenInclude(attendant => attendant.User)
                .ToList();
            List<Rsvp> UserActions = dbContext.Actions.Where(Rsvp => Rsvp.User.Equals(LoggedUser)).ToList();

            ViewBag.UserId = HttpContext.Session.GetInt32("UserId");
            ViewBag.LoggedUser = LoggedUser;
            ViewBag.AllWeddings = AllWeddings;
            ViewBag.UserActions = UserActions;
            
            return View();
        }
/////////////////////////////////////////////////////////////////////////////////////////////////////
        [HttpPost("CreateWedding")]
        public IActionResult CreateWedding(Wedding newWedding)
        {
            if(HttpContext.Session.GetInt32("UserId")==null)
            {
                return RedirectToAction("LoginReg");
            }
            if (newWedding.WeddingDate < DateTime.Now || newWedding.WeddingDate == null)
            {
                ModelState.AddModelError("WeddingDate", "Wedding date must be in the future");
            }
            if (ModelState.IsValid)
            {
                dbContext.Add(newWedding);
                dbContext.SaveChanges();

                return RedirectToAction("Dashboard");
            }
            else
            {
                return View("New", newWedding);
            }
        }
////////////////////////////////////////////////////////////////////////////////////////////////////// 
        [HttpGet]
        [Route("rsvp/{WeddingId}")]
        public IActionResult RSVP(int WeddingId)
        {
            if(HttpContext.Session.GetInt32("UserId")==null)
            {
                return RedirectToAction("LoginReg");
            }
            User LoggedUser = dbContext.Users.SingleOrDefault(user => user.UserId == HttpContext.Session.GetInt32("UserId"));
            Wedding ThisWedding = dbContext.Weddings
                .Include(wedding => wedding.Attending)
                .ThenInclude(attendant => attendant.User)
                .SingleOrDefault(wedding => wedding.WeddingId == WeddingId);
            Rsvp MakeRsvp = new Rsvp
            {
                UserId = LoggedUser.UserId,
                User = LoggedUser,
                WeddingId = ThisWedding.WeddingId,
                Wedding = ThisWedding
            };
            ThisWedding.Attending.Add(MakeRsvp);
            dbContext.SaveChanges();

            return RedirectToAction("Dashboard");
        }
////////////////////////////////////////////////////////////////////////////////////////////////// 
        [HttpGet]
        [Route("unrsvp/{WeddingId}")]
        public IActionResult UnRsvp(int WeddingId)
        {
            if(HttpContext.Session.GetInt32("UserId") == null)
            {
                return RedirectToAction("LoginReg");
            }
            User LoggedUser = dbContext.Users.SingleOrDefault(user => user.UserId == HttpContext.Session.GetInt32("UserId"));
            Rsvp ThisRsvp = dbContext.Actions.SingleOrDefault(rsvp => rsvp.UserId == HttpContext.Session.GetInt32("UserId") && rsvp.WeddingId == WeddingId);
            Wedding ThisWedding = dbContext.Weddings
                .Include(wedding => wedding.Attending)
                .ThenInclude(guest => guest.User)
                .SingleOrDefault(wedding_planner => wedding_planner.WeddingId == WeddingId);
            ThisWedding.Attending.Remove(ThisRsvp);
            dbContext.SaveChanges();

            return RedirectToAction("Dashboard");
        }
///////////////////////////////////////////////////////////////////////////////////////
        [HttpGet]
        [Route("delete/{WeddingId}")]
        public IActionResult Delete(int WeddingId)
        {
            if(HttpContext.Session.GetInt32("UserId") == null)
            {
                return RedirectToAction("LoginReg");
            }
            Wedding ThisWedding = dbContext.Weddings
                .SingleOrDefault(wedding => wedding.WeddingId == WeddingId);
            dbContext.Weddings.Remove(ThisWedding);
            dbContext.SaveChanges();

            return RedirectToAction("Dashboard");
        }
/////////////////////////////////////////////////////////////////////////////////////////////////// 
        [HttpGet]
        [Route("viewwedding/{WeddingId}")]
        public IActionResult ViewWedding(int WeddingId)
        {
            if(HttpContext.Session.GetInt32("UserId") == null)
            {
                return RedirectToAction("LoginReg");
            }
            Wedding ThisWedding = dbContext.Weddings
                .Include(wedding => wedding.Attending)
                .ThenInclude(guest => guest.User)
                .SingleOrDefault(wedding => wedding.WeddingId == WeddingId);
    
            return View("View", ThisWedding);
        }
/////////////////////////////////////////////////////////////////////////////////////////////////////
        public IActionResult Privacy()
        {
            return View();
        }
/////////////////////////////////////////////////////////////////////////////////////////////////////
        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
