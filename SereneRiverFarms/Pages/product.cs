
using System;

public class product
{

    public string name { get; set; }
    public string namePlural { get; set; }
    public string classCSS { get; set; }
    public string classInt { get; set; }
    public string displayCSS { get; set; }
    public decimal price { get; set; }
    public string category { get; set; }
    public string image { get; set; }
    public string description { get; set; }

    public product(string name, string namePlural, string classCSS, string classInt, string displayCSS, decimal price, string category, string image, string description)
    {
        this.name = name;
        this.namePlural = namePlural;
        this.classCSS = classCSS;
        this.classInt = classInt;
        this.price = Math.Round(price, 2, MidpointRounding.AwayFromZero);
        this.category = category;
        this.image = image;
        this.description = description;
    }
}