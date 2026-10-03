using AutoMapper;
using CommentModule.Context;
using CommentModule.Domain;
using CommentModule.Services.DTOs;
using Common.Application;
using Microsoft.EntityFrameworkCore;

namespace CommentModule.Services;

class CommentService(CommentContext context, IMapper mapper) : ICommentService
{
    public async Task<OperationResult> Create(CreateCommentCommand command)
    {
        var comment = new Comment
        {
            Id = Guid.NewGuid(),
            Text = command.Text,
            PrentId = command.PrentId,
            EntityId = command.EntityId,
            UserId = command.UserId,
            IsActive = true,
            CommentType = command.CommentType,
        };
        context.Add(comment);
        await context.SaveChangesAsync();
        return OperationResult.Success();
    }

    public async Task<OperationResult> Delete(Guid id)
    {
        var comment = await context.Comments.FirstOrDefaultAsync(s => s.Id == id);
        if (comment == null)
            return OperationResult.NotFound();

        context.Remove(comment);
        await context.SaveChangesAsync();
        return OperationResult.Success();
    }

    public async Task<CommentDto?> GetCommentById(Guid id)
    {
        var comment = await context.Comments
            .Include(s => s.User)
            .Include(d => d.Replies)
            .ThenInclude(w => w.User)
            .FirstOrDefaultAsync(s => s.Id == id);
        if (comment == null)
            return null;

        return new CommentDto
        {
            Id = comment.Id,
            CreationDate = comment.CreationDate,
            IsDelete = comment.IsDelete,
            Text = comment.Text,
            FullName = $"{comment.User.Name} {comment.User.Family}",
            EntityId = comment.EntityId,
            AvatarName = comment.User.AvatarName,
            UserId = comment.UserId,
            IsActive = comment.IsActive,
            CommentType = comment.CommentType,
            Replies = comment.Replies.Select(s => new CommentReplyDto
            {
                Id = s.Id,
                CreationDate = s.CreationDate,
                IsDelete = s.IsDelete,
                AvatarName = s.User.AvatarName,
                Text = s.Text,
                FullName = $"{comment.User.Name} {comment.User.Family}",
                PrentId = s.PrentId,
                EntityId = s.EntityId,
                UserId = s.UserId,
                IsActive = s.IsActive,
                CommentType = s.CommentType
            }).ToList()
        };
    }

    public async Task<CommentFilterResult> GetCommentByFilter(CommentFilterParams filterParams)
    {
        var result = context.Comments
            .Include(s => s.Replies)
            .ThenInclude(d => d.User)
            .Include(n => n.User)
            .Where(e => e.PrentId == null)
            .OrderByDescending(f => f.CreationDate).AsQueryable();

        var s = result.ToList();

        if (filterParams.CommentType != null)
            result = result.Where(d => d.CommentType == filterParams.CommentType);

        if (filterParams.EntityId != null)
            result = result.Where(f => f.EntityId == filterParams.EntityId);

        if (filterParams.StartDate.HasValue)
            result = result.Where(s => s.CreationDate.Date >= filterParams.StartDate.Value.Date);

        if (filterParams.EndDate.HasValue)
            result = result.Where(s => s.CreationDate.Date <= filterParams.EndDate.Value.Date);

        if (!string.IsNullOrWhiteSpace(filterParams.Name))
            result = result.Where(d => d.User.Name != null && d.User.Name.Contains(filterParams.Name));

        if (!string.IsNullOrWhiteSpace(filterParams.Family))
            result = result.Where(d => d.User.Family != null && d.User.Family.Contains(filterParams.Family));

        var skip = (filterParams.PageId - 1) * filterParams.Take;
        var model = new CommentFilterResult
        {
            Datas = await result.Skip(skip).Take(filterParams.Take).Select(s => new CommentDto()
            {
                Id = s.Id,
                CreationDate = s.CreationDate,
                IsDelete = s.IsDelete,
                Text = s.Text,
                FullName = $"{s.User.Name + s.User.Family}",
                Email = s.User.Email,
                EntityId = s.EntityId,
                UserId = s.UserId,
                IsActive = s.IsActive,
                CommentType = s.CommentType,
                Replies = s.Replies.Select(e => new CommentReplyDto
                {
                    Id = e.Id,
                    CreationDate = e.CreationDate,
                    IsDelete = e.IsDelete,
                    Text = e.Text,
                    FullName = $"{e.User.Name + e.User.Family}",
                    Email = e.User.Email,
                    PrentId = e.PrentId,
                    EntityId = e.EntityId,
                    UserId = e.UserId,
                    IsActive = e.IsActive,
                    CommentType = e.CommentType
                }).ToList()
            }).ToListAsync(),
            FilterParams = filterParams
        };
        model.GeneratePaging(result, filterParams.Take, filterParams.PageId);
        return model;
    }

    public async Task<AllCommentFilterResult> GetAllCommentByFilter(CommentFilterParams filterParams)
    {
        var result = context.Comments
            .Include(s => s.Replies)
            .ThenInclude(d => d.User)
            .Include(n => n.User)
            .OrderByDescending(f => f.CreationDate).AsQueryable();

        if (filterParams.CommentType != null)
            result = result.Where(d => d.CommentType == filterParams.CommentType);

        if (filterParams.EntityId != null)
            result = result.Where(f => f.EntityId == filterParams.EntityId);

        if (filterParams.StartDate.HasValue)
            result = result.Where(s => s.CreationDate.Date >= filterParams.StartDate.Value.Date);

        if (filterParams.EndDate.HasValue)
            result = result.Where(s => s.CreationDate.Date <= filterParams.EndDate.Value.Date);

        if (!string.IsNullOrWhiteSpace(filterParams.Name))
            result = result.Where(d => d.User.Name != null && d.User.Name.Contains(filterParams.Name));

        if (!string.IsNullOrWhiteSpace(filterParams.Family))
            result = result.Where(d => d.User.Family != null && d.User.Family.Contains(filterParams.Family));

        var skip = (filterParams.PageId - 1) * filterParams.Take;
        var model = new AllCommentFilterResult
        {
            Datas = await result.Skip(skip).Take(filterParams.Take).Select(e => new CommentReplyDto
            {
                Id = e.Id,
                CreationDate = e.CreationDate,
                IsDelete = e.IsDelete,
                Text = e.Text,
                FullName = $"{e.User.Name + e.User.Family}",
                Email = e.User.Email,
                PrentId = e.PrentId,
                EntityId = e.EntityId,
                UserId = e.UserId,
                IsActive = e.IsActive,
                CommentType = e.CommentType
            }).ToListAsync()
        };
        model.GeneratePaging(result, filterParams.Take, filterParams.PageId);
        return model;
    }
}