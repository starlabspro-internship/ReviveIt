public class GetAllJobsQuery
{
    public string? Keywords { get; set; }
    public int? SelectedCityId { get; set; }
    public int? SelectedCategoryId { get; set; }
    public decimal? Price { get; set; }
    public string? NumberOfApplicants { get; set; }
    public string? SortBy { get; set; }
}