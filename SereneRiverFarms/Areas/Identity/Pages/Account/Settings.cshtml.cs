using System;
using System.Linq;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text.Encodings.Web;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.UI.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.Extensions.Logging;
using SereneRiverFarms.Areas.Identity.Data;


namespace SereneRiverFarms.Areas.Identity.Pages.Account
{
    [Authorize]  //Restricts access to this page to only signed in users.
    public class SettingsModel : PageModel
    {

        private readonly SignInManager<SereneRiverFarmsUser> _signInManager;
        private readonly UserManager<SereneRiverFarmsUser> _userManager;
        private readonly ILogger<SettingsModel> _logger;

        public SettingsModel(
            UserManager<SereneRiverFarmsUser> userManager,
            SignInManager<SereneRiverFarmsUser> signInManager,
            ILogger<SettingsModel> logger)
        {
            _userManager = userManager;
            _signInManager = signInManager;
            _logger = logger;
        }


        [BindProperty]
        public InputChangeEmailModel InputEmail { get; set; }
        public InputChangePasswordModel InputPassword { get; set; }

        [TempData]
        public string updateEmailResponse { get; set; }
        public string updatePasswordResponse { get; set; }


        public class InputChangeEmailModel
        {
            /*For Email changes.*/
            [Required]
            [DataType(DataType.EmailAddress)]
            [Display(Name = "New Email")]
            public string NewEmail { get; set; }
        }

        public class InputChangePasswordModel
        {
            /*For password changes*/
            [Required]
            [DataType(DataType.Password)]
            [Display(Name = "Current Password")]
            public string CurrentPassword { get; set; }


            [Required]
            [StringLength(100, ErrorMessage = "The {0} must be at least {2} and at max {1} characters long.", MinimumLength = 7)]
            [DataType(DataType.Password)]
            [Display(Name = "New password")]
            public string NewPassword { get; set; }


            [Required]
            [DataType(DataType.Password)]
            [Display(Name = "Confirm new password")]
            [Compare("NewPassword", ErrorMessage = "The password and confirmation password do not match.")]
            public string ConfirmPassword { get; set; }
        }

        public void OnGet(string returnUrl = null)
        {         

        }


        public async Task<IActionResult> OnPostChangeEmailSectionAsync(string returnUrl = null)
        {

            //First check if the server side C# validation passed.
            if(!ModelState.IsValid)
            {
                return Page();
            }

            var user =  await _userManager.GetUserAsync(User);

            if (user == null)
            {
                return NotFound($"Unable to load the user with Id '{_userManager.GetUserId(User)}'.");
            }

            var currentUserName = _userManager.FindByNameAsync(_userManager.GetUserName(User)).Result.UserName;
            String startingUserEmail =  _userManager.FindByNameAsync(_userManager.GetUserName(User)).Result.Email;

            var emailToken =  await _userManager.GenerateChangeEmailTokenAsync(user, InputEmail.NewEmail);
            var updateEmailResult =  await _userManager.ChangeEmailAsync(user, InputEmail.NewEmail, emailToken);

            var newUserEmail = _userManager.FindByNameAsync(_userManager.GetUserName(User)).Result.Email;
            updateEmailResponse = "Success! " + currentUserName + ", your email has been updated from " + startingUserEmail + " to " + newUserEmail + ".";
            ViewData["updateEmailResponse"] = updateEmailResponse;
            return Page();

        }

        public async Task<IActionResult> OnPostChangePasswordSectionAsync(string returnUrl = null)
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }
            var user = await _userManager.GetUserAsync(User);
            if (user == null)
            {
                return NotFound($"Unable to load the user with Id '{_userManager.GetUserId(User)}'.");
            }

            var updatePasswordResult = await _userManager.ChangePasswordAsync(user, InputPassword.CurrentPassword, InputPassword.NewPassword);

            if (!updatePasswordResult.Succeeded)
            {
                foreach (var error in updatePasswordResult.Errors)
                {
                    ModelState.AddModelError(string.Empty, error.Description);
                }
                return Page();
            }

            await _signInManager.RefreshSignInAsync(user);
            _logger.LogInformation("The user changed their password successfully.");
            updatePasswordResponse = user.UserName + ", you have successfully updated your password!";
            return RedirectToPage();
        }
    }
}
