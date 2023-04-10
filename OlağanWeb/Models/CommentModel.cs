namespace OlağanWeb.Models;

public class CommentModel
{
    public int? CommentId { get; set; }
    public int? TextId { get; set; }
    public string Comments { get; set; }
    public string Email { get; set; }
    public string Name { get; set; }
    public bool IsActive { get; set; }
    public string? Title { get; set; }

    public DateTime? CommentDate { get; set; }

}

