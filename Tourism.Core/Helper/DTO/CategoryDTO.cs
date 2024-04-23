namespace Tourism.Core.Helper.DTO
{
    public class CategoryDTO
    {
        public int Id { get; set; }
        public string Name { get; set; }

        public virtual ICollection<PlaceDTO> Places { get; set; }
    }
}
