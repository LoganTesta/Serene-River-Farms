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
        List<string> productNames = new List<string>();
        List<decimal> productPrices = new List<decimal>();
        List<decimal> productSubtotals = new List<decimal>();

        //These products are used for the .cs part of the page, 
        //the products on the .cshtml page are used for that part of the page.
        List<product> products = new List<product>();
        product pear = new product("Pear", "Pears", "zero", 2.50M, "", "Fun to eat or bake in cakes.");
        product apple = new product("Apple", "Apples", "one", 1.50M, "", "Assorted varieties!");
        product blueberries = new product("Blueberries", "Blueberries", "two", 3.00M, "", "Delicious summer blueberries.");
        product strawberries = new product("Strawberries", "Strawberries", "three", 2.75M, "", "We use plants and other natural bug prevention methods so you don't have to worry about pesticides in your food!");
        product raspberries = new product("Raspberries", "Raspberries", "four", 4.50M, "", "Sweet and tart and a favorite in late summer.");
        product cherries = new product("Cherries", "Cherries", "five", 3.50M, "", "Very filling and tasty.");
        product pumpkin = new product("Pumpkin", "Pumpkins", "six", 1.79M, "", "Our pumpkin patch is ready to provide you with your next jack-o'-latern.  Also, rumor has it pumpkin goes well in pies.");
        product milkGallon = new product("Milk Gallon", "Milk Gallons", "seven", 2.50M, "", "We love our cows and give them plenty of room to roam.  The result is happier cows and better tasting milk.");
        product jamJar12Ounce = new product("12 Ounce Jam Jar", "12 Ounce Jam Jars", "eight", 6.00M, "", "Nothing like fresh produce to make your day in a jam.");


        public OurProductsModel()
        {
            products.Add(pear);
            products.Add(apple);
            products.Add(blueberries);
            products.Add(strawberries);
            products.Add(raspberries);
            products.Add(cherries);
            products.Add(pumpkin);
            products.Add(milkGallon);
            products.Add(jamJar12Ounce);

            for (int i = 0; i < products.Count; i++)
            {
                productNames.Add(products[i].namePlural);
                productPrices.Add(products[i].price);
                productSubtotals.Add(0);
            }

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

            HttpContext.Session.SetString("Number of Items", "" + 0);
            HttpContext.Session.SetString("Cart Total", "" + 0);

            ViewData["numberOfPears"] = "0";
            ViewData["numberOfApples"] = "0";
            ViewData["numberOfBlueberries"] = "0";
            ViewData["numberOfStrawberries"] = "0";
            ViewData["numberOfRaspberries"] = "0";
            ViewData["numberOfCherries"] = "0";
            ViewData["numberOfPumpkins"] = "0";
            ViewData["numberOfMilkGallons"] = "0";
            ViewData["numberOf12OunceJamJars"] = "0";

            ViewData["subtotalPears"] = "$0";
            ViewData["subtotalApples"] = "$0";
            ViewData["subtotalBlueberries"] = "$0";
            ViewData["subtotalStrawberries"] = "$0";
            ViewData["subtotalRaspberries"] = "$0";
            ViewData["subtotalCherries"] = "$0";
            ViewData["subtotalPumpkins"] = "$0";
            ViewData["subtotalMilkGallons"] = "$0";
            ViewData["subtotal12OunceJamJars"] = "$0";

            HttpContext.Session.SetString("Cart Total", "0");
            ViewData["numberOfItems"] = "" + HttpContext.Session.GetString("Number of Items");
            ViewData["cartTotal"] = "$" + HttpContext.Session.GetString("Cart Total");
        }


        public void OnGetSetItemQuantity()
        {
            int numberOfItems = 0;

            int productName = Convert.ToInt32(Request.Query["item"]);
            string sessionVariable = Convert.ToString(theSessionVariables[productName]);
            int numberOfItem = Convert.ToInt32(Request.Query["itemQuantity"]);
            if (numberOfItem < 0)
            {
                numberOfItem = 0;
            }
            if (numberOfItem > 100)
            {
                numberOfItem = 100;
            }

            HttpContext.Session.SetInt32("" + sessionVariable, numberOfItem);
            ViewData["" + sessionVariable] = HttpContext.Session.GetInt32("" + sessionVariable);

            decimal newCartTotal = 0;
            for (int i = 0; i < theSessionVariables.Count; i++)
            {
                int quantityOfEachItem = Convert.ToInt32(HttpContext.Session.GetInt32("" + Convert.ToString(theSessionVariables[i])));
                ViewData["numberOf" + Convert.ToString(productNames[i]).Replace(" ", "")] = Convert.ToInt32(HttpContext.Session.GetInt32("" + Convert.ToString(theSessionVariables[i])));
                decimal priceOfEachItem = productPrices[i];
                productSubtotals[i] = priceOfEachItem * quantityOfEachItem;

                ViewData["subtotal" + Convert.ToString(productNames[i]).Replace(" ", "")] = "$" + productSubtotals[i];  //Remove any spaces in a product name to prevent failed product matching.

                numberOfItems += quantityOfEachItem;
                newCartTotal += quantityOfEachItem * priceOfEachItem;
            }
            HttpContext.Session.SetString("Number of Items", Convert.ToString(numberOfItems));
            ViewData["numberOfItems"] = "" + HttpContext.Session.GetString("Number of Items");
            HttpContext.Session.SetString("Cart Total", Convert.ToString(newCartTotal));
            ViewData["cartTotal"] = "$" + HttpContext.Session.GetString("Cart Total");
        }


        public void OnGetAddItem()
        {
            int numberOfItems = 0;

            int productName = Convert.ToInt32(Request.Query["item"]);
            string sessionVariable = Convert.ToString(theSessionVariables[productName]);
            int numberOfItem = Convert.ToInt32(HttpContext.Session.GetInt32("" + sessionVariable) + 1);
            if (numberOfItem > 100)
            {
                numberOfItem = 100;
            }

            HttpContext.Session.SetInt32("" + sessionVariable, numberOfItem);
            ViewData["" + sessionVariable] = HttpContext.Session.GetInt32("" + sessionVariable);

            decimal newCartTotal = 0;
            for (int i = 0; i < theSessionVariables.Count; i++)
            {
                int quantityOfEachItem = Convert.ToInt32(HttpContext.Session.GetInt32("" + Convert.ToString(theSessionVariables[i])));
                ViewData["numberOf" + Convert.ToString(productNames[i]).Replace(" ", "")] = Convert.ToInt32(HttpContext.Session.GetInt32("" + Convert.ToString(theSessionVariables[i])));
                decimal priceOfEachItem = productPrices[i];
                productSubtotals[i] = priceOfEachItem * quantityOfEachItem;

                ViewData["subtotal" + Convert.ToString(productNames[i]).Replace(" ", "")] = "$" + productSubtotals[i];  //Remove any spaces in a product name to prevent failed product matching.

                numberOfItems += quantityOfEachItem;
                newCartTotal += quantityOfEachItem * priceOfEachItem;
            }
            HttpContext.Session.SetString("Number of Items", Convert.ToString(numberOfItems));
            ViewData["numberOfItems"] = "" + HttpContext.Session.GetString("Number of Items");
            HttpContext.Session.SetString("Cart Total", Convert.ToString(newCartTotal));
            ViewData["cartTotal"] = "$" + HttpContext.Session.GetString("Cart Total");
        }


        public void OnGetMinusItem()
        {
            int numberOfItems = 0;

            int productName = Convert.ToInt32(Request.Query["item"]);
            string sessionVariable = Convert.ToString(theSessionVariables[productName]);
            int numberOfItem = Convert.ToInt32(HttpContext.Session.GetInt32("" + sessionVariable) - 1);
            if (numberOfItem < 0)
            {
                numberOfItem = 0;
            }

            HttpContext.Session.SetInt32("" + sessionVariable, numberOfItem);
            ViewData["" + sessionVariable] = HttpContext.Session.GetInt32("" + sessionVariable);

            decimal newCartTotal = 0;
            for (int i = 0; i < theSessionVariables.Count; i++)
            {
                int quantityOfEachItem = Convert.ToInt32(HttpContext.Session.GetInt32("" + Convert.ToString(theSessionVariables[i])));
                ViewData["numberOf" + Convert.ToString(productNames[i]).Replace(" ", "")] = Convert.ToInt32(HttpContext.Session.GetInt32("" + Convert.ToString(theSessionVariables[i])));
                decimal priceOfEachItem = productPrices[i];
                productSubtotals[i] = priceOfEachItem * quantityOfEachItem;

                ViewData["subtotal" + Convert.ToString(productNames[i]).Replace(" ", "")] = "$" + productSubtotals[i];

                numberOfItems += quantityOfEachItem;
                newCartTotal += quantityOfEachItem * priceOfEachItem;
            }
            HttpContext.Session.SetString("Number of Items", Convert.ToString(numberOfItems));
            ViewData["numberOfItems"] = "" + HttpContext.Session.GetString("Number of Items");
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
                ViewData["subtotal" + Convert.ToString(productNames[i]).Replace(" ", "")] = "$0";  //Remove any spaces in a product name to prevent failed product matching.
            }
            HttpContext.Session.SetString("Number of Items", "0");
            ViewData["numberOfItems"] = "" + HttpContext.Session.GetString("Number of Items");
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
            if (userPhone.Length !=10 || !userPhoneIsNumeric)
            {
                validForm = false;
                contactFormResponse += "Phone must be exactly 10 digits in length, no dashes. ";
            }


            Int64 userZipCodeInteger;
            bool userZipCodeIsNumeric = Int64.TryParse(userZipCode, out userZipCodeInteger);
            if(userZipCode.Length != 5 || !userZipCodeIsNumeric)
            {
                validForm = false;
                contactFormResponse += "Zip code must be a number exactly 5 digits in length. ";
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
                    if (quantityOfEachItem > 0)
                    {
                        decimal productSubtotal = quantityOfEachItem * productPrices[i];
                        BodyEmail += "Product: " + Convert.ToString(productNames[i]) + ": " + quantityOfEachItem + " (Subtotal): $" + productSubtotal + ". <br />";
                    }
                }
                BodyEmail += "<strong>Estimate of Order Total:</strong> $" + HttpContext.Session.GetString("Cart Total") + ". <br />";
                BodyEmail += " <br/ >";


                var emailMessage = new MimeMessage();
                emailMessage.From.Add(new MailboxAddress(FromName, ToEmail));
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
                        "estimate soon.  Our reply will be sent to your email at: " + userEmail + ".<br /><br />";
                    contactFormResponse += " Your Order Request: <br /><br/>";
                    for (int i = 0; i < theSessionVariables.Count; i++)
                    {
                        int quantityOfEachItem = Convert.ToInt32(HttpContext.Session.GetInt32("" + Convert.ToString(theSessionVariables[i])));
                        decimal productSubtotal = quantityOfEachItem * productPrices[i];

                        if (quantityOfEachItem > 0)
                        {
                            contactFormResponse += " " + Convert.ToString(productNames[i]) + ": " + quantityOfEachItem + " (Sub: $" + productSubtotal + "), <br />";
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
                    HttpContext.Session.SetString("Number of Items", "0");
                    HttpContext.Session.SetString("Cart Total", "" + 0);
                }
            }

            ViewData["Message"] = "" + contactFormResponse;
        }
    }

}