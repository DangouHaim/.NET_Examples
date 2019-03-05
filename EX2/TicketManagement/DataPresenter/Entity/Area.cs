namespace DataPresenter.Entity
{
    public class Area
    {
        public int Id { get; set; }
        public int LayoutId { get; set; }
        public string Description { get; set; }
        public int CoordX { get; set; }
        public int CoordY { get; set; }

        public DAL.Area ToEntity()
        {
            return new DAL.Area()
            {
                Id = Id,
                CoordX = CoordX,
                LayoutId = LayoutId,
                Description = Description,
                CoordY = CoordY,
            };
        }

        public static Area FromEntity(DAL.Area entity)
        {
            return new Area()
            {
                Id = entity.Id,
                CoordX = entity.CoordX,
                LayoutId = entity.LayoutId,
                Description = entity.Description,
                CoordY = entity.CoordY
            };
        }
    }
}
