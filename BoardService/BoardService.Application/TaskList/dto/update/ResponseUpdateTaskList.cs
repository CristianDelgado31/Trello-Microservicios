using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BoardService.Application.TaskList.dto.update
{
    public class ResponseUpdateTaskList
    {
        public bool Success { get; set; }
        public required string Message { get; set; }
    }
}
