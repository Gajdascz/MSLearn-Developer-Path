namespace M1_Inheritance;

public interface IBankCustomer
{
  string FirstName { get; set; }
  string LastName { get; set; }
  string CustomerId { get; }

  string ReturnFullName();
  void UpdateName(string firstName, string lastName);
  string ReturnCustomerInfo();
}
