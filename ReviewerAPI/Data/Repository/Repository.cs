using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ReviewerDomain.Models;
using Microsoft.EntityFrameworkCore;
using ReviewerAPI.ViewsModels;


namespace ReviewerAPI.Data.Repository
{
    public class Repository : IRepository
    {
        private AppDbContext _ctx;
        public Repository(AppDbContext ctx)
        {
            _ctx = ctx;
        }
        public Post GetPost(int id)
        {
            return _ctx.Posts
                .Include(p => p.Review)
                    .ThenInclude(rw => rw.Comments)
               .FirstOrDefault(p => p.Id == id);
        }
        public List<Post> GetAllPosts()
        {
            return _ctx.Posts
                .ToList();
        }


        public IndexViewModel GetAllPosts(int pageNumber,string category,string search)
        {

            int postNum = 3;
            int skipAmount = postNum * (pageNumber - 1);
            var _post = _ctx.Posts.AsNoTracking().AsEnumerable();
           
           /* var _post = _ctx.Posts.AsNoTracking().AsEnumerable().Where(post => post.Category.ToLower().Equals(category.ToLower()))
                             .Where(x => x.Title.Contains(search)
                                    || x.Body.Contains(search)
                                    || x.Description.Contains(search));*/

             if (!String.IsNullOrEmpty(category))
                 _post = _post.Where(post => post.Category.ToLower().Equals(category.ToLower()));

            if (!String.IsNullOrEmpty(search))
                _post = _post.Where(x  => x.Title.Contains(search)
                                    || x.Body.Contains(search)
                                    || x.Description.Contains(search));



            int postsCount = _post.Count();
            

            return new IndexViewModel
            {
                PageNumber = pageNumber,
                NextPage = postsCount > skipAmount + postNum,
                Category = category,
                Search = search,
                Posts = _post
                    .Skip(skipAmount)
                    .Take(postNum)
                    .ToList()
            };
        }
        public void AddPost(Post post)
        {
            _ctx.Posts.Add(post);
        }
        public void UpdatePost(Post post)
        {
            _ctx.Posts.Update(post);
        }
        public void RemovePost(int id)
        {
            _ctx.Posts.Remove(GetPost(id));
        }

        public void AddComment(Comment comment)
        {
            _ctx.Comments.Add(comment);
        }

       
        public async Task<bool> SaveChangesAsync()
        {
            if (await _ctx.SaveChangesAsync() > 0)
            {
                return true;
            }
            return false;
        }
      
    }
}
