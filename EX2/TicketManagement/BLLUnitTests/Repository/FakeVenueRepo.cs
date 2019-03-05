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
    class FakeVenueRepo : IVenueRepository
    {
        public List<Venue> RepoList { get; set; }

        public FakeVenueRepo()
        {
            RepoList = new List<Venue>();
        }

        public bool Delete(int id)
        {
            bool r = false;
            for(int i = 0; i < RepoList.Count; i++)
            {
                if (RepoList[i].Id == id)
                {
                    r = RepoList.Remove(RepoList[i]);
                    break;
                }
            }
            return r;
        }

        public Venue Get(int id)
        {
            Venue r = new Venue();

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

        public IQueryable<Venue> All
        {
            get
            {
                return RepoList.AsQueryable();
            }
        }

        public int Save(Venue elem)
        {
            RepoList.Add(elem as Venue);
            return elem.Id;
        }

        public bool Update(Venue elem)
        {
            for(int i = 0; i < RepoList.Count; i++)
            {
                if(RepoList[i].Id == elem.Id)
                {
                    RepoList[i] = elem as Venue;
                    return true;
                }
            }
            return false;
        }
    }
}
