using SocialMedia.Core.Entities;
using SocialMedia.Core.Exceptions;
using SocialMedia.Core.Interfaces;
 
namespace SocialMedia.Core.Services
{
    public class SecurityService : ISecurityService
    {
        private readonly IUnitOfWork _unitOfWork;

        public SecurityService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<Security> GetLoginByCredentials(UserLogin userLogin)
        {
            return await _unitOfWork.SecurityRepository.GetLoginByCredential(userLogin);
        }

        public async Task RegisterUser(Security security)
        {
            var userExist = await _unitOfWork.SecurityRepository.UserExist(security);
            if (userExist) throw new BusinessException($"This user {security.User} exist.");
            await _unitOfWork.SecurityRepository.AddAsync(security);
            await _unitOfWork.SaveChangesAsync();
        }
    }
}
