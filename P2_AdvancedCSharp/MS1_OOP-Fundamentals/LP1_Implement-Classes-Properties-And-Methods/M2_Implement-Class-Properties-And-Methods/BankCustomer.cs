namespace M2_Classes;

public class BankCustomer
{
  public string FirstName { get; set; } = "Nolan";
  public string LastName { get; set; } = "Gajdascz";
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

  public string ReturnFullName() => $"{FirstName} {LastName}";

  public string ReturnCustomerInfo() => $"Customer ID: {CustomerId}, Name: {ReturnFullName()}";

  public void UpdateName(string firstName, string lastName)
  {
    FirstName = firstName;
    LastName = lastName;
  }
}
