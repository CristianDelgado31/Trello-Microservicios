﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BoardService.Application.Task.Dto.Create
{
    public class CreateTaskDto
    {
        public required string Name { get; set; }
        public required string Description { get; set; }
        public int IdTaskListId { get; set; }
    }
}
