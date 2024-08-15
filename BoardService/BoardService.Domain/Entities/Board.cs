namespace BoardService.Domain.Entities
{
    public class Board
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public List<TaskList> TaskLists { get; set; }
        public int IdAuth { get; set; }
    }
}
