using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BoardService.Application.Task.Dto.Update
{
    public class UpdateTaskDto
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public int IdTaskList { get; set; }
    }
}
