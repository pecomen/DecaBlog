using System.Threading.Tasks;
using DecaBlogMVC.Models;
using DecaBlogMVC.Services;
using DecaBlogMVC.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace DecaBlogMVC.Controllers
{
    public class SearchController : Controller
    {
        private readonly ArticleService _searchService;
        private readonly BlogPostService _blogPostService;
        private readonly AuthorService _authorService;
        private readonly TagService _tagService;

        public SearchController(ArticleService searchService, BlogPostService blogPostService, AuthorService authorService, TagService tagService)
        { 
            _searchService = searchService;
            _blogPostService = blogPostService;
            _authorService = authorService;
            _tagService = tagService;
        }

        

        [HttpPost]
        public async Task<IActionResult> Index(string searchTerm)
        {
            if (string.IsNullOrEmpty(searchTerm))
            {
                return RedirectToAction("SearchResultPage");
            }

            var searchResults = await _searchService.SearchArticles(searchTerm);

            var model = new SearchResultsViewModel
            {
                SearchTerm = searchTerm,
                SearchResults = searchResults,
                PopularBlogPosts = await _blogPostService.PopularPosts(),
                Authors = (await _authorService.GetAllAuthorsAsync()).Take(3),
                Tags = await _tagService.AllTags(),
            };

            return View("SearchResultPage", model);
        }
    
       

        [HttpGet]
        public async Task<IActionResult> SearchResultPage(string searchTerm)
        {
            
            if (string.IsNullOrEmpty(searchTerm))
            {
                return RedirectToAction("Index");
            }
            var model = new SearchResultsViewModel
            {
                SearchTerm = searchTerm,
                SearchResults = new List<SearchArticlesViewModel>(),
                PopularBlogPosts = await _blogPostService.PopularPosts(),
                Authors = (await _authorService.GetAllAuthorsAsync()).Take(3),
                Tags = await _tagService.AllTags(),
            };
         
            return View(model);
        }
    }
}
