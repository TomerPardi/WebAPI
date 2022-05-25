using Microsoft.AspNetCore.Mvc;
using RatingApp.Models;
using RatingApp.Services;

namespace RatingApp.Controllers
{
    public class MyViewModel
    {

        public List<User> users { get; set; }
        public double AVG { get; set; }

        public MyViewModel(List<User> users, double v)
        {
            this.users = users;
            this.AVG = v;
        }
    }
    public class UsersController : Controller
    {
        private readonly IUserService service;


        public UsersController(IUserService s)
        {
            service = s;
        }

        // GET: Users
        public IActionResult Index()
        {
            MyViewModel model = new(service.GetAllUsers(), service.GetAVG());
            return View(model);
        }

        // GET: Users/Details/5
        public IActionResult Details(int id)
        {


            var user = service.GetUser(id);
            if (user == null)
            {
                return NotFound();
            }

            return View(user);
        }

        // GET: Users/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Users/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create([Bind("Id,Rating,Name,Opinion")] User user)
        {
            if (ModelState.IsValid)
            {
                service.CreateUser(user);
                return RedirectToAction(nameof(Index));
            }
            return View(user);
        }

        // GET: Users/Edit/5
        public IActionResult Edit(int id)
        {


            var user = service.GetUser(id);
            if (user == null)
            {
                return NotFound();
            }
            return View(user);
        }

        // POST: Users/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Edit(int id, [Bind("Id,Rating,Name,Opinion")] User user)
        {
            if (!UserExists(user.Id))
            {
                return NotFound();
            }

            user.Name = "def";
            user.Opinion = "def";

            service.EditUser(id, user.Rating);
            return RedirectToAction(nameof(Index));
        }

        // GET: Users/Delete/5
        public IActionResult Delete(int id)
        {


            var user = service.GetUser(id);
            if (user == null)
            {
                return NotFound();
            }

            return View(user);
        }

        // POST: Users/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public IActionResult DeleteConfirmed(int id)
        {
            service.DeleteUser(id);
            return RedirectToAction(nameof(Index));
        }

        private bool UserExists(int id)
        {
            return service.GetAllUsers().All(e => e.Id == id);
        }

        [HttpPost, ActionName("Search")]
        public IActionResult Search(string query)
        {
            if (query == null)
            {
                return View(service.GetAllUsers());
            }
            List<User> list = service.GetAllUsers().Where(User => User.Opinion.Contains(query) ||
                 User.Name.Contains(query)).ToList();
            return View(list);

        }

        public IActionResult Search()
        {
            return View(service.GetAllUsers());


        }
    }
}
