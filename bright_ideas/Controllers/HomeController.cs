using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using System.Linq;
using Microsoft.AspNetCore.Mvc;
using bright_ideas.Models;
using Microsoft.AspNetCore.Http;
using System.Diagnostics;
using Microsoft.AspNetCore.Identity;

namespace bright_ideas.Controllers
{
    public class HomeController : Controller
    {
        private MyContext dbContext;
        public HomeController(MyContext context)
        {
            dbContext = context;
        }
/////////////////////////////////////////////////////////////////////////////////////////
        [HttpGet]
        [Route("")]
        public IActionResult LoginReg()
        {
            return View();
        }
////////////////////////////////////////////////////////////////////////////////////////////
        
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
///////////////////////////////////////////////////////////////////////////////////////////////
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
/////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
        [HttpGet]
        [Route("logout")]
        public IActionResult Logout()
        {
            HttpContext.Session.Clear();
            return RedirectToAction("LoginReg");
        }
/////////////////////////////////////////////////////////////////////////////////////////////
        [HttpGet]
        [Route("dashboard")]
        public IActionResult Dashboard()
        {
            if(HttpContext.Session.GetInt32("UserId")==null)
            {
                return RedirectToAction("LoginReg");
            }
            User LoggedUser = dbContext.Users.SingleOrDefault(user => user.UserId == HttpContext.Session.GetInt32("UserId"));
            List<Idea> AllIdeas = dbContext.Ideas
                .Include(u => u.User)
                .Include(l => l.likeThis)
                .ThenInclude(u => u.User)
                .ToList();
            List<Like> UserActions = dbContext.Actions.Where(Like => Like.User.Equals(LoggedUser)).ToList();
            
            ViewBag.UserId = HttpContext.Session.GetInt32("UserId");
            ViewBag.LoggedUser = LoggedUser;
            ViewBag.AllIdeas = AllIdeas;
            ViewBag.UserActions = UserActions;
            
            return View();
        }
////////////////////////////////////////////////////////////////////////////////////
        [HttpPost("CreateIdea")]
        public IActionResult CreateIdea(Idea newIdea)
        {
            if(HttpContext.Session.GetInt32("UserId") == null)
            {
                return RedirectToAction("LoginReg");
            }

            newIdea.UserId = (int)HttpContext.Session.GetInt32("UserId");
    
            if (ModelState.IsValid)
            {
                dbContext.Add(newIdea);
                dbContext.SaveChanges();

                return RedirectToAction("Dashboard");
            }
            else
            {
                User LoggedUser = dbContext.Users.SingleOrDefault(user => user.UserId == HttpContext.Session.GetInt32("UserId"));
                List<Idea> AllIdeas = dbContext.Ideas
                    .Include(u => u.User)
                    .Include(l => l.likeThis)
                    .ThenInclude(u => u.User)
                    .ToList();
                List<Like> UserActions = dbContext.Actions.Where(Like => Like.User.Equals(LoggedUser)).ToList();
                
                ViewBag.UserId = HttpContext.Session.GetInt32("UserId");
                ViewBag.LoggedUser = LoggedUser;
                ViewBag.AllIdeas = AllIdeas;
                ViewBag.UserActions = UserActions;

                return View("Dashboard", newIdea);
            }
        }
////////////////////////////////////////////////////////////////////////////////////////////////
        [HttpGet]
        [Route("delete/{IdeaId}")]
        public IActionResult Delete(int IdeaId)
        {
            if(HttpContext.Session.GetInt32("UserId") == null)
            {
                return RedirectToAction("LoginReg");
            }
            Idea ThisIdea = dbContext.Ideas
                .SingleOrDefault(idea => idea.IdeaId == IdeaId);
            dbContext.Ideas.Remove(ThisIdea);
            dbContext.SaveChanges();

            return RedirectToAction("Dashboard");
        }
////////////////////////////////////////////////////////////////////////////////////////////
        [HttpGet]
        [Route("viewidea/{IdeaId}")]
        public IActionResult ViewIdea(int IdeaId)
        {
            if(HttpContext.Session.GetInt32("UserId") == null)
            {
                return RedirectToAction("LoginReg");
            }
            Idea ThisIdea = dbContext.Ideas
                .Include(a => a.User)
                .Include(idea => idea.likeThis)
                .ThenInclude(guest => guest.User)
                .SingleOrDefault(idea => idea.IdeaId == IdeaId);
            
            return View("ViewIdea", ThisIdea);
        }
//////////////////////////////////////////////////////////////////////////////////////
        [HttpGet]
        [Route("like/{IdeaId}")]
        public IActionResult Like(int IdeaId)
        {
            if(HttpContext.Session.GetInt32("UserId") == null)
            {
                return RedirectToAction("LoginReg");
            }
            User LoggedUser = dbContext.Users.SingleOrDefault(user => user.UserId == HttpContext.Session.GetInt32("UserId"));
            Idea ThisIdea = dbContext.Ideas
                .Include(idea => idea.likeThis)
                .ThenInclude(liker => liker.User)
                .SingleOrDefault(idea => idea.IdeaId == IdeaId);
            Like MakeLike = new Like
            {
                UserId = LoggedUser.UserId,
                User = LoggedUser,
                IdeaId = ThisIdea.IdeaId,
                Idea = ThisIdea
            };
            ThisIdea.likeThis.Add(MakeLike);
            dbContext.SaveChanges();

            return RedirectToAction("Dashboard");
        }
////////////////////////////////////////////////////////////////////////////////////////////////////////////////
        [HttpGet]
        [Route("unlike/{IdeaId}")]
        public IActionResult Unlike(int IdeaId)
        {
            if(HttpContext.Session.GetInt32("UserId") == null)
            {
                return RedirectToAction("LoginReg");
            }
            User LoggedUser = dbContext.Users.SingleOrDefault(user => user.UserId == HttpContext.Session.GetInt32("UserId"));
            Like ThisLike = dbContext.Actions.SingleOrDefault(like => like.UserId == HttpContext.Session.GetInt32("UserId") && like.IdeaId == IdeaId);
            Idea ThisIdea = dbContext.Ideas
                .Include(idea => idea.likeThis)
                .ThenInclude(guest => guest.User)
                .SingleOrDefault(bright_ideas => bright_ideas.IdeaId == IdeaId);
            ThisIdea.likeThis.Remove(ThisLike);
            dbContext.SaveChanges();

            return RedirectToAction("Dashboard");
        }
////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
        [HttpGet]
        [Route("viewuser/{UserId}")]
        public IActionResult ViewUser(int UserId)
        {
            if(HttpContext.Session.GetInt32("UserId") == null)
            {
                return RedirectToAction("LoginReg");
            }
            User ThisUser = dbContext.Users
                .SingleOrDefault(idea => idea.UserId == UserId);

            return View("ViewUser", ThisUser);
        }
//////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
        public IActionResult Privacy()
        {
            return View();
        }
/////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
