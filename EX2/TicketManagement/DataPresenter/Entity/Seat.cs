namespace DataPresenter.Entity
{
    public class Seat
    {
        public int Id { get; set; }
        public int AreaId { get; set; }
        public int Row { get; set; }
        public int Number { get; set; }

        public static Seat GetFromEntity(DAL.Seat data)
        {
            return new Seat()
            {
                Id = data.Id,
                Number = data.Number,
                Row = data.Row,
                AreaId = data.AreaId
            };
        }

        public DAL.Seat ToEntity()
        {
            return new DAL.Seat()
            {
                Id = Id,
                Number = Number,
                Row = Row,
                AreaId = AreaId
            };
        }
    }
}
