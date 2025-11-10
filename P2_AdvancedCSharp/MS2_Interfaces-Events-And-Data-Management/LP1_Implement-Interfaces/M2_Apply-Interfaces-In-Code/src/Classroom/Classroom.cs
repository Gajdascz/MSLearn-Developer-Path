using System.Collections;

namespace M2_ApplyInterfacesInCode;

public class Classroom : IEnumerable<Student>
{
  private List<Student> students = [];

  public void AddStudent(Student student) => students.Add(student);

  public void SortStudentsByAge() => students.Sort(); // Uses IComparable impl.

  public IEnumerator<Student> GetEnumerator() => students.GetEnumerator();

  IEnumerator IEnumerable.GetEnumerator() => GetEnumerator();
}
