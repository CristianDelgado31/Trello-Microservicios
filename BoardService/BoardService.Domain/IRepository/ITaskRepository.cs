using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BoardService.Domain.IRepository
{
    public interface ITaskRepository
    {
        public Task<Entities.Task> CreateTasK(Entities.Task task);
        public Task<List<Entities.Task>> GetTasks(int id);
    }
}
