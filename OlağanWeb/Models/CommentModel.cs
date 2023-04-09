namespace OlağanWeb.Models;

public class CommentModel
{
    public int? CommentId { get; set; }
    public string? TextId { get; set; }
    public string Comments { get; set; }
    public string Email { get; set; }
    public string Name { get; set; }
    public bool IsActive { get; set; }
    public DateTime? CommentDate { get; set; }

}

