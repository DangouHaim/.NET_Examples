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
    class FakeLayoutRepo : ILayoutRepository
    {
        public List<Layout> RepoList { get; set; }

        public FakeLayoutRepo()
        {
            RepoList = new List<Layout>();
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

        public Layout Get(int id)
        {
            Layout r = new Layout();

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

        public IQueryable<Layout> All
        {
            get
            {
                return RepoList.AsQueryable();
            }
        }

        public int Save(Layout elem)
        {
            RepoList.Add(elem as Layout);
            return elem.Id;
        }

        public bool Update(Layout elem)
        {
            for (int i = 0; i < RepoList.Count; i++)
            {
                if (RepoList[i].Id == elem.Id)
                {
                    RepoList[i] = elem as Layout;
                    return true;
                }
            }
            return false;
        }

        public IEnumerable<Layout> GetByParentId(int pid)
        {
            List<Layout> ls = new List<Layout>();
            foreach(var v in RepoList)
            {
                if(v.VenueId == pid)
                {
                    ls.Add(v);
                }
            }

            return ls;
        }
    }
}
