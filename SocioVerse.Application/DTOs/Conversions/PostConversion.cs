using SocioVerse.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SocioVerse.Application.DTOs.Conversions
{
    public static class PostConversion
    {
        public static Post ToEntity(PostDTO post) => new Post
        {
            PostId = post.PostId,
            Content = post.Content,
            AuthorId = post.AuthorId,
            CreatedAt = post.CreatedAt,
        };

        public static (PostDTO?, IEnumerable<PostDTO>?) FromEntity(Post post, IEnumerable<Post>? posts)
        {
            if (post is not null || posts is null)
            {
                var _post = new PostDTO(post!.PostId, post.Content, post.CreatedAt, post.AuthorId);
                return (_post, null);
            }

            if (post is null || post is not null)
            {
                var _posts = posts.Select(s => new PostDTO(s.PostId, s.Content, s.CreatedAt, s.AuthorId)).ToList();
                return (null, _posts);
            }

            return (null, null);
        }
    }
}
