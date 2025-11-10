namespace M3_Classes;

public partial class BankCustomer
{
  public string FirstName { get; set; } = "Nolan";
  public string LastName { get; set; } = "Gajdascz";
  public readonly string CustomerId;

  private static int s_nextCustomerId;

  static BankCustomer()
  {
    s_nextCustomerId = Random.Shared.Next(Constants.IdRange.low, Constants.IdRange.high);
  }

  public BankCustomer(BankCustomer existingCustomer)
  {
    FirstName = existingCustomer.FirstName;
    LastName = existingCustomer.LastName;
    CustomerId = s_nextCustomerId++.ToString(Constants.CustomerAccountIdStringFormat);
  }

  public BankCustomer()
  {
    CustomerId = s_nextCustomerId++.ToString(Constants.CustomerAccountIdStringFormat);
  }

  public BankCustomer(string firstName, string lastName)
  {
    FirstName = firstName;
    LastName = lastName;
    CustomerId = s_nextCustomerId++.ToString(Constants.CustomerAccountIdStringFormat);
  }
}
