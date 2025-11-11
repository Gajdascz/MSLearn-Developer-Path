namespace M1_GetStartedWithDatesTimesAndTimeZones;

public static class BankCustomerExtensions
{
  public static bool IsValidCustomerId(this BankCustomer customer) => customer.CustomerId.Length == 10;

  public static string ReturnCustomerGreeting(this BankCustomer customer) => $"Hello, {customer.ReturnFullName()}";
}
