namespace DataPresenter.Entity
{
    public class User
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string EMail { get; set; }
        public string Password { get; set; }
        public decimal Balance { get; set; }

        public static User FromEntity(DAL.User data)
        {
            return new User()
            {
                Balance = data.Balance,
                EMail = data.Email,
                Id = data.Id,
                Name = data.Name,
                Password = data.Password
            };
        }

        public DAL.User ToEntity()
        {
            return new DAL.User()
            {
                Balance = Balance,
                Email = EMail,
                Id = Id,
                Name = Name,
                Password = Password
            };
        }
    }
}
