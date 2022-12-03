using Microsoft.EntityFrameworkCore;
using SocialMedia.Core.Entities;
using SocialMedia.Core.Interfaces;
using SocialMedia.Infrastructure.Data;

namespace SocialMedia.Infrastructure.Repositories
{
    public class SecurityRepository : BaseRepository<Security>, ISecurityRepository
    {
        public SecurityRepository(SocialMediaContext context) : base(context) { }
        public async Task<Security> GetLoginByCredential(UserLogin login)
        {
            return await _entities.FirstOrDefaultAsync(x => x.User == login.User);
        }

        public async Task<bool> UserExist(Security security)
        {
            return await _entities.Where(x => x.User == security.User).FirstOrDefaultAsync() != null;
        }
    }
}
