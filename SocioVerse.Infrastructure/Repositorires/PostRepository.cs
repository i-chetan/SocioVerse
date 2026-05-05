using Microsoft.EntityFrameworkCore;
using SocioVerse.Application.DTOs;
using SocioVerse.Application.DTOs.Conversions;
using SocioVerse.Application.Interfaces;
using SocioVerse.Domain.Entities;
using SocioVerse.Infrastructure.Persistence;

namespace SocioVerse.Infrastructure.Repositorires
{
    public class PostRepository : IPostService
    {
        private readonly SocialMediaDbContext _context;
        public PostRepository(SocialMediaDbContext context) => _context = context;
        public async Task AddComment(CommentDTO comment)
        {
            var _comment = new Comment { 
                PostId = comment.PostId,
                UserId = comment.UserId,
                Content = comment.Content,
                CreatedAt = DateTime.UtcNow,
            };

            _context.Comments.Add(_comment);
            await _context.SaveChangesAsync();
        }

        public async Task AddLike(int postid, int userid)
        {
            var like = new Like
            {
                PostId = postid,
                UserId = userid,
                CreatedAt = DateTime.UtcNow
            };

            _context.Likes.Add(like);

            await _context.SaveChangesAsync();
        }

        public async Task CreatePost(PostDTO post)
        {
            var _post = PostConversion.ToEntity(post);
            _post.CreatedAt = DateTime.UtcNow;

            _context.Posts.Add(_post);
            await _context.SaveChangesAsync();
        }

        public async Task EditComment(CommentDTO comment)
        {
            var _comment = await _context.Comments.FirstOrDefaultAsync(x => x.CommentId == comment.Id);

            if (_comment == null) return;

            _comment.Content = comment.Content;
            _comment.CreatedAt = DateTime.UtcNow;

            await _context.SaveChangesAsync();
        }

        public async Task<IEnumerable<CommentDTO>> GetCommentsByPost(int postId)
        {
            var comments = await _context.Comments.Where(x => x.PostId == postId)
                .Select(s => new CommentDTO(s.CommentId, s.PostId, s.UserId, s.Content, s.CreatedAt))
                .ToListAsync();

            return comments ?? [];
        }

        public async Task<IEnumerable<LikeDTO>> GetLikeByPost(int postId)
        {
            var likes = await _context.Likes.Where(x => x.PostId == postId)
                .Select(s => new LikeDTO(s.Id , s.UserId, s.PostId, s.CreatedAt)).ToListAsync();

            return likes ?? [];
        }

        public async Task<PostDTO> GetPostById(int id)
        {
            var _post = await _context.Posts.FirstOrDefaultAsync(x => x.PostId == id);
            if (_post == null) return null!;

            var (post, _) = PostConversion.FromEntity(_post, null);
            return post!;
        }

        public async Task<IEnumerable<PostDTO>> GetPostByUser(int userId)
        {
            var posts = await _context.Posts.Where(x => x.AuthorId == userId).ToListAsync();

            if (posts == null) return [];

            var (_, _posts) = PostConversion.FromEntity(null!, posts);
            return _posts ?? [];
        }

        public async Task<IEnumerable<PostDTO>> GetPosts()
        {
            var posts = await _context.Posts.ToListAsync();

            if (posts == null) return [];

            var (_, _posts) = PostConversion.FromEntity(null!, posts);
            return _posts ?? [];
        }

        public async Task UpdatePost(PostDTO post)
        {
            var _post = await _context.Posts.FirstOrDefaultAsync(x => x.PostId == post.PostId);

            if (_post == null) return;

            _post.Content = post.Content;
            _post.CreatedAt = DateTime.UtcNow;

            await _context.SaveChangesAsync();
        }
    }
}
