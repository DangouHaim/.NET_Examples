using DAL;
using DAL.DataEntity;
using DAL.IRepository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BLLUnitTests
{
    class FakeEventSeatRepo : IEventSeatRepository
    {
        public List<EventSeat> RepoList { get; set; }

        public FakeEventSeatRepo()
        {
            RepoList = new List<EventSeat>();
        }

        public bool Delete(int id)
        {
            bool r = false;
            for (int i = 0; i < RepoList.Count; i++)
            {
                if(RepoList[i].Id == id)
                {
                    r = RepoList.Remove(RepoList[i]);
                    break;
                }
            }
            return r;
        }

        public EventSeat Get(int id)
        {
            EventSeat r = new EventSeat();

            foreach (var v in RepoList)
            {
                if (v.Id == id)
                {
                    r = v;
                    break;
                }
            }

            return r;
        }

        public IQueryable<EventSeat> All
        {
            get
            {
                return RepoList.AsQueryable();
            }
        }

        public IEnumerable<EventSeat> VisibleEventSeats
        {
            get
            {
                throw new NotImplementedException();
            }
        }

        public int Save(EventSeat elem)
        {
            RepoList.Add(elem as EventSeat);
            return elem.Id;
        }

        public bool Update(EventSeat elem)
        {
            for (int i = 0; i < RepoList.Count; i++)
            {
                if (RepoList[i].Id == elem.Id)
                {
                    RepoList[i] = elem as EventSeat;
                    return true;
                }
            }
            return false;
        }
    }
}
