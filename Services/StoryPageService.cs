using DecaBlogMVC.Models;
using DecaBlogMVC.Models.Api;
using DecaBlogMVC.Models.Components;
using DecaBlogMVC.Services.Interfaces;
using DecaBlogMVC.Utilities;

namespace DecaBlogMVC.Services
{
    public class StoryPageService : BaseService
    {
        private readonly BlogPostService _blogPostService;
        private readonly HttpClientService _httpClientService;

        public StoryPageService(HttpClient client, IHttpContextAccessor httpContextAccessor, IConfiguration config,
            BlogPostService blogPostService, HttpClientService httpClientService) : base(client, httpContextAccessor,
            config)
        {
            _blogPostService = blogPostService;
            _httpClientService = httpClientService;
        }

        public async Task<IEnumerable<StoryArticlesVM>> GetBookmarkedArticles()
        {
            const string address = "/api/articles/get-bookmarked-articles";

            var res = await _httpClientService.GetAsync<ResponseObject<IEnumerable<BlogPostVM>>>(address);

            return res.Data.Select(article => new StoryArticlesVM
            {
                Id = article.Id,
                ImageUrl = article.ImageUrl,
                Tag = new TagViewModel
                {
                    Id = article.TagId,
                    Name = article.TagName
                },
                Title = article.Title,
                Text = article.Text,
                CreatedOn = article.CreatedOn.FormatDate(),
            });
        }

        public async Task<IEnumerable<StoryArticlesVM>> PendingArticlesAsync()
        {
            return (await _blogPostService.GetAllArticles(new FilterArticleDto
            {
                Size = 4
            })).PageItems.Select(blog => new StoryArticlesVM
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
            });
        }
    }
}