namespace M2_ApplyInterfacesInCode;

public interface IPerson
{
  string Name { get; set; }
  int Age { get; set; }
  void DisplayInfo();

  string Role { get; }

  void Greet()
  {
    Console.WriteLine($"Hello, my name is {Name} and I am a {Role}.");
  }
}
