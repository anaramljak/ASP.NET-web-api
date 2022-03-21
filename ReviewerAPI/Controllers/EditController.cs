using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using ReviewerAPI.Data.Repository;
using Microsoft.AspNetCore.Authorization;
using ReviewerDomain.Models;
using ReviewerAPI.ViewsModels;
using ReviewerAPI.Data.FileManager;


namespace ReviewerAPI.Controllers
{
    [Authorize(Roles = "Admin")]
    public class EditController : Controller
    {
           
           private IRepository _repo;
           private IFileManager _fileManager;

            public EditController(IRepository repo, IFileManager fileManager)
            {
                _repo = repo;
                 _fileManager = fileManager;
            }
          
            public IActionResult Index()
            {
               var posts = _repo.GetAllPosts();
                return View(posts);
            }

        [HttpGet]
        public IActionResult Edit(int? id)
        {
            if (id == null)
            {

                return View(new PostViewModel());
            }
            else
            {
                var post = _repo.GetPost((int)id);
                return View(new PostViewModel
                {
                    Id = post.Id,
                    Title = post.Title,
                    Body = post.Body,
                    CurrentImage=post.Image,
                    Description = post.Description,
                    Tags = post.Tags,
                    Category = post.Category,
                    

                });
            }

        }

        [HttpPost]
        public async Task<IActionResult> Edit(PostViewModel vm)
        {
            var post = new Post
            {
                Id = vm.Id,
                Title = vm.Title,
                Body = vm.Body,
                Description = vm.Description,
                Tags = vm.Tags,
                Category = vm.Category,
                Image = await _fileManager.SaveImage(vm.Image)
        };

            if (post.Id > 0)
                _repo.UpdatePost(post);
            else
                _repo.AddPost(post);

            if (await _repo.SaveChangesAsync())
                return RedirectToAction("Index","Home");
            else
                return View(vm);
        }

        [HttpGet]
        public async Task<IActionResult> Remove(int id)
        {
            _repo.RemovePost(id);
            await _repo.SaveChangesAsync();
            return RedirectToAction("Index","Home");

        }
    }
}

