namespace DecaBlogMVC.Models.Components;

public class CommentListItemVM
{
    public string Id { get; set; }
    public string Name { get; set; }
    public string Image { get; set; }
    public string Text { get; set; }
    public DateTime CreatedOn { get; set; }
}