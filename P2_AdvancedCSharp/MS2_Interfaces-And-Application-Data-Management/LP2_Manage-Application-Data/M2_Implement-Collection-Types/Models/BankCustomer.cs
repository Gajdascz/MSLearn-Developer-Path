namespace Data_M2;

public partial class BankCustomer : IBankCustomer
{
  public string FirstName { get; set; } = "Nolan";
  public string LastName { get; set; } = "Gajdascz";
  public string CustomerId { get; private set; }
  public List<IBankAccount> Accounts = [];

  private static int s_nextCustomerId;

  static BankCustomer()
  {
    s_nextCustomerId = Random.Shared.Next(1000000, 20000000);
  }

  public BankCustomer(BankCustomer existingCustomer)
  {
    FirstName = existingCustomer.FirstName;
    LastName = existingCustomer.LastName;
    CustomerId = s_nextCustomerId++.ToString("D10");
  }

  public BankCustomer()
  {
    CustomerId = s_nextCustomerId++.ToString("D8");
  }

  public BankCustomer(string firstName, string lastName)
  {
    FirstName = firstName;
    LastName = lastName;
    CustomerId = s_nextCustomerId++.ToString("D8");
  }
}
