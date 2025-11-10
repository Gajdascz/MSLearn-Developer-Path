namespace M1_Inheritance;

public partial class BankCustomer : IBankCustomer
{
  public string ReturnFullName() => $"{FirstName} {LastName}";

  public string ReturnCustomerInfo() => $"Customer ID: {CustomerId}, Name: {ReturnFullName()}";

  public void UpdateName(string firstName, string lastName)
  {
    FirstName = firstName;
    LastName = lastName;
  }
}
