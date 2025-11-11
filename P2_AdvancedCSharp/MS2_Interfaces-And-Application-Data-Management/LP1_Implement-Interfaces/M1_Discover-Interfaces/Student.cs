namespace M1_DiscoverInterfaces;

public class Student(string name = "", int age = 0) : IPerson
{
  public string Name { get; set; } = name;
  public int Age { get; set; } = age;

  public void DisplayInfo()
  {
    Console.WriteLine($"Student Name: {Name}, Age: {Age}");
  }
}
