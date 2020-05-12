namespace CategoriesAPI.Data
{
    public interface ICategoryEntity
    {
        int Id { get; set; }
        string CategoryName_En { get; set; }

        string CategoryName_Fr { get; set; }
    }
}
