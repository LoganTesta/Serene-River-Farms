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

        public string updateNameResponse { get; set; }
        public string updateEmailResponse { get; set; }

        [BindProperty]
        public InputChangePasswordModel InputPassword { get; set; }

        [TempData]
        public string updatePasswordResponse { get; set; }

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

        public async Task<IActionResult> OnPostChangeNameSectionAsync(string returnUrl = null)
        {
            //Validate name.
            bool validForm = true;
            string userNewName = "";

            try
            {
                userNewName = System.Web.HttpUtility.HtmlEncode(Request.Form["userNewName"]);
            }
            catch (Exception)
            {
                userNewName = "";
            }


            bool validNewName = true;
            if (userNewName == "")
            {
                validNewName = false;
                updateNameResponse += "Please enter your desired new user name. ";
            }


            if (validNewName == false)
            {
                validForm = false;
                updateNameResponse += " Please fill out all fields in the peoper format.";
            }

            if (validForm)
            {
                var user = await _userManager.GetUserAsync(User);

                if (user == null)
                {
                    return NotFound($"Unable to load the user with Id '{_userManager.GetUserId(User)}'.");
                }
                var currentUserName = _userManager.FindByNameAsync(_userManager.GetUserName(User)).Result.UserName;
                var currentUserId = _userManager.FindByNameAsync(_userManager.GetUserName(User)).Result.Id;



                var changeNameUser = await _userManager.FindByIdAsync(currentUserId);
                changeNameUser.UserName = "" + userNewName;
                await _userManager.UpdateAsync(changeNameUser);
                await _signInManager.RefreshSignInAsync(user);


                ViewData["updateNameResponse"] = updateNameResponse;

                ViewData["currentUser"] =""+ await _userManager.FindByIdAsync(currentUserId);
                ViewData["currentUserName"] = "" + changeNameUser.UserName;

                return RedirectToPage();   //This is critical to prevent a potential error.
            }
            else
            {
                return Page();
            }
        }


        public async Task<IActionResult> OnPostChangeEmailSectionAsync(string returnUrl = null)
        {
            //Validate email.
            bool validForm = true;
            string userNewEmail = "";

            try
            {
                userNewEmail = System.Web.HttpUtility.HtmlEncode(Request.Form["userNewEmail"]);
            }
            catch (Exception)
            {
                userNewEmail = "";
            }


            bool validNewEmail = true;
            if (userNewEmail == "")
            {
                validNewEmail = false;
                updateEmailResponse = "Sorry, form not valid, please fill in all required (**) input fields. ";
            }


            if (!userNewEmail.Contains("@"))
            {
                validNewEmail = false;
                updateEmailResponse += "Email must contain at least 1 @ symbol. ";
            }

            if (!userNewEmail.Contains("."))
            {
                validNewEmail = false;
                updateEmailResponse += "Email must contain at least 1 period (.). ";
            }

            int atSymbolIndex = userNewEmail.IndexOf("@");
            int lastPeriodSymbol = userNewEmail.LastIndexOf(".");
            int userEmailLength = userNewEmail.Length;


            //Ensure at least 1 char before first @ symbol.
            if (!(atSymbolIndex > 0))
            {
                validNewEmail = false;
                updateEmailResponse += "Email must have at least one chracter before first @. ";
            }

            //Verify that at least 1 @ symbol comes before the last period, and that there is at least
            //one char in between them.
            if (!(atSymbolIndex + 1 < lastPeriodSymbol))
            {
                validNewEmail = false;
                updateEmailResponse += "Email must have at least 1 @ symbol before the last period (.). ";
            }

            //Verify that there are at least 2 chars after the last period.
            if (!(lastPeriodSymbol + 2 < userEmailLength))
            {
                validNewEmail = false;
                updateEmailResponse += "Email must contain at least two characters after the last period (.). ";
            }

            if (validNewEmail == false)
            {
                validForm = false;
                updateEmailResponse += " Please enter a valid email.";
            }

            if (validForm) { 
                var user = await _userManager.GetUserAsync(User);
                
                if (user == null)
                {
                    return NotFound($"Unable to load the user with Id '{_userManager.GetUserId(User)}'.");
                }
                var currentUserName = _userManager.FindByNameAsync(_userManager.GetUserName(User)).Result.UserName;
                var currentUserId = _userManager.FindByNameAsync(_userManager.GetUserName(User)).Result.Id;

                String startingUserEmail = _userManager.FindByNameAsync(_userManager.GetUserName(User)).Result.Email;
                var emailToken = await _userManager.GenerateChangeEmailTokenAsync(user, userNewEmail);
                var updateEmailResult = await _userManager.ChangeEmailAsync(user, userNewEmail, emailToken);


                var newUserEmail = _userManager.FindByNameAsync(_userManager.GetUserName(User)).Result.Email;
                updateEmailResponse = "Success! " + currentUserName + ", your email has been updated from " + startingUserEmail + " to " + newUserEmail + ".";
                ViewData["updateEmailResponse"] = updateEmailResponse;


                await _signInManager.RefreshSignInAsync(user);
                return RedirectToPage();
            } else
            {
                return Page();
            }
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
            updatePasswordResponse += "" + user.UserName + ", you have successfully updated your password!";
            return RedirectToPage();
        }
    }
}
