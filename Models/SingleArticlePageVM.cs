using DecaBlogMVC.Models.Api;
using DecaBlogMVC.Models.Components;

namespace DecaBlogMVC.Models;

public class SingleArticlePageVM
{
    public SingleArticleVM Article { get; set; }
    public IEnumerable<AuthorListItemViewModel> Authors { get; set; }
    public IEnumerable<TagViewModel> Tags { get; set; }
    public IEnumerable<BlogPostVM> MoreArticles { get; set; }
    public AddCommentVM AddComment { get; set; }
}