using BlogModule.Context;
using BlogModule.Domain;
using Common.Domain.Repository;
using Common.Infrastructure;

namespace BlogModule.Repositories.Posts;

interface IPostRepository : IBaseRepository<Post>
{
    public void Delete(Post post);
}

class PostRepository(BlogContext context) : BaseRepository<Post, BlogContext>(context), IPostRepository
{
    public void Delete(Post post)
    {
        Context.Posts.Remove(post);
    }
}