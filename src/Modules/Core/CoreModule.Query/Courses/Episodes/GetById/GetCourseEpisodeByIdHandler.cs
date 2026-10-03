using Common.Query;
using CoreModule.Query._Data;
using CoreModule.Query.Courses.Episodes.DTOs;
using Microsoft.EntityFrameworkCore;

namespace CoreModule.Query.Courses.Episodes.GetById;

internal class GetCourseEpisodeByIdHandler(QueryContext context) : IQueryHandler<GetCourseEpisodeCommand, EpisodeDto?>
{
    private readonly QueryContext _context = context;
    public async Task<EpisodeDto?> Handle(GetCourseEpisodeCommand request, CancellationToken cancellationToken)
    {
        var episode = await _context.Episodes.FirstOrDefaultAsync(e => e.Id == request.EpisodeId, cancellationToken);
        if (episode == null)
            return null;

        return new EpisodeDto
        {
            Id = episode.Id,
            CreationDate = episode.CreationDate,
            IsDelete = episode.IsDelete,
            SectionId = episode.SectionId,
            Title = episode.Title,
            EnglishTitle = episode.EnglishTitle,
            Token = episode.Token,
            Time = episode.Time,
            VideoName = episode.VideoName,
            AttachmentName = episode.AttachmentName,
            IsActive = episode.IsActive,
            IsFree = episode.IsFree
        };
    }
}