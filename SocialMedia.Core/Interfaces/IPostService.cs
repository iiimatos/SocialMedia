﻿using SocialMedia.Core.Entities;

namespace SocialMedia.Core.Interfaces
{
    public interface IPostService
    {
        Task<IReadOnlyList<Post>> GetPosts();
        Task<Post> GetPostById(int id);
        Task InsertPost(Post post);
        Task<bool> UpdatePost(Post post);
        Task<bool> DeletePost(int id);
    }
}