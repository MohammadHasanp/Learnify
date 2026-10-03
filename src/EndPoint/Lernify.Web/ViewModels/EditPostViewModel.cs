using BlogModule.Services.DTOs.Query;
using System.ComponentModel.DataAnnotations;

namespace Learnify.Web.ViewModels;

public class EditPostViewModel
{
    public List<BlogCategoryDto?>? Categories { get; set; } = (List<BlogCategoryDto?>)[];
    public Guid Id { get; set; }
    public Guid CategoryId { get; set; }

    [Display(Name = "عنوان")]
    [Required(ErrorMessage = "{0} را وارد کنید")]
    public string Title { get; set; } = null!;
    public Guid UserId { get; set; }

    [Display(Name = "نام نویسنده")]
    [Required(ErrorMessage = "{0} را وارد کنید")]
    public string OwnerName { get; set; } = null!;

    [Display(Name = "توئضیحات")]
    [Required(ErrorMessage = "{0} را وارد کنید")]
    public string Description { get; set; } = null!;

    [Display(Name = "slug")]
    [Required(ErrorMessage = "{0} را وارد کنید")]
    public string Slug { get; set; } = null!;
    public IFormFile? ImageFile { get; set; }
}