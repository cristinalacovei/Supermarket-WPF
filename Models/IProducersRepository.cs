using System.Collections.Generic;
using WPF_LoginForm.Models;

namespace WPF_LoginForm.Repositories
{
    public interface IProducerRepository
    {
        void Add(Producer producer);
        void Edit(Producer producer);
        IEnumerable<Producer> GetAll();
        Producer GetById(int id);
        void Remove(int id);
    }
}
