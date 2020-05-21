using System;
using System.Collections.Generic;

namespace Events.Repositories.Interfaces
{
    public interface IRepository<T>
    {
        List<T> GetAll();
        T Add(T item);
        T Update(T item);
        bool Delete(T item);
        T GetById(Guid id);

    }
}