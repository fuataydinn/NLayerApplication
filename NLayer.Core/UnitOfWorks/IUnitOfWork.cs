using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NLayer.Core.UnitOfWorks
{
    public interface IUnitOfWork
    {
        //Repolarda toplu sekilde saveChange yapmamıza saglayan patterndır.

        Task CommitAsync();

        void Commit();
    }
}
