using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BoardService.Domain.Entities
{
    public class Task
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public int IdTaskList { get; set; }
        //public DateTime CreatedAt { get; set; }
        //public DateTime? CompletedAt { get; set; }
    }
}
