using DevsTutorialCenterAPI.Models.DTOs;
using DecaBlogMVC.Models;
using DecaBlogMVC.Models.Api;
using DecaBlogMVC.Models.Components;
using DecaBlogMVC.Services.Interfaces;

namespace DecaBlogMVC.Services
{
    public class ArticleService : BaseService
    {
        private readonly HttpClientService _httpClientService;
        private readonly ImageService _imageService;

        public ArticleService(HttpClient client, HttpClientService httpClientService, ImageService imageService,
            IHttpContextAccessor httpContextAccessor, IConfiguration config) : base(client, httpContextAccessor, config)
        {
            _httpClientService = httpClientService;
            _imageService = imageService;
        }

        public async Task<CreateArticleDtoReturn?> Create(CreateArticleDto article)
        {
            var imageUploadRes = await _imageService.UploadImage(article.Image);
            
            article.ImageUrl = imageUploadRes?.Url;
            article.PublicId = imageUploadRes?.PublicId;
            
            const string apiUrl = "api/articles/create-article";
            
            var result =
                await _httpClientService.PostAsync<CreateArticleDto, ResponseDto<CreateArticleDtoReturn>>(apiUrl,
                    article);
            if (!result.IsSuccessful)
                return null;

            return result.Data;
        }

        public async Task<UpdateArticleDto> GetArticleById(int id)
        {
            try
            {
                string apiUrl = $"get-single-article/{id}";

                var article = await MakeRequest<UpdateArticleDto, object>(apiUrl, "GET", data: null);

                return article;
            }
            catch (HttpRequestException)
            {
                return null;
            }
        }


        public async Task<bool> UpdateArticle(int articleId, UpdateArticleDto updatedArticle)
        {
            try
            {
                string apiUrl = $"api/articles/update-article/{articleId}";
                const string methodType = "PATCH";


                var result = await MakeRequest<bool, UpdateArticleDto>(apiUrl, methodType, updatedArticle);

                return result;
            }
            catch (Exception ex)
            {
                return false;
            }
        }
        public async Task<IEnumerable<SearchArticlesViewModel>> SearchArticles(string searchTerm)
        {
            var address = $"/api/articles/search-articles?searchTerm={searchTerm}";

            var result =
                await _httpClientService.GetAsync<ResponseDto<IEnumerable<SearchArticlesViewModel>>>(address);

            if (result?.Data == null)
                return Enumerable.Empty<SearchArticlesViewModel>();

            var articles = result.Data.Select(a => new SearchArticlesViewModel
            {
                Id = a.Id,
                Title = a.Title,
                Text = a.Text,
                AuthorId = a.AuthorId,
                AuthorName = a.AuthorName,
                AuthorImage = a.AuthorImage,
                TagId = a.TagId,
                TagName = a.TagName,
                ReadCount = a.ReadCount,
                ImageUrl = a.ImageUrl,
                PublicId = a.PublicId,
                ReadTime = a.ReadTime,
                IsDeleted = a.IsDeleted,
                CreatedOn = a.CreatedOn,
                IsRecentlyAdded = false, 
                IsTopRead = false
            });

            return articles;
        }

        public async Task<bool> AddComment(AddCommentVM commentDto, string articleId)
        {
            var apiUrl = $"api/comments/add-comment/{articleId}";
            
            var result =
                await _httpClientService.PostAsync<object, ResponseDto<object>>(apiUrl,
                    new
                    {
                        Text = commentDto.Text,
                    });
            if (!result.IsSuccessful) 
                return false;
            
            return true;
        }
    }
}