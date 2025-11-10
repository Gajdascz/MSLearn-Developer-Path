namespace M1_Classes;

public class BankCustomer
{
  public string FirstName = "Nolan";
  public string LastName = "Gajdascz";
  public readonly string CustomerId;

  private static int s_nextCustomerId;

  static BankCustomer()
  {
    s_nextCustomerId = Random.Shared.Next(1000000, 2000000);
  }

  public BankCustomer()
  {
    CustomerId = s_nextCustomerId++.ToString("D10");
  }

  public BankCustomer(string firstName, string lastName)
  {
    FirstName = firstName;
    LastName = lastName;
    CustomerId = s_nextCustomerId++.ToString("D10");
  }
}
