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
    public class OurFarmModel : PageModel
    {

        public string Message { get; set; }

        public void OnGet()
        {

        }

        public void OnPostSetUpTourSection()
        {
            bool validForm = true;
            string contactFormResponse = "";

            string userName = "";
            string userEmail = "";
            string userPhone = "";
            string partySize = "";
            string visitDate = "";
            string visitTime = "";
            string userComments = "";
            try
            {
                userName = System.Web.HttpUtility.HtmlEncode(Request.Form["userName"]);
                userEmail = System.Web.HttpUtility.HtmlEncode(Request.Form["userEmail"]);
                userPhone = System.Web.HttpUtility.HtmlEncode(Request.Form["userPhone"]);
                partySize = System.Web.HttpUtility.HtmlEncode(Request.Form["partySize"]);
                visitDate = System.Web.HttpUtility.HtmlEncode(Request.Form["visitDate"]);
                visitTime = System.Web.HttpUtility.HtmlEncode(Request.Form["visitTime"]);
                userComments = System.Web.HttpUtility.HtmlEncode(Request.Form["userComments"]);
            }
            catch (Exception)
            {
                userName = "";
                userEmail = "";
                userPhone = "";
                partySize = "";
                visitDate = "";
                visitTime = "";
                userComments = "";
            }


            if (userName == "" || userEmail == "" || userPhone == "" || partySize == "" || visitDate == "" || visitTime == "")
            {
                validForm = false;
                contactFormResponse = "Sorry, form not valid, please fill in all required (**) input fields. ";
            }



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


            Int64 userPhoneInteger;
            bool userPhoneIsNumeric = Int64.TryParse(userPhone, out userPhoneInteger);
            if (userPhone.Length != 10 || !userPhoneIsNumeric)
            {
                validForm = false;
                contactFormResponse += "Phone must be exactly 10 digits in length, no dashes. ";
            }

            Int64 partySizeInteger;
            bool partySizeIsNumeric = Int64.TryParse(partySize, out partySizeInteger);
            if (partySize.Length < 1 || userPhone.Length > 2 || !partySizeIsNumeric)
            {
                validForm = false;
                contactFormResponse += "Party size must be an integer of between 1-99. ";
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
                string ToEmail = "contact@sereneriverfarms.com";
                string EmailSubject = "Serene River Farms: Request a Tour Date!";

                string BodyEmail = "<strong>From:</strong> " + userName + "<br />";
                BodyEmail += "<strong>Email:</strong> " + userEmail + "<br />";
                BodyEmail += "<strong>Phone:</strong> " + userPhone + "<br />";
                BodyEmail += "<strong>Party Size:</strong> " + partySize + "<br />";
                BodyEmail += "<strong>Requested Tour Date and Time:</strong> " + visitDate + " at " + visitTime + "<br />";
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
                    destinationSmtp.Authenticate("youremail", "yourpassword");
                    destinationSmtp.Send(emailMessage);
                    destinationSmtp.Disconnect(true);
                    destinationSmtp.Dispose();

                    contactFormResponse = "Thank you " + userName + ", we will review your tour request for " + visitDate + " at " + visitTime + " for " + partySize +" and get back to you soon.  " +
                        "Our reply will be sent to your email at: " + userEmail + ".";
                }
            }

            ViewData["Message"] = "" + contactFormResponse;
        }
    }
}