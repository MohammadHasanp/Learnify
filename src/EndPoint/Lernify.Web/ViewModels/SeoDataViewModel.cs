using Common.Domain.ValueObjects;
using System.ComponentModel.DataAnnotations;

namespace Learnify.Web.ViewModels;

public class SeoDataViewModel
{
    public string? MetaTitle { get; set; }

    [DataType(DataType.MultilineText)]
    public string? MetaDescription { get; set; }
    public string? MetaKeyWords { get; set; }

    [DataType(DataType.Url)]
    public string? Canonical { get; set; }

    public bool IndexPage { get; set; }
    public string? Schema { get; set; }


    public SeoData Map()
    {
        return new SeoData(MetaTitle, MetaDescription, MetaKeyWords, IndexPage, Canonical, Schema);
    }
    public static SeoDataViewModel ConvertToViewModel(SeoData? seoData)
    {

        return new SeoDataViewModel()
        {
            Canonical = seoData?.Canonical,
            MetaDescription = seoData?.MetaDescription,
            MetaKeyWords = seoData?.MetaKeyWords,
            MetaTitle = seoData?.MetaTitle,
            Schema = seoData?.Schema,
            IndexPage = seoData!.IndexPage,
        };
    }
}