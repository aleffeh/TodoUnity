using SQLite4Unity3d;

namespace Models
{
    public class TodoItem
    {
        [PrimaryKey,Unique,AutoIncrement]
        public int Id { get; set; }

        public string Text { get; set; }
    }
}