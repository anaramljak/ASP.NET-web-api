using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Identity;
using ReviewerAPI.ViewsModels;

namespace ReviewerAPI.Controllers
{
    public class AuthController : Controller
    {
        private SignInManager<IdentityUser> _signInManager;
        private UserManager<IdentityUser> _userManager;
       
        public AuthController(SignInManager<IdentityUser> signInManager,
            UserManager<IdentityUser> userManager)
        {
            _signInManager = signInManager;
            _userManager = userManager;
           
        }

        [HttpGet]
        public IActionResult Login()
        {
            return View(new LoginViewModel());
        }

        [HttpPost]
        public async Task<IActionResult> Login(LoginViewModel vm)
        {
           var result = await _signInManager.PasswordSignInAsync(vm.UserName, vm.Password, false, false);
            if(!result.Succeeded)
            {
                ViewBag.message = "Pogresan mail ili sifra!!";  
                return View(vm);
            }
            var user = await _userManager.FindByNameAsync(vm.UserName);
        
            return RedirectToAction("Index", "Home");
            
        }

        [HttpGet]
        public async Task<IActionResult> Logout()
        {
            await _signInManager.SignOutAsync();

            return RedirectToAction("Index", "Home");
        }

        [HttpGet]
        public IActionResult Register()
        {
            return View(new RegisterViewModel());
        }

        [HttpPost]
        public async Task<IActionResult> Register(RegisterViewModel vm)
        {
            if (!ModelState.IsValid)
            { 
                return View(vm);
            }

            var user = new IdentityUser
            {
                UserName = vm.Email,
                Email = vm.Email
            };

            var password = vm.ConfirmPassword;
            var result = await _userManager.CreateAsync(user,password);

            if (result.Succeeded)
            {
                await _signInManager.SignInAsync(user, false);
                return RedirectToAction("Index", "Home");
            }
            
            return View(vm);
            
            
            
           
        }
    }
}

