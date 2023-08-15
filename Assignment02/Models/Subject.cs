using System.ComponentModel.DataAnnotations.Schema;

namespace Assignment02.Models;

[Table("Subject"), PrimaryKey(nameof(Id))]
public class Subject : Entity
{
    public string Name { get; set; }
    public string Code { get; set; }
    public int FullMark { get; set; }
    public IEnumerable<StudentSubject>? StudentsSubjects { get; set; }
}
