using SQLite;

namespace Mvvm.Models;

public class Poetry
{
    [PrimaryKey, AutoIncrement]//特性标记，主键自增
    public int Id { get; set; }

    public string Name { get; set; } = string.Empty;
}