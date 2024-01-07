using DecaBlogMVC.Models.Api;
using DecaBlogMVC.Models.Components;

namespace DecaBlogMVC.Models;

public class ReadArticlesVM
{
    public IEnumerable<BlogPostListItemVM> ReadArticles { get; set; } = new List<BlogPostListItemVM>();
    public IEnumerable<TagViewModel> AllTags { get; set; } = new List<TagViewModel>();
    public IEnumerable<AuthorListItemViewModel> TopAuthors { get; set; } = new List<AuthorListItemViewModel>();
    public IEnumerable<BlogPostVM> RecentBlogPosts { get; set; } = new List<BlogPostVM>();
}