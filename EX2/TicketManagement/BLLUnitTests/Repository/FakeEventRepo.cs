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
    class FakeEventRepo : IEventRepository
    {
        public List<Event> RepoList { get; set; }

        public FakeEventRepo()
        {
            RepoList = new List<Event>();
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

        public Event Get(int id)
        {
            Event r = new Event();

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

        public IQueryable<Event> All
        {
            get
            {
                return RepoList.AsQueryable();
            }
        }

        public IEnumerable<Event> VisibleEvents
        {
            get
            {
                throw new NotImplementedException();
            }
        }

        public int Save(Event elem)
        {
            RepoList.Add(elem as Event);
            return elem.Id;
        }

        public bool Update(Event elem)
        {
            for (int i = 0; i < RepoList.Count; i++)
            {
                if (RepoList[i].Id == elem.Id)
                {
                    RepoList[i] = elem as Event;
                    return true;
                }
            }
            return false;
        }

        public IEnumerable<Event> GetByLayoutId(int lid)
        {
            List<Event> ls = new List<Event>();
            foreach(var v in RepoList)
            {
                if(v.LayoutId == lid)
                {
                    ls.Add(v);
                }
            }

            return ls;
        }
    }
}
