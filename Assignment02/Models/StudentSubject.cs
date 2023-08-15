using System.ComponentModel.DataAnnotations.Schema;

namespace Assignment02.Models;

[Table("StudentsSubjects"), PrimaryKey(nameof(StudentId), nameof(SubjectId))]
public class StudentSubject
{
    public int StudentId { get; set; }
    public int SubjectId { get; set; }

    [ForeignKey(nameof(StudentId))]
    public Student? Student { get; set; }

    [ForeignKey(nameof(SubjectId))]
    public Subject? Subject { get; set; }

}
