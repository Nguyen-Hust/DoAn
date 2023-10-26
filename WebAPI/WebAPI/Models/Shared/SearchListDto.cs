namespace WebAPI.Models.Shared
{
    public class SearchListDto
    {
        public string Filter { get; set; }
        public int? MaxResultCount { get; set; }
        public int? SkipCount { get; set; }
    }
}
