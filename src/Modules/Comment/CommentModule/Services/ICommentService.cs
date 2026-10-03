using CommentModule.Services.DTOs;
using Common.Application;

namespace CommentModule.Services;

public interface ICommentService
{
    public Task<OperationResult> Create(CreateCommentCommand command);
    public Task<OperationResult> Delete(Guid id);
    public Task<CommentDto?> GetCommentById(Guid id);
    public Task<CommentFilterResult> GetCommentByFilter(CommentFilterParams filterParams);
    public Task<AllCommentFilterResult> GetAllCommentByFilter(CommentFilterParams filterParams);
}