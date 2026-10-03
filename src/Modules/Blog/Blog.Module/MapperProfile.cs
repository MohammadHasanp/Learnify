using AutoMapper;
using BlogModule.Domain;
using BlogModule.Services.DTOs.Command;
using BlogModule.Services.DTOs.Query;

namespace BlogModule;

class MapperProfile : Profile
{
    public MapperProfile()
    {
        CreateMap<Category, CreateCategoryCommand>().ReverseMap();
        CreateMap<Category, EditCategoryCommand>().ReverseMap();
        CreateMap<Category, BlogCategoryDto>().ReverseMap();
        CreateMap<Post, CreatePostCommand>().ReverseMap();
        CreateMap<Post, EditPostCommand>().ReverseMap();
        CreateMap<Post, BlogPostDto>().ReverseMap();
    }
}
