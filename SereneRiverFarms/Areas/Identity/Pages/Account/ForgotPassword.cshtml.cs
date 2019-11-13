using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc.RazorPages;


using MailKit.Net.Smtp;
using MailKit.Security;
using MimeKit;


namespace SereneRiverFarms.Areas.Identity.Pages.Account
{

    public class ForgotPasswordModel : PageModel
    {

        public string Message { get; set; }

        public ForgotPasswordModel()
        {

        }

        public void OnPostForgotPasswordSection()
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
                contactFormResponse += "Sorry, form not valid";
            }
            else if (validForm)
            {

                //Construct the Email
                string FromName = "Serene River Farms";
                string FromEmail = "contact@sereneriverfarms.com";
                string ToEmail = userEmail;
                string EmailSubject = "Reset Password Request";

                string BodyEmail = "<strong>From:</strong> " + FromName + "<br />";
                BodyEmail += "<strong>Email:</strong> " + userEmail + "<br />";
                BodyEmail += "<strong>Subject:</strong> Reset your Password<br />";
                BodyEmail += "<strong>Message/Comments:</strong> It looks like you recently requested to reset your password.  Click this link " +
                    "to go to the password reset page.";


                var emailMessage = new MimeMessage();
                emailMessage.From.Add(new MailboxAddress(FromName, FromEmail));
                emailMessage.To.Add(new MailboxAddress("Serene River Farms", ToEmail));

                emailMessage.Subject = EmailSubject;
                BodyBuilder emailBody = new BodyBuilder();
                emailBody.HtmlBody = "" + BodyEmail;
                emailMessage.Body = emailBody.ToMessageBody();

                using (var destinationSmtp = new SmtpClient())
                {
                    destinationSmtp.Connect("cmx5.my-hosting-panel.com", 465, true);
                    destinationSmtp.Authenticate("youremail", "yourpassword");
                    destinationSmtp.Send(emailMessage);
                    destinationSmtp.Disconnect(true);
                    destinationSmtp.Dispose();

                    contactFormResponse = "Your request to reset your password has been sent to " + userEmail + ".";
                }
            }
            ViewData["Message"] = contactFormResponse;
        }
    }
}
