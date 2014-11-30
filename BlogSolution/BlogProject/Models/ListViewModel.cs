using BlogProject.Core;
using BlogProject.Core.Objects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace BlogProject.Models
{
    public class ListViewModel
    {
        public ListViewModel(IBlogRepository blogRepository, int p)
        {
            Posts = blogRepository.Posts(p - 1, 10);
            TotalPosts = blogRepository.TotalPosts();
        }
        public IList<Post> Posts { get; private set; }
        public int TotalPosts { get; private set; }
    }
}