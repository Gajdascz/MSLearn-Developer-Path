namespace M2_ApplyInterfacesInCode;

public class Teacher(string name = "", int age = 0) : IPerson
{
  public string Name { get; set; } = name;
  public int Age { get; set; } = age;

  public string Role => "Teacher";

  public void DisplayInfo()
  {
    Console.WriteLine($"Teacher Name: {Name}, Age: {Age}");
  }

  public void Greet()
  {
    Console.WriteLine($"Hello, I am {Name}, an I am a teacher.");
  }
}
