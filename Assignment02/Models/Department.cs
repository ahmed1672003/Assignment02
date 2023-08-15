using System.ComponentModel.DataAnnotations.Schema;

using Microsoft.EntityFrameworkCore;

namespace Assignment02.Models;

[Table("Departments"), PrimaryKey(nameof(Id))]
public class Department : Entity
{
    public string Name { get; set; }
    public IEnumerable<Student> Students { get; set; } = new HashSet<Student>();
}
