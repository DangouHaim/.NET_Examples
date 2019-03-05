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
    class FakeAreaRepo : IAreaRepository
    {
        public List<Area> RepoList { get; set; }

        public FakeAreaRepo()
        {
            RepoList = new List<Area>();
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

        public Area Get(int id)
        {
            Area r = new Area();

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

        public IQueryable<Area> All
        {
            get
            {
                return RepoList.AsQueryable();
            }
        }

        public int Save(Area elem)
        {
            RepoList.Add(elem as Area);
            return elem.Id;
        }

        public bool Update(Area elem)
        {
            for (int i = 0; i < RepoList.Count; i++)
            {
                if (RepoList[i].Id == elem.Id)
                {
                    RepoList[i] = elem as Area;
                    return true;
                }
            }
            return false;
        }
    }
}
