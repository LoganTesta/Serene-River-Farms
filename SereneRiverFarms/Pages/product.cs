
using System;

public class product
{

    public string name { get; set; }
    public string namePlural { get; set; }
    public string classCSS { get; set; }
    public decimal price { get; set; }
    public string image { get; set; }
    public string description { get; set; }

    public product(string name, string namePlural, string classCSS, decimal price, string image, string description)
    {
        this.name = name;
        this.namePlural = namePlural;
        this.classCSS = classCSS;
        this.price = Math.Round(price, 2, MidpointRounding.AwayFromZero);
        this.image = image;
        this.description = description;
    }
}