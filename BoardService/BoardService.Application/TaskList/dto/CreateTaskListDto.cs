using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BoardService.Application.TaskList.dto
{
    public class CreateTaskListDto
    {
        public required string Name { get; set; }
        public int IdBoard { get; set; }
    }
}
