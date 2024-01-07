using DecaBlogMVC.Models;
using DecaBlogMVC.Models.Api;

namespace DecaBlogMVC.Services.Interfaces
{
    public interface IArticleService
    {
        Task<CreateArticleReturnDto?> Create(CreateArticleDto article);

        Task<UpdateArticleDto> GetArticleById(int id);

        Task<bool> UpdateArticle(int articleId, UpdateArticleDto updatedArticle);

        Task<IEnumerable<SearchArticlesViewModel>> SearchArticles(string searchTerm);
    }
}
