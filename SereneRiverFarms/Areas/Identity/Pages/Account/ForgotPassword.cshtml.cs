using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.Extensions.Logging;
using SereneRiverFarms.Areas.Identity.Data;

using System.Text.Encodings.Web;
using Microsoft.AspNetCore.Identity.UI.Services;   //For IEmail Sender

using MailKit.Net.Smtp;
using MailKit.Security;
using MimeKit;


namespace SereneRiverFarms.Areas.Identity.Pages.Account
{
    [AllowAnonymous]
    public class ForgotPasswordModel : PageModel
    {

        public string Message { get; set; }
        private readonly UserManager<SereneRiverFarmsUser> _userManager;
        private readonly IEmailSender _emailSender;


        public ForgotPasswordModel(UserManager<SereneRiverFarmsUser> userManager, IEmailSender emailSender)
        {
            _userManager = userManager;
            _emailSender = emailSender;
        }

        public async Task<IActionResult> OnPostForgotPasswordSection()
        {
            bool validForm = true;
            string contactFormResponse = "";

            string userEmail = "";
            try
            {
                userEmail = System.Web.HttpUtility.HtmlEncode(Request.Form["userEmail"]);
            }
            catch (Exception)
            {
                userEmail = "";
            }


            if (userEmail == "")
            {
                validForm = false;
                contactFormResponse = "Sorry, form not valid, please fill in all required (**) input fields. ";
            }

            if (validForm)
            {
                if (!userEmail.Contains("@"))
                {
                    validForm = false;
                    contactFormResponse += "Email must contain at least 1 @ symbol. ";
                }

                if (!userEmail.Contains("."))
                {
                    validForm = false;
                    contactFormResponse += "Email must contain at least 1 period (.). ";
                }

                int atSymbolIndex = userEmail.IndexOf("@");
                int lastPeriodSymbol = userEmail.LastIndexOf(".");
                int userEmailLength = userEmail.Length;


                //Ensure at least 1 char before first @ symbol.
                if (!(atSymbolIndex > 0))
                {
                    validForm = false;
                    contactFormResponse += "Email must have at least one chracter before first @. ";
                }

                //Verify that at least 1 @ symbol comes before the last period, and that there is at least
                //one char in between them.
                if (!(atSymbolIndex + 1 < lastPeriodSymbol))
                {
                    validForm = false;
                    contactFormResponse += "Email must have at least 1 @ symbol before the last period (.). ";
                }

                //Verify that there are at least 2 chars after the last period.
                if (!(lastPeriodSymbol + 2 < userEmailLength))
                {
                    validForm = false;
                    contactFormResponse += "Email must contain at least two characters after the last period (.). ";
                }
            }


            if (!validForm)
            {
                contactFormResponse += "Sorry, form not valid.";
            }
            else if (validForm)
            {
                var user = await _userManager.FindByEmailAsync(userEmail);
                if(user == null || !(await _userManager.IsEmailConfirmedAsync(user)))
                {
                    //Don't let the user know that their provided username does not exist.
                    return RedirectToPage("./ForgotPasswordEmailSent");
                }


                var code = await _userManager.GeneratePasswordResetTokenAsync(user);
                var callbackUrl = Url.Page(
                    "/ResetPassword",
                    pageHandler: null,
                    values: new { code },
                    protocol: Request.Scheme);

                await _emailSender.SendEmailAsync(
                    userEmail,
                    "Reset Password",
                    $"Please reset your password by <a href='{HtmlEncoder.Default.Encode(callbackUrl)}'>clicking here to go to the reset page</a>.");

                contactFormResponse = "Your request to reset your password has been sent to " + userEmail + ".";
                ViewData["Message"] = contactFormResponse;
                return RedirectToPage("./ForgotPasswordEmailSent");

            }
            ViewData["Message"] = contactFormResponse;
            return Page();
        }
    }
}
