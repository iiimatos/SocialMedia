using Microsoft.EntityFrameworkCore;
using SocialMedia.Core.Entities;
using SocialMedia.Core.Interfaces;
using SocialMedia.Infrastructure.Data;
 
namespace SocialMedia.Infrastructure.Repositories
{
    public class PostRepository : BaseRepository<Post>, IPostRepository
    {
        public PostRepository(SocialMediaContext context) : base(context) { }

        public async Task<IEnumerable<Post>> GetPostsByUser(int id)
        {
            return await _entities.Where(p => p.UserId == id).ToListAsync();
        }
    }
}
