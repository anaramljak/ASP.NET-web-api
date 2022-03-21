using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using ReviewerDomain.Models;
using ReviewerAPI.Data.Repository;
using ReviewerAPI.Data.FileManager;
using ReviewerAPI.ViewsModels;

namespace ReviewerAPI.Controllers
{
    public class HomeController : Controller
    {
        private IRepository _repo;
        private IFileManager _fileManager;
        public HomeController(IRepository repo, IFileManager fileManager)
        {
            _repo = repo;
            _fileManager = fileManager;

        }
        public IActionResult Index(int pageNumber,string category,string search)
        {

            if (pageNumber < 1)
                return RedirectToAction("Index", new { pageNumber = 1, category, search });

            var vm = _repo.GetAllPosts(pageNumber,category,search); 

            return View(vm);
        }

        public IActionResult Post(int id)
        { 
            var post = _repo.GetPost(id);
            return View(post);
        }

      

        [HttpGet("/Image/{image}")]
        public IActionResult Image(string image)
        {
            var mime = image.Substring(image.LastIndexOf('.') + 1);

            return new FileStreamResult(_fileManager.ImageStream(image), $"image/{mime}");
        }

        [HttpPost]
        public async Task<IActionResult> Review (ReviewViewModel vm)
        {
           
            if (!ModelState.IsValid)
                return RedirectToAction("Post", new { id = vm.PostId });
            
            var post = _repo.GetPost(vm.PostId);
            if (vm.ReviewId == 0)
            {
                post.Review = post.Review ?? new List<Review>();

                post.Review.Add(new Review
                {
                    Message = vm.Message,
                    Created = DateTime.Now,
                    Rating = vm.Rating,
                    UserId = vm.UserId
                    
                });

                _repo.UpdatePost(post);
            }
            else
            {
                var comment = new Comment
                {
                    ReviewId = vm.ReviewId,
                    Message = vm.Message,
                    Created = DateTime.Now,
                    UserId = vm.UserId

                };
                _repo.AddComment(comment);
            }

            await _repo.SaveChangesAsync();

            return RedirectToAction("Post", new { id = vm.PostId });
        }

        
    }

}

