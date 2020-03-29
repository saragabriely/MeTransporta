using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WebAppTeste.Models;

namespace WebAppTeste.Interface
{
    public interface IStatusRepository
    {
        IEnumerable<Status> GetAll();
        Status  Get(int id);
        Status  Add(Status status);
        void    Remove(int id);
        bool    Update(Status status);
    }
}
