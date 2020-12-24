using EquipmentManagementSystem.Models;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.Extensions.Logging;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;

namespace EquipmentManagementSystem.Areas.Identity.Pages.Account
{
    [AllowAnonymous]
    public class LoginModel : PageModel
    {
        private readonly UserManager<User> _userManager;
        private readonly SignInManager<User> _signInManager;
        private readonly ILogger<LoginModel> _logger;

        public LoginModel(SignInManager<User> signInManager,
            ILogger<LoginModel> logger,
            UserManager<User> userManager)
        {
            _userManager = userManager;
            _signInManager = signInManager;
            _logger = logger;
        }

        [BindProperty]
        public InputModel Input { get; set; }

        public IList<AuthenticationScheme> ExternalLogins { get; set; }

        public string ReturnUrl { get; set; }

        [TempData]
        public string ErrorMessage { get; set; }

        public class InputModel
        {
            [Required(ErrorMessage = "请输入工号")]
            [Display(Name = "工号")]
            public string Number { get; set; }

            [Required(ErrorMessage = "请输入密码")]
            [DataType(DataType.Password)]
            [Display(Name = "密码")]
            public string Password { get; set; }

            [Display(Name = "记住我？")]
            public bool RememberMe { get; set; }
        }

        public async Task OnGetAsync(string returnUrl = null)
        {
            if (!string.IsNullOrEmpty(ErrorMessage))
            {
                ModelState.AddModelError(string.Empty, ErrorMessage);
            }

            returnUrl = returnUrl ?? Url.Content("~/");

            // Clear the existing external cookie to ensure a clean login process
            await HttpContext.SignOutAsync(IdentityConstants.ExternalScheme);

            ExternalLogins = (await _signInManager.GetExternalAuthenticationSchemesAsync()).ToList();

            ReturnUrl = returnUrl;
        }

        public async Task<IActionResult> OnPostAsync(string returnUrl = null)
        {
            returnUrl = returnUrl ?? Url.Content("~/");

            if (ModelState.IsValid)
            {
                // This doesn't count login failures towards account lockout
                // To enable password failures to trigger account lockout, set lockoutOnFailure: true
                var result = await _signInManager.PasswordSignInAsync(Input.Number, Input.Password, Input.RememberMe, lockoutOnFailure: false);
                if (result.Succeeded)
                {
                    var user = await _userManager.FindByNameAsync(Input.Number);
                    var claimsPrincipal = await _signInManager.CreateUserPrincipalAsync(user);

                    // 添加Group到Claim，用于在AuthorizationService模块中进行验证
                    if (claimsPrincipal.HasClaim(i => i.Type == "Group"))
                    {
                        var oldCalim = claimsPrincipal.FindFirst("Group");
                        await _userManager.ReplaceClaimAsync(user, oldCalim, new Claim("Group", user.Group));
                    }
                    else
                    {
                        await _userManager.AddClaimAsync(user, new Claim("Group", user.Group));
                    }

                    // 添加用户名到Claim，用于在AuthorizationService模块中进行验证
                    if (claimsPrincipal.HasClaim(i => i.Type == ClaimTypes.GivenName))
                    {
                        var oldCalim = claimsPrincipal.FindFirst(ClaimTypes.GivenName);
                        await _userManager.ReplaceClaimAsync(user, oldCalim, new Claim(ClaimTypes.GivenName, user.Name));
                    }
                    else
                    {
                        await _userManager.AddClaimAsync(user, new Claim(ClaimTypes.GivenName, user.Name));
                    }

                    await _signInManager.RefreshSignInAsync(user);

                    _logger.LogInformation("User logged in.");
                    return LocalRedirect(returnUrl);
                }

                //if (result.RequiresTwoFactor)
                //{
                //    return RedirectToPage("./LoginWith2fa", new { ReturnUrl = returnUrl, RememberMe = Input.RememberMe });
                //}
                //if (result.IsLockedOut)
                //{
                //    _logger.LogWarning("User account locked out.");
                //    return RedirectToPage("./Lockout");
                //}
                else
                {
                    ModelState.AddModelError(string.Empty, "账号或密码输入错误，请重新输入。");
                    return Page();
                }
            }

            // If we got this far, something failed, redisplay form
            return Page();
        }
    }
}
