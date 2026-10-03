using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Common.Domain;

namespace CommentModule.Domain;

class Comment : Entity
{
    [Required]
    [MaxLength(500)]
    public string Text { get; set; } = null!;
    public Guid? PrentId { get; set; }
    public Guid EntityId { get; set; }
    public Guid UserId { get; set; }
    public bool IsActive { get; set; }
    public CommentType CommentType { get; set; }

    [ForeignKey("UserId")]
    public User User { get; set; } = null!;

    [ForeignKey("PrentId")]
    public List<Comment> Replies { get; set; } = [];
}

public enum CommentType
{
    Course = 0,
    Article = 1,
}