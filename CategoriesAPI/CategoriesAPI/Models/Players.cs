namespace CategoriesAPI.Models
{
    /// <summary>
    /// 
    /// </summary>
    public partial class Players
    {
        public int PlayerId { get; set; }
        public string EmailAddress { get; set; }
        public string Name { get; set; }
        public int? Score { get; set; }
        public int? Ranking { get; set; }
    }
}
