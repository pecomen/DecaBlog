using DecaBlogMVC.Models.Api;
using DecaBlogMVC.Models.Components;

namespace DecaBlogMVC.Models;

public class StoryPageVM
{
    public IEnumerable<StoryArticlesVM> PendingArticles { get; set; } = Enumerable.Empty<StoryArticlesVM>();
    public IEnumerable<StoryArticlesVM> PublishedArticles { get; set; } = Enumerable.Empty<StoryArticlesVM>();
    public IEnumerable<StoryArticlesVM> RejectedArticles { get; set; } = Enumerable.Empty<StoryArticlesVM>();
    public IEnumerable<TagViewModel> AllTags { get; set; } = Enumerable.Empty<TagViewModel>();
    public IEnumerable<AuthorListItemViewModel> TopAuthors { get; set; } = Enumerable.Empty<AuthorListItemViewModel>();
    public IEnumerable<BlogPostVM> RecentBlogPosts { get; set; } = new List<BlogPostVM>();
}