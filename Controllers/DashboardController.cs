using DecaBlogMVC.Models;
using DecaBlogMVC.Models.Components;
using DecaBlogMVC.Services;
using DecaBlogMVC.Utilities;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace DecaBlogMVC.Controllers;

[Route("dashboard")]
public class DashboardController : Controller
{
    private readonly LibraryPageService _libraryPageService;
    private readonly BlogPostService _blogPostService;
    private readonly AuthorService _authorService;
    private readonly StoryPageService _storyPageService;
    private readonly TagService _tagService;

    public DashboardController(LibraryPageService libraryPageService, BlogPostService blogPostService,
        AuthorService authorService, TagService tagService, StoryPageService storyPageService)
    {
        _libraryPageService = libraryPageService;
        _blogPostService = blogPostService;
        _authorService = authorService;
        _tagService = tagService;
        _storyPageService = storyPageService;
    }

    [HttpGet("")]
    public async Task<IActionResult> Index([FromQuery] int page = 1)
    {
        var latestPosts = await _blogPostService.RecentPosts(page);
        var model = new DashboardViewModel
        {
            LatestBlogPosts = latestPosts.PageItems,
            TrendingPosts = await _blogPostService.TrendingPosts(),
            PopularBlogPosts = await _blogPostService.PopularPosts(),
            Authors = (await _authorService.GetAllAuthorsAsync()).Take(3),
            Tags = await _tagService.AllTags(),
            Pagination = new PaginationVM
            {
                NextPage = latestPosts.NextPage,
                PreviousPage = latestPosts.PreviousPage
            }
        };

        return View(model);
    }

    [HttpGet("settings")]
    [Authorize]
    public IActionResult Settings()
    {
        return View();
    }

    [Authorize]
    [HttpGet("story")]
    public async Task<IActionResult> StoryPage()
    {
        var userId = User.Claims.FirstOrDefault(c => c.Type == "Id").Value;
        var authorArticles = (await _authorService.GetAuthorById(userId))
            .Select(blog => new StoryArticlesVM
            {
                Id = blog.Id,
                ImageUrl = blog.ImageUrl,
                Tag = new TagViewModel
                {
                    Id = blog.TagId,
                    Name = blog.TagName
                },
                Title = blog.Title,
                Text = blog.Text,
                CreatedOn = blog.CreatedOn.FormatDate(),
                Status = blog.ApprovalStatus
            });
        var pendingArticles = authorArticles.Where(article => article.Status == ApprovalStatusConstant.Pending);
        var publishedArticles = authorArticles.Where(article => article.Status == ApprovalStatusConstant.IsPublished);
        var rejectedArticles = authorArticles.Where(article => article.Status == ApprovalStatusConstant.IsRejected);
        var topAuthors = (await _authorService.GetAllAuthorsAsync()).Take(3);
        var allTags = await _tagService.AllTags();
        var recentBlogPost = (await _blogPostService.GetRecommendedArticles()).Take(3);

        var pageModel = new StoryPageVM
        {
            RejectedArticles = rejectedArticles,
            PublishedArticles = publishedArticles,
            PendingArticles = pendingArticles,
            TopAuthors = topAuthors,
            AllTags = allTags,
            RecentBlogPosts = recentBlogPost
        };

        return View(pageModel);
    }

    [Authorize]
    [HttpGet("library")]
    public async Task<IActionResult> LibraryPage()
    {
        var topAuthors = (await _authorService.GetAllAuthorsAsync()).Take(3);
        var allTags = await _tagService.AllTags();
        var recentBlogPost = _blogPostService.GetRecommendedArticles().Result.Take(3);
        var pageModel = new LibraryPageVM
        {
            SavedArticles = await _libraryPageService.SavedArticles(),
            ReadArticles = await _libraryPageService.ReadArticles(),
            TopAuthors = topAuthors,
            AllTags = allTags,
            RecentBlogPosts = recentBlogPost
        };

        return View(pageModel);
    }
}