using System.ComponentModel.DataAnnotations;

namespace OlağanWeb.Models;

public class TextModel
{
    public int? Id { get; set; }

    public string Title { get; set; }

    public string Category { get; set; }

    public string Writer { get; set; }

    public string number { get; set; }

    public string picture { get; set; }

    public string Text { get; set; }

    public string Referances{ get; set; }

    public string Summary { get; set; }

    public bool PublishedInOlaganSiir{ get; set; }
}
