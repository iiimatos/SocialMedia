using SocialMedia.Core.Entities;
using SocialMedia.Core.Interfaces;

namespace SocialMedia.Core.Services
{
    public class PostService : IPostService
    {
        private readonly IUnitOfWork _unitOfWork;

        public PostService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<Post> GetPostById(int id)
        {
            return await _unitOfWork.PostRepository.GetByIdAsync(id);
        }

        public async Task<IReadOnlyList<Post>> GetPosts()
        {
            return await _unitOfWork.PostRepository.GetAllAsync();
        }

        public async Task InsertPost(Post post)
        {
            var user = await _unitOfWork.UserRepository.GetByIdAsync(post.UserId);
            if (user == null) throw new Exception("User doesn't exist.");
            if (post.Description.Contains("Sexo")) throw new Exception("Content not allowed");
            await _unitOfWork.PostRepository.AddAsync(post);
        }

        public async Task<bool> UpdatePost(Post post)
        {
            await _unitOfWork.PostRepository.UpdateAsync(post);
            return true;
        }

        public async Task<bool> DeletePost(int id)
        {
            await _unitOfWork.PostRepository.DeleteAsync(id);
            return true;
        }
    }
}
