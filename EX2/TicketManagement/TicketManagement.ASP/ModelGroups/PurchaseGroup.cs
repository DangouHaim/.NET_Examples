using System.Collections.Generic;
using DataPresenter.Entity;

namespace TicketManagement.ASP.ModelGroups
{
    public class PurchaseGroup
    {
        public IEnumerable<Purchase> Purchases { get; set; }

        public PurchaseGroup(IEnumerable<Purchase> Purchases)
        {
            this.Purchases = Purchases;
        }
    }
}
