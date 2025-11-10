namespace M2_Classes;

public static class BankCustomerExtensions
{
  public static bool IsValidCustomerId(this BankCustomer customer) => customer.CustomerId.Length == 10;

  public static string ReturnCustomerGreeting(this BankCustomer customer) => $"Hello, {customer.ReturnFullName()}";
}

public static class BankAccountExtensions
{
  public static bool IsOverdrawn(this BankAccount account) => account.Balance < 0;

  public static bool CanWithdraw(this BankAccount account, decimal amount) => account.Balance >= amount;
}
