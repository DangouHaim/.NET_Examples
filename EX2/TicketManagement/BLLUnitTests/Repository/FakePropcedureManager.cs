using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DAL;
using DAL.DataEntity;
using DAL.IRepository;

namespace BLLUnitTests.Repository
{
    class FakePropcedureManager : IProcedureManager
    {
        FakeEventRepo Repo { get; set; }
        private static int _id = 0;

        public FakePropcedureManager(FakeEventRepo repo)
        {
            Repo = repo;
        }

        public int AddEvent(string name, string description, int layoutId, DateTime eventDate)
        {
            _id++;
            Repo.RepoList.Add(new Event()
            {
                Id = _id,
                Name = name,
                Description = description,
                LayoutId = layoutId,
                EventDate = eventDate
            });
            return _id;
        }

        public int AttachFileToEvent(int eventId, string url)
        {
            throw new NotImplementedException();
        }

        public int Buy(int userId, int eventSeatId)
        {
            throw new NotImplementedException();
        }

        public bool DeleteEvent(int eventId)
        {
            bool r = false;
            for (int i = 0; i < Repo.RepoList.Count; i++)
            {
                if (Repo.RepoList[i].Id == eventId)
                {
                    r = Repo.RepoList.Remove(Repo.RepoList[i]);
                    break;
                }
            }
            return r;
        }

        public bool DeleteFileFromEvent(int fileId)
        {
            throw new NotImplementedException();
        }

        public bool FromCart(int userId, int eventSeatId)
        {
            throw new NotImplementedException();
        }

        public List<string> GetAttachedFilesForEvent(int eventId)
        {
            throw new NotImplementedException();
        }

        public List<Tuple<int, string>> GetAttachedFilesForEventToPair(int eventId)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<GetUserCart_Result3> GetCart(int uid)
        {
            throw new NotImplementedException();
        }

        public int TicketCount(int eventId)
        {
            throw new NotImplementedException();
        }

        public int TicketCountTotal(int eventId)
        {
            throw new NotImplementedException();
        }

        public int ToCart(int userId, int eventSeatId)
        {
            throw new NotImplementedException();
        }

        public bool UpdateEvent(int eventId, string name, string description, int layoutId, DateTime eventDate)
        {
            for (int i = 0; i < Repo.RepoList.Count; i++)
            {
                if (Repo.RepoList[i].Id == eventId)
                {
                    Repo.RepoList[i] = new Event()
                    {
                        Name = name,
                        Description = description,
                        LayoutId = layoutId,
                        EventDate = eventDate
                    };
                    return true;
                }
            }
            return false;
        }
    }
}
