namespace CommentModule;

using AutoMapper;
using Domain;
using Services.DTOs;

public class CommentProfile : Profile
{
    public CommentProfile()
    {
        CreateMap<Comment, CommentDto>().ReverseMap();
    }
}