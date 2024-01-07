using System.ComponentModel.DataAnnotations;

namespace DecaBlogMVC.Models.Components;

public class AddCommentVM
{
    [Required]
    public string Text { get; set; }
}