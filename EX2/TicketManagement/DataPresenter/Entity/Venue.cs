namespace DataPresenter.Entity
{
    public partial class Venue
    {
        public int Id { get; set; }
        public string Description { get; set; }
        public string Address { get; set; }
        public string Phone { get; set; }

        public DAL.Venue ToEntity()
        {
            return new DAL.Venue()
            {
                Id = Id,
                Description = Description,
                Address = Address,
                Phone = Phone,
            };
        }

        public static Venue FromEntity(DAL.Venue entity)
        {
            return new Venue()
            {
                Id = entity.Id,
                Description = entity.Description,
                Address = entity.Address,
                Phone = entity.Phone
            };
        }
    }
}
