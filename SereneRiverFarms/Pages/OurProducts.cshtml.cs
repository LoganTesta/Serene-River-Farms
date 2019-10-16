using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;


using MailKit.Net.Smtp;
using MailKit.Security;
using MimeKit;

namespace SereneRiverFarms.Pages
{
    public class OurProductsModel : PageModel
    {
        public void OnGet()
        {

        }

        public void OnPostEstimateSection()
        {
            bool validForm = true;
            string contactFormResponse = "";

            string userName = "";
            string userEmail = "";
            string userPhone = "";
            string userAddress = "";
            string userCity = "";
            string userState = "";
            string userZipCode = "";
            string userComments = "";

            try
            {
                userName = System.Web.HttpUtility.HtmlEncode(Request.Form["userName"]);
                userEmail = System.Web.HttpUtility.HtmlEncode(Request.Form["userEmail"]);
                userPhone = System.Web.HttpUtility.HtmlEncode(Request.Form["userPhone"]);
                userAddress = System.Web.HttpUtility.HtmlEncode(Request.Form["userAddress"]);
                userCity = System.Web.HttpUtility.HtmlEncode(Request.Form["userCity"]);
                userState = System.Web.HttpUtility.HtmlEncode(Request.Form["userState"]);
                userZipCode = System.Web.HttpUtility.HtmlEncode(Request.Form["userZipCode"]);
                userComments = System.Web.HttpUtility.HtmlEncode(Request.Form["additionalNotes"]);
            }
            catch (Exception)
            {
                userName = "";
                userEmail = "";
                userPhone = "";
                userAddress = "";
                userCity = "";
                userState = "";
                userZipCode = "";
                userComments = "";
            }



            if (userName == "" || userEmail == "" || userPhone == "" || userAddress == "" || userCity == "" || userState == "" || userZipCode == "")
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

            //Need userPhone validation here.



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
                string EmailSubject = "Serene River Farms: Request an Order Estimate";

                string BodyEmail = "<strong>From:</strong> " + userName + "<br />";
                BodyEmail += "<strong>Email:</strong> " + userEmail + "<br />";
                BodyEmail += "<strong>Phone:</strong> " + userPhone + "<br />";
                BodyEmail += "<strong>Shipping Destination:</strong> " + userAddress + "<br />";
                BodyEmail += " " + userCity + ", " + userState + " " + userZipCode + " <br />";
                BodyEmail += "<strong>Addition Notes:</strong> " + userComments;


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

                    contactFormResponse = "Thank you " + userName + ", we will look over your request and send you an " +
                        "estimate soon.  Our reply will be sent to your email at: " + userEmail + ".";
                }
            }

            ViewData["Message"] = "" + contactFormResponse;
        }

    }
}