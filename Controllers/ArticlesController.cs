using DecaBlogMVC.Models;
using DecaBlogMVC.Models.Components;
using DecaBlogMVC.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore.Metadata.Internal;

namespace DecaBlogMVC.Controllers;

[Route("articles")]
public class ArticlesController : Controller
{
    private readonly BlogPostService _blogPostService;
    private readonly TagService _tagService;
    private readonly AuthorService _authorService;
    private readonly ArticleService _articleService;

    public ArticlesController(BlogPostService blogPostService, TagService tagService, AuthorService authorService,
        ArticleService articleService)
    {
        _blogPostService = blogPostService;
        _tagService = tagService;
        _authorService = authorService;
        _articleService = articleService;
    }

    [HttpGet("{id}")]
    public async Task<IActionResult> GetSingleArticle([FromRoute] string id)
    {
        var article = await _blogPostService.GetArticleById(id);

        ViewBag.ArticleId = id;
        
        var model = new SingleArticlePageVM
        {
            Article = article,
            Authors = (await _authorService.GetAllAuthorsAsync()).Take(3),
            Tags = await _tagService.AllTags(),
            MoreArticles = await _authorService.GetAuthorById(article.AuthorId),
        };

        return View(model);
    }

    [HttpGet("create")]
    public async Task<IActionResult> CreateArticle()
    {
        var tags = await _tagService.AllTags();

        var createArticleDto = new CreateArticleDto
        {
            Tags = tags,
        };

        return View(createArticleDto);
    }
    
    [HttpPost("create")]
    [Authorize]
    public async Task<IActionResult> CreateArticle([FromForm] CreateArticleDto article)
    {
        if(!ModelState.IsValid)
        {
            article.Tags = await _tagService.AllTags();
            return View(article);
        }
        var createdArticle = await _articleService.Create(article);

        if (createdArticle == null)
        {
            article.Tags = await _tagService.AllTags();
            ViewBag.ErrorMessage = "Failed to create article.";
            return View(article);
        }

        return View("CreateArticleSuccessful", createdArticle.Id);
    }
    
    [Authorize]
    [HttpPost("{articleId}/add-comment")]
    public async Task<IActionResult> AddComment([FromForm] AddCommentVM comment, string articleId)
    {
        if (!ModelState.IsValid)
        {
            return RedirectToAction("GetSingleArticle", new { id = articleId });
        }
        
        await _articleService.AddComment(comment, articleId);

        return RedirectToAction("GetSingleArticle", new {id = articleId});
    }
}