namespace BoardService.Domain.Entities
{
    public class TaskList
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public List<Task> Tasks { get; set; }
        public int IdBoard { get; set; }
    }
}
