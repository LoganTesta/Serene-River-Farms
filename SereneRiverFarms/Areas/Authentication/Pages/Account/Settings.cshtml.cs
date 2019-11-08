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
using SereneRiverFarms.Areas.Authentication.Data;


namespace SereneRiverFarms.Areas.Authentication.Pages.Account
{
    [AllowAnonymous]
    public class SettingsModel : PageModel
    {

        public string updateUserNameResponse { get; set; }

        private readonly SignInManager<SereneRiverFarmsUser> _signInManager;
        private readonly UserManager<SereneRiverFarmsUser> _userManager;
        private readonly ILogger<RegisterModel> _logger;
        private readonly IEmailSender _emailSender;

        public SettingsModel(
            UserManager<SereneRiverFarmsUser> userManager,
            SignInManager<SereneRiverFarmsUser> signInManager,
            ILogger<RegisterModel> logger,
            IEmailSender emailSender)
        {
            _userManager = userManager;
            _signInManager = signInManager;
            _logger = logger;
            _emailSender = emailSender;
        }

        [BindProperty]
        public InputModel Input { get; set; }

        public string ReturnUrl { get; set; }


        public void OnPostChangeUserNameSection()
        {
            bool validForm = true;
            string updateUserNameResponse = "";

            string userNameNew = System.Web.HttpUtility.HtmlEncode(Request.Form["userNameNew"]);
            if(userNameNew == "")
            {
                validForm = false; 
            }

            if (userNameNew.Length < 3 || userNameNew.Length > 40)
            {
                validForm = false;
            }
          
            if (validForm == false)
            {
                updateUserNameResponse = "The username must be between 3 and 40 characters in length.";
            }

            
            ViewData["updateUserNameResponse"] = "" + updateUserNameResponse;
        }

        public class InputModel
        {

            [StringLength(100, ErrorMessage = "The {0} must be at least {2} and at max {1} characters long.", MinimumLength = 7)]
            [DataType(DataType.Password)]
            [Display(Name = "userPassword")]
            public string userPassword { get; set; }

            [DataType(DataType.Password)]
            [Display(Name = "Confirm password")]
            [Compare("userConfirmPassword", ErrorMessage = "The password and confirmation password do not match.")]
            public string userConfirmPassword { get; set; }
        }

        public void OnGet(string returnUrl = null)
        {
            ReturnUrl = returnUrl;
        }


        public async Task<IActionResult> OnPostAsync(string returnUrl = null)
        {
            returnUrl = returnUrl ?? Url.Content("~/");

            if (ModelState.IsValid)
            {
                string contactFormResponse = "";
                ViewData["Message"] = "" + contactFormResponse;
            }

            //If we made it here, something went wrong so redisplay the form.
            return Page();
        }
    }
}
