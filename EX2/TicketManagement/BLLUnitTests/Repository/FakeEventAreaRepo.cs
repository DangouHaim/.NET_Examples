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
    class FakeEventAreaRepo : IEventAreaRepository
    {
        public List<EventArea> RepoList { get; set; }

        public FakeEventAreaRepo()
        {
            RepoList = new List<EventArea>();
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

        public EventArea Get(int id)
        {
            EventArea r = new EventArea();

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

        public IQueryable<EventArea> All
        {
            get
            {
                return RepoList.AsQueryable();
            }
        }

        public IEnumerable<EventArea> VisibleEventAreas
        {
            get
            {
                throw new NotImplementedException();
            }
        }

        public int Save(EventArea elem)
        {
            RepoList.Add(elem as EventArea);
            return elem.Id;
        }

        public bool Update(EventArea elem)
        {
            for (int i = 0; i < RepoList.Count; i++)
            {
                if (RepoList[i].Id == elem.Id)
                {
                    RepoList[i] = elem as EventArea;
                    return true;
                }
            }
            return false;
        }
    }
}
