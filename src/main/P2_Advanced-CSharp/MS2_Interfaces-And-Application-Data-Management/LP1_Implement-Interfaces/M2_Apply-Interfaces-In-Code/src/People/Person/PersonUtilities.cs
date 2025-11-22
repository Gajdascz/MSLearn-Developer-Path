namespace M2_ApplyInterfacesInCode;

public class PersonUtilities
{
  public static void PrintPersonDetails(IPerson person)
  {
    person.DisplayInfo();
    person.Greet();
  }
}
