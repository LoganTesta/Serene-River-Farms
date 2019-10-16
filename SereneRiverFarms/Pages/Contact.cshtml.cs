using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc.RazorPages;


using MailKit.Net.Smtp;
using MailKit.Security;
using MimeKit;

namespace SereneRiverFarms.Pages
{
    public class ContactModel : PageModel
    {
        public string Message { get; set; }

        public void OnGet()
        {
            Message = "Your contact page.";
        }

        public void OnPostContactSection()
        {
            bool validForm = true;
            string contactFormResponse = "";

            string userName = "";
            string userEmail = "";
            string userSubject = "";
            string userComments = "";
            try
            {
                userName = System.Web.HttpUtility.HtmlEncode(Request.Form["userName"]);
                userEmail = System.Web.HttpUtility.HtmlEncode(Request.Form["userEmail"]);
                userSubject = System.Web.HttpUtility.HtmlEncode(Request.Form["userSubject"]);
                userComments = System.Web.HttpUtility.HtmlEncode(Request.Form["userComments"]);
            }
            catch (Exception)
            {
                userName = "";
                userEmail = "";
                userSubject = "";
                userComments = "";
            }



            if (userName == "" || userEmail == "" || userComments == "")
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
                contactFormResponse += "";
            }
            else if (validForm)
            {

                //Construct the Email
                string FromName = userName;
                string FromEmail = userEmail;
                string ToEmail = "contact@riverfrontsandwiches.com";
                string EmailSubject = userSubject;

                string BodyEmail = "<strong>From:</strong> " + userName + "<br />";
                BodyEmail += "<strong>Email:</strong> " + userEmail + "<br />";
                BodyEmail += "<strong>Subject:</strong> " + userSubject + "<br />";
                BodyEmail += "<strong>Message/Comments:</strong> " + userComments;


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
                    destinationSmtp.Authenticate("yourname", "yourpassword");
                    destinationSmtp.Send(emailMessage);
                    destinationSmtp.Disconnect(true);
                    destinationSmtp.Dispose();

                    contactFormResponse = "Thank you " + userName + ", we look forward to reading your comments and our reply will be sent to your email at: " + userEmail + ".";
                }
            }

            ViewData["Message"] = "" + contactFormResponse;
        }
    }
}
