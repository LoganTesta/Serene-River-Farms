
public class product
{

    public string name { get; set; }
    public string namePlural { get; set; }
    public double price { get; set; }
    public string image { get; set; }
    public string description { get; set; }

    public product(string name, string namePlural, double price, string image, string description)
    {
        this.name = name;
        this.namePlural = namePlural;
        this.price = price;
        this.image = image;
        this.description = description;
    }
}