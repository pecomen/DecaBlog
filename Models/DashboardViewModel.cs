using DecaBlogMVC.Models.Api;
using DecaBlogMVC.Models.Components;

namespace DecaBlogMVC.Models;

public class DashboardViewModel
{
    public IEnumerable<BlogPostVM> TrendingPosts { get; set; }
    public IEnumerable<BlogPostVM> LatestBlogPosts { get; set; }
    public IEnumerable<BlogPostVM> PopularBlogPosts { get; set; }
    public IEnumerable<AuthorListItemViewModel> Authors { get; set; }
    public IEnumerable<TagViewModel> Tags { get; set; }
    public PaginationVM Pagination { get; set; }
}