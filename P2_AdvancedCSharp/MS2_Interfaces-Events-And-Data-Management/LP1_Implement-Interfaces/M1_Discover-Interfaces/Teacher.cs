namespace M1_DiscoverInterfaces;

public class Teacher(string name = "", int age = 0) : IPerson
{
  public string Name { get; set; } = name;
  public int Age { get; set; } = age;

  public void DisplayInfo()
  {
    Console.WriteLine($"Teacher Name: {Name}, Age: {Age}");
  }
}
