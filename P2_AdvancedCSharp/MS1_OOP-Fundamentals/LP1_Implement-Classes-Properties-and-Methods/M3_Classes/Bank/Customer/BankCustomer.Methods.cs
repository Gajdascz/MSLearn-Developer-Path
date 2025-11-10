namespace M3_Classes;

public partial class BankCustomer
{
  public string ReturnFullName() => $"{FirstName} {LastName}";

  public string ReturnCustomerInfo() => $"Customer ID: {CustomerId}, Name: {ReturnFullName()}";

  public void UpdateName(string firstName, string lastName)
  {
    FirstName = firstName;
    LastName = lastName;
  }
}
