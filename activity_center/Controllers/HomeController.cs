using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using System.Linq;
using Microsoft.AspNetCore.Mvc;
using activity_center.Models;
using Microsoft.AspNetCore.Http;
using System.Diagnostics;
using Microsoft.AspNetCore.Identity;

namespace activity_center.Controllers
{
    public class HomeController : Controller
    {
        private MyContext dbContext;
        public HomeController(MyContext context)
        {
            dbContext = context;
        }

        // USER LOGIN/REG // USER LOGIN/REG // USER LOGIN/REG // USER LOGIN/REG //
///////////////////////////////////////////////////////////////////////////////////
        [HttpGet]
        [Route("")]
        public IActionResult LoginReg()
        {
            return View("LoginReg");
        }
///////////////////////////////////////////////////////////////////////////////////
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
//////////////////////////////////////////////////////////////////////////////////////////////////// 
        [HttpPost]
        public IActionResult Login(Login login)
        {
            if (!ModelState.IsValid)
            {
                return View("LoginReg");
            }
            User ValidateEmail = dbContext.Users.SingleOrDefault(user => user.email == login.loginEmail);
            if (ValidateEmail != null)
            {
                var Hasher = new PasswordHasher<User>();
                if(0 != Hasher.VerifyHashedPassword(ValidateEmail, ValidateEmail.password, login.loginPassword))
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
//////////////////////////////////////////////////////////////////////////////////////////////////
        [HttpGet]
        [Route("logout")]
        public IActionResult Logout()
        {
            HttpContext.Session.Clear();
            return RedirectToAction("LoginReg");
        }

        // Activity CONTROLLER // Activity CONTROLLER // Activity CONTROLLER // Activity CONTROLLER //
//////////////////////////////////////////////////////////////////////////////////////////////////////////////////
        [HttpGet]
        [Route("new")]
        public IActionResult ActivityForm()
        {
            if(HttpContext.Session.GetInt32("UserId")==null)
            {
                return RedirectToAction("LoginReg");
            }
            ViewBag.UserId = HttpContext.Session.GetInt32("UserId");
            
            return View("New");
        }
///////////////////////////////////////////////////////////////////////////////////////////////////
        [HttpGet]
        [Route("dashboard")]
        public IActionResult Dashboard()
        {
            if(HttpContext.Session.GetInt32("UserId")==null)
            {
                return RedirectToAction("LoginReg");
            }
            User LoggedUser = dbContext.Users.SingleOrDefault(user => user.UserId == HttpContext.Session.GetInt32("UserId"));
            List<TheActivity> AllActivities = dbContext.Activities
                .Include(u => u.User)
                .Include(a => a.Attending)
                .ThenInclude(u =>u.User)
                .ToList();
            List<Rsvp> UserActions = dbContext.Actions.Where(Rsvp => Rsvp.User.Equals(LoggedUser)).ToList();

            ViewBag.UserId = HttpContext.Session.GetInt32("UserId");
            ViewBag.LoggedUser = LoggedUser;
            ViewBag.AllActivities = AllActivities;
            ViewBag.UserActions = UserActions;

            return View();
        }
///////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
        [HttpPost("CreateActivity")]
        public IActionResult CreateActivity(TheActivity newActivity)
        {
            if(HttpContext.Session.GetInt32("UserId") == null)
            {
                return RedirectToAction("LoginReg");
            }

            newActivity.UserId = (int)HttpContext.Session.GetInt32("UserId");

            if (ModelState.IsValid)
            {
                dbContext.Add(newActivity);
                dbContext.SaveChanges();

                return RedirectToAction("Dashboard");
            }
            else
            {
                return View("New", newActivity);
            }
        }
/////////////////////////////////////////////////////////////////////////////////////////////////////
        [HttpGet]
        [Route("delete/{TheActivityId}")]

        public IActionResult Delete(int TheActivityId)
        {
            if(HttpContext.Session.GetInt32("UserId") == null)
            {
                return RedirectToAction("LoginReg");
            }
            TheActivity ThisActivity = dbContext.Activities
                .SingleOrDefault(activity => activity.TheActivityId == TheActivityId);
            dbContext.Activities.Remove(ThisActivity);
            dbContext.SaveChanges();

            return RedirectToAction("Dashboard");
        }
/////////////////////////////////////////////////////////////////////////////////////////////////////////
        [HttpGet]
        [Route("viewactivity/{TheActivityId}")]
        public IActionResult ViewActivity(int TheActivityId)
        {
            if(HttpContext.Session.GetInt32("UserId") == null)
            {
                return RedirectToAction("LoginReg");
            }
            TheActivity ThisActivity = dbContext.Activities
                .Include(a => a.User)
                .Include(activity => activity.Attending)
                .ThenInclude(guest => guest.User)
                .SingleOrDefault(activity => activity.TheActivityId == TheActivityId);

            return View("View", ThisActivity);
        }
//////////////////////////////////////////////////////////////////////////////////////////////////////
        [HttpGet]
        [Route("join/{TheActivityId}")]
        public IActionResult Join(int TheActivityId)
        {
            if(HttpContext.Session.GetInt32("UserId") == null)
            {
                return RedirectToAction("LoginReg");
            }
            User LoggedUser = dbContext.Users.SingleOrDefault(user => user.UserId == HttpContext.Session.GetInt32("UserId"));
            TheActivity ThisActivity = dbContext.Activities
                .Include(activity => activity.Attending)
                .ThenInclude(attendant => attendant.User)
                .SingleOrDefault(activity => activity.TheActivityId == TheActivityId);
            Rsvp MakeRsvp = new Rsvp
            {
                UserId = LoggedUser.UserId,
                User = LoggedUser,
                TheActivityId = ThisActivity.TheActivityId,
                TheActivity = ThisActivity
            };
            ThisActivity.Attending.Add(MakeRsvp);
            dbContext.SaveChanges();

            return RedirectToAction("Dashboard");
        }
//////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
        [HttpGet]
        [Route("leave/{TheActivityId}")]
        public IActionResult Leave(int TheActivityId)
        {
            if(HttpContext.Session.GetInt32("UserId") == null)
            {
                return RedirectToAction("LoginReg");
            }
            User LoggedUser = dbContext.Users.SingleOrDefault(user => user.UserId == HttpContext.Session.GetInt32("UserId"));
            Rsvp ThisRsvp = dbContext.Actions.SingleOrDefault(rsvp => rsvp.UserId == HttpContext.Session.GetInt32("UserId") && rsvp.TheActivityId == TheActivityId);
            TheActivity ThisActivity = dbContext.Activities
                .Include(activity => activity.Attending)
                .ThenInclude(guest => guest.User)
                .SingleOrDefault(activity_center => activity_center.TheActivityId == TheActivityId);
            ThisActivity.Attending.Remove(ThisRsvp);
            dbContext.SaveChanges();

            return RedirectToAction("Dashboard");
        }
////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
        public IActionResult Privacy()
        {
            return View();
        }
//////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
