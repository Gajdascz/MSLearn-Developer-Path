namespace M2_ApplyInterfacesInCode;

public class Student(string name = "", int age = 0) : IPerson, IComparable<Student>
{
  public string Name { get; set; } = name;
  public int Age { get; set; } = age;
  public string Role => "Student";

  public void DisplayInfo()
  {
    Console.WriteLine($"Student Name: {Name}, Age: {Age}");
  }

  public int CompareTo(Student? other) => other is null ? 1 : Age.CompareTo(other.Age);
}
