using SocioVerse.Application.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SocioVerse.Application.Interfaces
{
    public interface IPostService
    {
        Task CreatePost(PostDTO post);
        Task UpdatePost(PostDTO post);
        Task<IEnumerable<PostDTO>> GetPosts();
        Task<PostDTO> GetPostById(int id);
        Task<IEnumerable<PostDTO>> GetPostByUser(int userId);
        Task AddLike(int postid, int userid);
        Task<IEnumerable<LikeDTO>> GetLikeByPost(int postId);
        Task AddComment(CommentDTO comment);
        Task EditComment(CommentDTO comment);
        Task<IEnumerable<CommentDTO>> GetCommentsByPost(int postId);
    }
}
