﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;


using MailKit.Net.Smtp;
using MailKit.Security;
using MimeKit;

using Microsoft.AspNetCore.Http;   //For Sessions

using System.Collections;   //For ArrayLists

namespace SereneRiverFarms.Pages
{
    public class OurProductsModel : PageModel
    {

        public string Message { get; set; }

        ArrayList theSessionVariables = new ArrayList();
        ArrayList productNames = new ArrayList();
        ArrayList productPrices = new ArrayList();
        ArrayList productSubtotals = new ArrayList();


        public OurProductsModel()
        {

            productNames.Add("Pears");
            productNames.Add("Apples");
            productNames.Add("Blueberries");
            productNames.Add("Strawberries");
            productNames.Add("Raspberries");
            productNames.Add("Cherries");
            productNames.Add("Pumpkins");
            productNames.Add("Milk Gallons");
            productNames.Add("12 Ounce Jam Jars");

            productPrices.Add(2.50);
            productPrices.Add(1.50);
            productPrices.Add(3.00);
            productPrices.Add(2.75);
            productPrices.Add(4.50);
            productPrices.Add(3.50);
            productPrices.Add(1.79);
            productPrices.Add(2.50);
            productPrices.Add(6.00);

            productSubtotals.Add(0);
            productSubtotals.Add(0);
            productSubtotals.Add(0);
            productSubtotals.Add(0);
            productSubtotals.Add(0);
            productSubtotals.Add(0);
            productSubtotals.Add(0);
            productSubtotals.Add(0);
            productSubtotals.Add(0);

            theSessionVariables.Add("numberOfPears");
            theSessionVariables.Add("numberOfApples");
            theSessionVariables.Add("numberOfBlueberries");
            theSessionVariables.Add("numberOfStrawberries");
            theSessionVariables.Add("numberOfRaspberries");
            theSessionVariables.Add("numberOfCherries");
            theSessionVariables.Add("numberOfPumpkins");
            theSessionVariables.Add("numberOfMilkGallons");
            theSessionVariables.Add("numberOf12OunceJamJars");

        }
        public void OnGet()
        {
            HttpContext.Session.SetInt32("numberOfPears", 0);
            HttpContext.Session.SetInt32("numberOfApples", 0);
            HttpContext.Session.SetInt32("numberOfBlueberries", 0);
            HttpContext.Session.SetInt32("numberOfStrawberries", 0);
            HttpContext.Session.SetInt32("numberOfRaspberries", 0);
            HttpContext.Session.SetInt32("numberOfCherries", 0);
            HttpContext.Session.SetInt32("numberOfPumpkins", 0);
            HttpContext.Session.SetInt32("numberOfMilkGallons", 0);
            HttpContext.Session.SetInt32("numberOf12OunceJamJars", 0);
            HttpContext.Session.SetString("Cart Total", "" + 0);
        }



        public void OnGetAddItem()
        {
            int productName = Convert.ToInt32(Request.Query["item"]);
            string sessionVariable = Convert.ToString(theSessionVariables[productName]);
            int numberOfItem = Convert.ToInt32(HttpContext.Session.GetInt32("" + sessionVariable) + 1);

            HttpContext.Session.SetInt32("" + sessionVariable, numberOfItem);
            ViewData["" + sessionVariable] = HttpContext.Session.GetInt32("" + sessionVariable);

            double newCartTotal = 0;
            for (int i = 0; i < theSessionVariables.Count; i++)
            {
                int quantityOfEachItem = Convert.ToInt32(HttpContext.Session.GetInt32("" + Convert.ToString(theSessionVariables[i])));
                double priceOfEachItem = Convert.ToDouble(productPrices[i]);
                productSubtotals[i] = priceOfEachItem * quantityOfEachItem;

                ViewData["subtotal" + Convert.ToString(productNames[i]).Replace(" ","")] = "$" + productSubtotals[i];  //Remove any spaces in a product name to prevent failed product matching.
                newCartTotal += quantityOfEachItem * priceOfEachItem;
            }
            HttpContext.Session.SetString("Cart Total", Convert.ToString(newCartTotal));
            ViewData["cartTotal"] = "$" + HttpContext.Session.GetString("Cart Total");
        }


