using DecaBlogMVC.Models;

namespace DecaBlogMVC.Services.Interfaces
{
    public interface ITagService
    {
        Task<IEnumerable<GetAllTagsViewModel>> GetAllTagsAsync();
    }
}
