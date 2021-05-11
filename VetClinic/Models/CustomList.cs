using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace VetClinic.Models
{
    public abstract class CustomList<T> : List<T>
    {
        public abstract List<T> GetAll();

        public abstract T GetById(int? id);

        public abstract new void Add(T item);

        public abstract void Edit(T item);

        public abstract void Delete(int? id);
    }
}
