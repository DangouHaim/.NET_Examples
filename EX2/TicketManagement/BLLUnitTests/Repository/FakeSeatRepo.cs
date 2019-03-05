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
    class FakeSeatRepo : ISeatRepository
    {
        public List<Seat> RepoList { get; set; }

        public FakeSeatRepo()
        {
            RepoList = new List<Seat>();
        }

        public bool Delete(int id)
        {
            bool r = false;
            for (int i = 0; i < RepoList.Count; i++)
            {
                if (RepoList[i].Id == id)
                {
                    r = RepoList.Remove(RepoList[i]);
                    break;
                }
            }
            return r;
        }

        public Seat Get(int id)
        {
            Seat r = new Seat();

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

        public IQueryable<Seat> All
        {
            get
            {
                return RepoList.AsQueryable();
            }
        }

        public int Save(Seat elem)
        {
            RepoList.Add(elem as Seat);
            return elem.Id;
        }

        public bool Update(Seat elem)
        {
            for (int i = 0; i < RepoList.Count; i++)
            {
                if (RepoList[i].Id == elem.Id)
                {
                    RepoList[i] = elem as Seat;
                    return true;
                }
            }
            return false;
        }

        public IEnumerable<Seat> GetByParentId(int pid)
        {
            List<Seat> ls = new List<Seat>();
            foreach (var v in RepoList)
            {
                if (v.AreaId == pid)
                {
                    ls.Add(v);
                }
            }

            return ls;
        }
    }
}
