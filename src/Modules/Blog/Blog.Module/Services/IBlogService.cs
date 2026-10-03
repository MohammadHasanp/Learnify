using AutoMapper;
using BlogModule.Context;
using BlogModule.Domain;
using BlogModule.Repositories.Categories;
using BlogModule.Repositories.Posts;
using BlogModule.Services.DTOs.Command;
using BlogModule.Services.DTOs.Query;
using BlogModule.Utilities;
using BlogModule.Utilities;
using Common.Application;
using Common.Application.FileUtil.StorageInterfaces;
using Common.Application.FileUtil.Validations;
using Common.Application.SecurityUtil;
using Microsoft.EntityFrameworkCore;

namespace BlogModule.Services;

public interface IBlogService
{
    public Task<OperationResult> CreateCategory(CreateCategoryCommand command);
    public Task<OperationResult> EditCategory(EditCategoryCommand command);
    public Task<OperationResult> DeleteCategory(Guid id);
    public List<BlogCategoryDto?> GetAllCategories();
    public Task<BlogCategoryDto> GetCategoryById(Guid id);
    public Task AddPostVisit(Guid id);
    public Task<BlogPostFilterResult> GetPostByFilter(BlogPostFilterParams filterParams);

    public Task<OperationResult> CreatePost(CreatePostCommand command);
    public Task<OperationResult> EditPost(EditPostCommand command);
    public Task<OperationResult> DeletePost(Guid id);
    public Task<BlogPostDto?> GetPostById(Guid id);
    public Task<BlogPostDto?> GetPostBySlug(string slug);
}

class BlogService(ICategoryRepository repository, IMapper mapper, IPostRepository postRepository, ILocalFileService localFileService,
    BlogContext context) : IBlogService
{
    public async Task<OperationResult> CreateCategory(CreateCategoryCommand command)
    {
        var category = mapper.Map<Category>(command);
        if (await repository.ExistAsync(c => c.Slug == command.Slug))
            return OperationResult.Error("slug Is Exist");

        repository.Add(category);
        await repository.Save();
        return OperationResult.Success();
    }

    public async Task AddPostVisit(Guid id)
    {
        var post = await postRepository.GetAsync(id);
        if (post != null)
        {
            post.Visit += 1;

            postRepository.Update(post);
            await context.SaveChangesAsync();
        }
    }

    public async Task<BlogPostFilterResult> GetPostByFilter(BlogPostFilterParams filterParams)

    {
        var posts = context.Posts.Include(s => s.Category)
            .OrderByDescending(d => d.CreationDate).AsQueryable();

        if (filterParams.Search != null)
            posts = posts.Where(s => s.Title.Contains(filterParams.Search) || s.Description.Contains(filterParams.Search));

        if (filterParams.CategorySlug != null)
            posts = posts.Where(s => s.Category.Slug.Contains(filterParams.CategorySlug));

        var skip = (filterParams.PageId - 1) * filterParams.Take;
        var model = new BlogPostFilterResult
        {
            Datas = await posts.Skip(skip).Take(filterParams.Take).Select(s => new BlogPostFilterData
            {
                Id = s.Id,
                CreationDate = s.CreationDate,
                IsDelete = s.IsDelete,
                Title = s.Title,
                UserId = s.UserId,
                WriterName = s.WriterName,
                Description = s.Description,
                Slug = s.Slug,
                Visit = s.Visit,
                ImageName = s.ImageName,
                Category = new BlogCategoryDto
                {
                    Id = s.Category.Id,
                    Title = s.Category.Title,
                    Slug = s.Category.Slug
                }
            }).ToListAsync()
        };
        model.GeneratePaging(posts, filterParams.Take, filterParams.PageId);
        return model;
    }

    public async Task<OperationResult> CreatePost(CreatePostCommand command)
    {
        var post = mapper.Map<Post>(command);
        if (await postRepository.ExistAsync(p => p.Slug == command.Slug))
            return OperationResult.Error("Slug Is Exist");

        if (!command.ImageFile.IsImage())
            return OperationResult.Error("Image Invalid");

        var imageName = await localFileService.SaveFileAndGenerateName(command.ImageFile, BlogDirectories.PostImage);

        post.ImageName = imageName;
        post.Visit = 0;
        post.Description = command.Description;

        postRepository.Add(post);
        await postRepository.Save();
        return OperationResult.Success();
    }

    public async Task<OperationResult> DeleteCategory(Guid id)
    {
        var category = await repository.GetAsync(id);
        if (category == null)
            return OperationResult.NotFound();

        if (await postRepository.ExistAsync(p => p.CategoryId == id))
            return OperationResult.Error("این دسته بندی قبلا استفاده شده است ابتدا پست مربوط را پاک کرده بعد تلاش کنید");

        repository.Delete(category);
        await repository.Save();
        return OperationResult.Success();
    }

    public async Task<OperationResult> DeletePost(Guid id)
    {
        var post = await postRepository.GetAsync(id);
        if (post == null)
            return OperationResult.NotFound();

        postRepository.Delete(post);
        await postRepository.Save();
        localFileService.DeleteFile(BlogDirectories.PostImage, post.ImageName);
        return OperationResult.Success();
    }

    public async Task<OperationResult> EditCategory(EditCategoryCommand command)
    {
        var category = await repository.GetAsync(command.CategoryId);
        if (category == null)
            return OperationResult.NotFound();

        if (category.Slug != command.Slug)
        {
            if (await repository.ExistAsync(c => c.Slug == command.Slug))
                return OperationResult.Error("slug Is Exist");
        }

        category.Slug = command.Slug;
        category.Title = command.Title;

        repository.Update(category);
        await repository.Save();
        return OperationResult.Success();

    }

    public async Task<OperationResult> EditPost(EditPostCommand command)
    {
        var post = await postRepository.GetAsync(command.Id);
        if (post == null)
            return OperationResult.NotFound();

        if (post.Slug != command.Slug)
            if (await postRepository.ExistAsync(p => p.Slug == command.Slug))
                return OperationResult.Error("Slug Is Exist");

        if (command.ImageFile != null)
            if (!command.ImageFile.IsImage())
                return OperationResult.Error("image Invalid");

            else
            {
                var imageName = await localFileService.SaveFileAndGenerateName(command.ImageFile, BlogDirectories.PostImage);
                post.ImageName = imageName;
            }

        post.WriterName = command.WriterName;
        post.CategoryId = command.CategoryId;
        post.Slug = command.Slug;
        post.Title = command.Title;
        post.Description = command.Description;

        postRepository.Update(post);
        await postRepository.Save();
        return OperationResult.Success();

    }
    public List<BlogCategoryDto?> GetAllCategories()
    {
        var categories = repository.GetAll();
        return mapper.Map<List<BlogCategoryDto?>>(categories);
    }

    public async Task<BlogCategoryDto> GetCategoryById(Guid id)
    {
        var category = await repository.GetAsync(id);
        return mapper.Map<BlogCategoryDto>(category);
    }

    public async Task<BlogPostDto?> GetPostById(Guid id)
    {
        var post = await postRepository.GetAsync(id);
        return mapper.Map<BlogPostDto>(post);
    }

    public async Task<BlogPostDto?> GetPostBySlug(string slug)
    {
        var post = await context.Posts.FirstOrDefaultAsync(s => s.Slug == slug);
        return mapper.Map<BlogPostDto>(post);
    }
}