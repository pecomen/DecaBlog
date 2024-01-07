using DecaBlogMVC.Models;
using DecaBlogMVC.Models.Components;
using DecaBlogMVC.Services;
using DecaBlogMVC.Utilities;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace DecaBlogMVC.Controllers;

[Authorize]
[Route("story")]
public class StoryController: Controller
{
    private readonly LibraryPageService _libraryPageService;
    private readonly TagService _tagService;
    private readonly AuthorService _authorService;
    private readonly BlogPostService _blogPostService;
    
    public StoryController(LibraryPageService libraryPageService, TagService tagService, AuthorService authorService,
        BlogPostService blogPostService)
    {
        _libraryPageService = libraryPageService;
        _tagService = tagService;
        _authorService = authorService;
        _blogPostService = blogPostService;
    }

    [HttpGet("")]
    public async Task<IActionResult> PublishedArticles()
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
        var publishedArticles = authorArticles.Where(article => article.Status == ApprovalStatusConstant.IsPublished);
        var topAuthors = (await _authorService.GetAllAuthorsAsync()).Take(3);
        var allTags = await _tagService.AllTags();
        var recentBlogPost = (await _blogPostService.GetRecommendedArticles()).Take(3);

        var model = new PublishedArticlesVM
        {
            PublishedArticles = publishedArticles,
            TopAuthors = topAuthors,
            AllTags = allTags,
            RecentBlogPosts = recentBlogPost
        };
        return View(model);
    }

    [HttpGet("pending-articles")]
    public async Task<IActionResult> PendingArticles()
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
        var topAuthors = (await _authorService.GetAllAuthorsAsync()).Take(3);
        var allTags = await _tagService.AllTags();
        var recentBlogPost = (await _blogPostService.GetRecommendedArticles()).Take(3);

        var model = new PendingArticlesVM()
        {
            PendingArticles = pendingArticles,
            TopAuthors = topAuthors,
            AllTags = allTags,
            RecentBlogPosts = recentBlogPost
        };
        return View(model);
    }

    [HttpGet("rejected-articles")]
    public async Task<IActionResult> RejectedArticles()
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
        var rejectedArticles = authorArticles.Where(article => article.Status == ApprovalStatusConstant.IsRejected);
        var topAuthors = (await _authorService.GetAllAuthorsAsync()).Take(3);
        var allTags = await _tagService.AllTags();
        var recentBlogPost = (await _blogPostService.GetRecommendedArticles()).Take(3);

        var model = new RejectedArticlesVM
        {
            RejectedArticles = rejectedArticles,
            TopAuthors = topAuthors,
            AllTags = allTags,
            RecentBlogPosts = recentBlogPost
        };
        return View(model);
    }
}