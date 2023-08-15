using System.ComponentModel.DataAnnotations.Schema;

namespace Assignment02.Models;

[Table("Students"), PrimaryKey(nameof(Id))]
public class Student : Entity
{
    public string SSN { get; set; }
    public string FirstName { get; set; }
    public string LastName { get; set; }
    public string PhoneNumber { get; set; }
    public string StreetName { get; set; }
    public int StreetNumber { get; set; }
    public string City { get; set; }
    public string ZipCode { get; set; }
    public int DepartmentId { get; set; }

    [ForeignKey(nameof(DepartmentId))]
    public Department? Department { get; set; }

    public IEnumerable<StudentSubject>? StudentSubjects { get; set; }
}
