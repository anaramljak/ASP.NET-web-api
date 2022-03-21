using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ReviewerDomain.Models;
using ReviewerAPI.ViewsModels;

namespace ReviewerAPI.Data.Repository
{
    public interface IRepository    
    {
        Post GetPost(int id);
        List<Post> GetAllPosts();
        IndexViewModel GetAllPosts(int pageNumber,string category,string search);
        void AddPost(Post post);
        void UpdatePost(Post post);
        void RemovePost(int id);
        void AddComment(Comment comment);

        
        Task<bool> SaveChangesAsync();
        
        
    }
}
