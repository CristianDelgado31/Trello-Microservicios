using BoardService.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BoardService.Application.Board.dto
{
    public class CreateBoardDto
    {
        public required string Name { get; set; }
    }
}

//public int Id { get; set; }
//public string Name { get; set; }
//public List<TaskList> TaskLists { get; set; }
//public int IdAuth { get; set; }