        public void OnGetMinusItem()
        {
            int productName = Convert.ToInt32(Request.Query["item"]);
            string sessionVariable = Convert.ToString(theSessionVariables[productName]);
            int numberOfItem = Convert.ToInt32(HttpContext.Session.GetInt32("" + sessionVariable) - 1);
            if(numberOfItem < 0)
            {
                numberOfItem = 0;
            }

            HttpContext.Session.SetInt32("" + sessionVariable, numberOfItem);
            ViewData["" + sessionVariable] = HttpContext.Session.GetInt32("" + sessionVariable);

            double newCartTotal = 0;
            for (int i = 0; i < theSessionVariables.Count; i++)
            {
                int quantityOfEachItem = Convert.ToInt32(HttpContext.Session.GetInt32("" + Convert.ToString(theSessionVariables[i])));
                double priceOfEachItem = Convert.ToDouble(productPrices[i]);
                productSubtotals[i] = priceOfEachItem * quantityOfEachItem;

                ViewData["subtotal" + Convert.ToString(productNames[i]).Replace(" ", "")] = "$" + productSubtotals[i];
                newCartTotal += quantityOfEachItem * priceOfEachItem;
            }
            HttpContext.Session.SetString("Cart Total", Convert.ToString(newCartTotal));
            ViewData["cartTotal"] = "$" + HttpContext.Session.GetString("Cart Total");
        }



        public void OnGetResetCart()
        {
            //IMPORTANT: Afterwards, reset the session variables to 0 (products, product subtotals, and the cart total).
            for (int i = 0; i < theSessionVariables.Count; i++)
            {
                HttpContext.Session.SetInt32("" + Convert.ToString(theSessionVariables[i]), 0);
                string sessionVariable = Convert.ToString(theSessionVariables[i]);
                ViewData["" + sessionVariable] = HttpContext.Session.GetInt32("" + sessionVariable);
            }
            for (int i = 0; i < productSubtotals.Count; i++)
            {
                productSubtotals[i] = 0;
                ViewData["subtotal" + Convert.ToString(productNames[i]).Replace(" ", "")] = 0;  //Remove any spaces in a product name to prevent failed product matching.
            }
            HttpContext.Session.SetString("Cart Total", "0");
            ViewData["cartTotal"] = "$" + HttpContext.Session.GetString("Cart Total");
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
                BodyEmail += "<strong>Additional Notes:</strong> " + userComments;

                BodyEmail += "<br/ >";
                BodyEmail += "<strong>Products Requested: </strong>" + "" + " <br/ >";

                for (int i = 0; i < theSessionVariables.Count; i++)
                {
                    int quantityOfEachItem = Convert.ToInt32(HttpContext.Session.GetInt32("" + Convert.ToString(theSessionVariables[i])));
                    if(quantityOfEachItem > 0)
                    {
                        double productSubtotal = quantityOfEachItem * Convert.ToDouble(productPrices[i]);
                        BodyEmail += "Product: " + Convert.ToString(productNames[i]) + ": " + quantityOfEachItem + " (Subtotal): $" + productSubtotal + ". <br />";
                    }             
                }
                BodyEmail += "<strong>Estimate of Order Total:</strong> $" + HttpContext.Session.GetString("Cart Total") + ". <br />";
                BodyEmail += " <br/ >";


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

                    contactFormResponse = "Thank you " + userName + ", we will look over your request and send you an " +
                        "estimate soon.  Our reply will be sent to your email at: " + userEmail + ".";
                    contactFormResponse += " Your Order Request: ";
                    for (int i = 0; i < theSessionVariables.Count; i++)
                    {
                        int quantityOfEachItem = Convert.ToInt32(HttpContext.Session.GetInt32("" + Convert.ToString(theSessionVariables[i])));
                        double productSubtotal = quantityOfEachItem * Convert.ToDouble(productPrices[i]);

                        if (quantityOfEachItem > 0)
                        {
                            contactFormResponse += " " + Convert.ToString(productNames[i]) + ": " + quantityOfEachItem + " (Sub: $" + productSubtotal + "), ";
                        }
                    }
                    contactFormResponse += " Estimate of Order Total: $" + HttpContext.Session.GetString("Cart Total") + ". ";



                    //IMPORTANT: Afterwards, reset the session variables to 0 (products, product subtotals, and the cart total).
                    for (int i = 0; i < theSessionVariables.Count; i++)
                    {       
                       HttpContext.Session.SetInt32("" + Convert.ToString(theSessionVariables[i]), 0);
                    }
                    for (int i = 0; i < productSubtotals.Count; i++)
                    {
                        productSubtotals[i] = 0;
                    }
                    HttpContext.Session.SetString("Cart Total", "" + 0);
                }
            }

            ViewData["Message"] = "" + contactFormResponse;
        }
    }
}