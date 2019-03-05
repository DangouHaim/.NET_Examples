namespace DataPresenter.Entity
{
    public class Layout
    {
        public int Id { get; set; }
        public int VenueId { get; set; }
        public string Description { get; set; }

        public DAL.Layout ToEntity()
        {
            return new DAL.Layout()
            {
                Id = Id,
                VenueId = VenueId,
                Description = Description
            };
        }

        public static Layout FromEntity(DAL.Layout entity)
        {
            return new Layout()
            {
                Id = entity.Id,
                Description = entity.Description,
                VenueId = entity.VenueId
            };
        }
    }
}
