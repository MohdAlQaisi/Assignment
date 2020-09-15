using Assignment.Interfaces;
using Assignment.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Assignment.Controllers
{
    public class AppsController : Controller
    {
        private readonly ISvcApplication _svcApplication;
        public AppsController(ISvcApplication svcApplication)
        {
            _svcApplication = svcApplication;
        }

        //View all applications
        [Authorize(Roles = "Admin, Operation")]
        public IActionResult Index()
        {
            return View(_svcApplication.GetAllApplication());
        }

        //Create new application
        [Authorize(Roles = "Admin")]
        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "Admin")]
        public IActionResult Create(Application app)
        {
            if (ModelState.IsValid)
            {
                _svcApplication.CreateApplication(app);
                return RedirectToAction(nameof(Index));
            }
            return View(app);
        }

        [Authorize(Roles = "Admin")]
        public IActionResult Edit(string id)
        {
            var app = _svcApplication.GetApplicationById(id);
            return View(app);
        }

        //edit application
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "Admin")]
        public IActionResult Edit(Application app)
        {
            if (ModelState.IsValid)
            {
                var result = _svcApplication.EditApplication(app);
                return RedirectToAction(nameof(Index));
            }
            return View(app);
        }
    }
}
