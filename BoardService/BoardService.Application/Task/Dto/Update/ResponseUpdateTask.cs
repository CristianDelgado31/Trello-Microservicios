using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BoardService.Application.Task.Dto.Update
{
    public class ResponseUpdateTask
    {
        //public int Id { get; set; }
        //public string Name { get; set; }
        //public string Description { get; set; }
        //public int IdTaskList { get; set; }

        public bool Success { get; set; }
        public required string Message { get; set; }
    }
}
