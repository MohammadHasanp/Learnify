using CommentModule.Domain;
using Common.Query;

namespace CommentModule.Services.DTOs;

public record CreateCommentCommand(Guid? PrentId, Guid EntityId, Guid UserId, string Text, CommentType CommentType);

public class CommentDto : BaseDto
{
    public string Text { get; set; } = null!;
    public string? FullName { get; set; }
    public string? Email { get; set; }
    public string AvatarName { get; set; } = null!;
    public Guid EntityId { get; set; }
    public Guid UserId { get; set; }
    public bool IsActive { get; set; }
    public CommentType CommentType { get; set; }

    public List<CommentReplyDto> Replies { get; set; } = [];
}

public class CommentReplyDto : BaseDto
{
    public string Text { get; set; } = null!;
    public string? FullName { get; set; }
    public string? Email { get; set; }
    public Guid? PrentId { get; set; }
    public string AvatarName { get; set; } = null!;
    public Guid EntityId { get; set; }
    public Guid UserId { get; set; }
    public bool IsActive { get; set; }
    public CommentType CommentType { get; set; }
}


public class CommentFilterParams : BaseFilterParam
{
    public DateTimeOffset? StartDate { get; set; }
    public DateTimeOffset? EndDate { get; set; }
    public string? Name { get; set; }
    public string? Family { get; set; }
    public Guid? EntityId { get; set; }
    public CommentType? CommentType { get; set; }
}

public class CommentFilterResult : BaseFilter<CommentDto, CommentFilterParams>
{

}

public class AllCommentFilterResult : BaseFilter<CommentReplyDto>
{
}