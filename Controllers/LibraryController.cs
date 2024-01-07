using DecaBlogMVC.Models;
using DecaBlogMVC.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace DecaBlogMVC.Controllers;

[Authorize]
[Route("library")]
public class LibraryController : Controller
{
    private readonly LibraryPageService _libraryPageService;
    private readonly TagService _tagService;
    private readonly AuthorService _authorService;
    private readonly BlogPostService _blogPostService;

    public LibraryController(LibraryPageService libraryPageService, TagService tagService, AuthorService authorService,
        BlogPostService blogPostService)
    {
        _libraryPageService = libraryPageService;
        _tagService = tagService;
        _authorService = authorService;
        _blogPostService = blogPostService;
    }

    [HttpGet("")]
    public async Task<IActionResult> SavedArticles()
    {
        var topAuthors = (await _authorService.GetAllAuthorsAsync()).Take(3);
        var allTags = await _tagService.AllTags();
        var recentBlogPost = _blogPostService.GetRecommendedArticles().Result.Take(3);
        var model = new SavedArticlesVM()
        {
            SavedArticles = await _libraryPageService.SavedArticles(),
            TopAuthors = topAuthors,
            AllTags = allTags,
            RecentBlogPosts = recentBlogPost
        };
        return View(model);
    }

    [HttpGet("reading-history")]
    public async Task<IActionResult> ReadingHistory()
    {
        var topAuthors = (await _authorService.GetAllAuthorsAsync()).Take(3);
        var allTags = await _tagService.AllTags();
        var recentBlogPost = _blogPostService.GetRecommendedArticles().Result.Take(3);
        var model = new ReadArticlesVM()
        {
            ReadArticles = await _libraryPageService.ReadArticles(),
            TopAuthors = topAuthors,
            AllTags = allTags,
            RecentBlogPosts = recentBlogPost
        };
        return View(model);
    }
}