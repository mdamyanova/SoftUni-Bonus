namespace SoftUni.SimpleApp.Web.Controllers
{
    using Microsoft.AspNetCore.Authorization;
    using Microsoft.AspNetCore.Identity;
    using Microsoft.AspNetCore.Mvc;
    using Models;
    using Services.Contracts;
    using System.Threading.Tasks;

    public class UsersController : Controller
    {
        private readonly IUserService users;
        private readonly UserManager<IdentityUser> userManager;

        public UsersController(IUserService users, UserManager<IdentityUser> userManager)
        {
            this.users = users;
            this.userManager = userManager;
        }

        [Authorize]
        public IActionResult All()
        {
            var model = this.users.All();

            return this.View(model);
        }

        [Authorize]
        public async Task<IActionResult> Profile(string username)
        {
            var user = await this.userManager.FindByNameAsync(username);

            if (user == null)
            {
                return this.NotFound();
            }
            var roles = await this.userManager.GetRolesAsync(user);
            
            var model = await this.users.ProfileAsync(user.Id);

            return View(model);
        }

        [Authorize]
        public async Task<IActionResult> Journals(string username)
        {
            var user = await this.userManager.FindByNameAsync(username);

            if (user == null)
            {
                return this.NotFound();
            }

            var model = await this.users.JournalsAsync(user.Id);

            return View(model);
        }

        [HttpGet]
        public IActionResult Edit(string username)
        {
            var task = this.userManager.FindByNameAsync(username);
            var user = task.Result;

            if (user == null)
            {
                return this.NotFound();
            }

            if (User.Identity.Name != user.UserName &&
                !this.User.IsInRole("Administrator"))
            {
                // user doesn't have the rights
                return RedirectToAction("All");
            }

            return this.View(new UserFormModel
            {
                Username = user.UserName
            });
        }

        [HttpPost]
        public IActionResult Edit(string username, UserFormModel model)
        {
            if (!this.ModelState.IsValid)
            {
                return this.View(model);
            }

            var user = this.userManager.FindByNameAsync(username);
            var userExists = this.users.Exists(user.Result.Id);

            if (!userExists)
            {
                return this.NotFound();
            }

            this.users.Edit(model.Username, model.Name);

            return RedirectToAction("All");
        }

        [Authorize]
        public async Task<IActionResult> Delete(string username)
        {
            var user = await this.userManager.FindByNameAsync(username);

            if (user == null)
            {
                return this.NotFound();
            }

            // we won't delete the admin
            if (user.UserName == "Administrator")
            {
                return this.RedirectToAction(nameof(All));
            }

            if (User.Identity.Name != user.UserName
                && User.Identity.Name != "Administrator")
            {
                // user doesn't have the rights
                return this.RedirectToAction(nameof(this.All));
            }

            return this.View(model: username);
        }

        [Authorize]
        [HttpPost, ActionName("Delete")]
        public async Task<IActionResult> ConfirmDelete(string username)
        {
            var user = await this.userManager.FindByNameAsync(username);
            var userExists = this.users.Exists(user.Id);

            if (!userExists)
            {
                return this.NotFound();
            }

            var result = this.users.Remove(user.Id);

            return this.RedirectToAction(nameof(this.All));
        }
    }
}