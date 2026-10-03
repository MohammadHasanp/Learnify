namespace Common.Domain.ValueObjects;


public class SeoData : ValueObject
{
    private SeoData() { }
    public string? MetaTitle { get; private set; }
    public string? MetaDescription { get; private set; }
    public string? MetaKeyWords { get; private set; }
    public bool IndexPage { get; set; }
    public string? Canonical { get; private set; }
    public string? Schema { get; private set; }

    public SeoData(string? metaTitle, string? metaDescription, string? mateKeyWords, bool indexPage, string? canonical, string? schema)
    {
        MetaTitle = metaTitle;
        MetaDescription = metaDescription;
        MetaKeyWords = mateKeyWords;
        IndexPage = indexPage;
        Canonical = canonical;
        Schema = schema;
    }
    public static SeoData CreateEmpty()
    {
        return new SeoData();
    }

}