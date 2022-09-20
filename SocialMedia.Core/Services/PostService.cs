﻿using SocialMedia.Core.Entities;
using SocialMedia.Core.Enumerations;
using SocialMedia.Core.Interfaces;
using SocialMedia.Core.QueryFilters;

namespace SocialMedia.Core.Services
{
    public class PostService : IPostService
    {
        private readonly IUnitOfWork _unitOfWork;

        public PostService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public IEnumerable<Post> GetPosts(PostQueryFilter filters)
        {
            var posts = _unitOfWork.PostRepository.GetAll();

            if(filters.UserId != null)
            {
                posts = posts.Where(x => x.UserId == filters.UserId);
            }

            if(filters.Date != null)
            {
                posts = posts.Where(x => x.Date.ToShortDateString() == filters.Date.Value.ToShortDateString());
            }

            if(filters.Description != null)
            {
                posts = posts.Where(x => x.Description.ToLower().Contains(filters.Description.ToLower()));
            }

            return posts;
        }

        public async Task<Post> GetPostById(int id)
        {
            return await _unitOfWork.PostRepository.GetByIdAsync(id);
        }

        public async Task InsertPost(Post post)
        {
            var user = await _unitOfWork.UserRepository.GetByIdAsync(post.UserId);
            if (user == null) throw new BusinessException("User doesn't exist.");
            var userPost = await _unitOfWork.PostRepository.GetPostsByUser(post.UserId);
            if (userPost.Count() < 10)
            {
                var lastPost = userPost.OrderByDescending(x => x.Date).FirstOrDefault();
                if((DateTime.Now - lastPost.Date).TotalDays < 7) throw new BusinessException("You are not able to publish the post.");

            }
            if (post.Description.Contains("Sexo")) throw new BusinessException("Content not allowed");
            await _unitOfWork.PostRepository.AddAsync(post);
            await _unitOfWork.SaveChangesAsync();
        }

        public async Task<bool> UpdatePost(Post post)
        {
            _unitOfWork.PostRepository.Update(post);
            await _unitOfWork.SaveChangesAsync();
            return true;
        }

        public async Task<bool> DeletePost(int id)
        {
            await _unitOfWork.PostRepository.DeleteAsync(id);
            await _unitOfWork.SaveChangesAsync();
            return true;
        }
    }
}
