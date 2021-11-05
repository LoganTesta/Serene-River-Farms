using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SereneRiverFarms
{
    public class Program
    {

        //These products are used for the .cs part of the page, 
        //the products on the .cshtml page are used for that part of the page.
        public static product pear = new product("Pear", "Pears", "zero", "0", "", 2.50M, "fruits", "", "Fun to eat or bake in cakes.");
        public static product apple = new product("Apple", "Apples", "one", "1", "", 1.50M, "fruits", "", "Assorted varieties!");
        public static product blueberries = new product("Blueberries", "Blueberries", "two", "2", "", 3.00M, "fruits", "", "Delicious summer blueberries.");
        public static product strawberries = new product("Strawberries", "Strawberries", "three", "3", "", 2.75M, "fruits", "", "We use plants and other natural bug prevention methods so you don't have to worry about pesticides in your food!");
        public static product raspberries = new product("Raspberries", "Raspberries", "four", "4", "", 4.50M, "fruits", "", "Sweet and tart and a favorite in late summer.");
        public static product cherries = new product("Cherries", "Cherries", "five", "5", "", 3.50M, "fruits", "", "Very filling and tasty.");
        public static product pumpkin = new product("Pumpkin", "Pumpkins", "six", "6", "", 1.79M, "squashes", "", "Our pumpkin patch is ready to provide you with your next jack-o'-latern.  Also, rumor has it pumpkin goes well in pies.");
        public static product milkGallon = new product("Milk Gallon", "Milk Gallons", "seven", "7", "", 2.50M, "dairy", "", "We love our cows and give them plenty of room to roam.  The result is happier cows and better tasting milk.");
        public static product jamJar = new product("Jam Jar", "Jam Jars", "eight", "8", "", 6.00M, "jams", "", "Nothing like fresh produce to make your day in a jam.");

        public static List<product> products = new List<product>();



        public static void Main(string[] args)
        {
            products.Add(pear);
            products.Add(apple);
            products.Add(blueberries);
            products.Add(strawberries);
            products.Add(raspberries);
            products.Add(cherries);
            products.Add(pumpkin);
            products.Add(milkGallon);
            products.Add(jamJar);

            CreateHostBuilder(args).Build().Run();
        }

        public static IHostBuilder CreateHostBuilder(string[] args) =>
            Host.CreateDefaultBuilder(args)
                .ConfigureWebHostDefaults(webBuilder =>
                {
                    webBuilder.UseStartup<Startup>();
                });
    }
}
