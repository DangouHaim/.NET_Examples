using System.Collections.Generic;

namespace DAL
{
    public partial class User
    {
        public User()
        {
            Cart = new HashSet<Cart>();
            Purchase = new HashSet<Purchase>();
        }

        public int Id { get; set; }
        public string Name { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
        public decimal Balance { get; set; }

        public ICollection<Cart> Cart { get; set; }
        public ICollection<Purchase> Purchase { get; set; }
    }
}
