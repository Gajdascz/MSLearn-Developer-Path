using System;

namespace Files_M2;

public class BankCustomerDTO
{
  public string CustomerId { get; set; } = "";
  public string FirstName { get; set; } = "";
  public string LastName { get; set; } = "";
  public List<int> AccountNumbers { get; set; } = [];

  public static BankCustomerDTO MapToDTO(BankCustomer customer) =>
    new()
    {
      CustomerId = customer.CustomerId,
      FirstName = customer.FirstName,
      LastName = customer.LastName,
      AccountNumbers = [.. customer.Accounts.Select(a => a.AccountNumber)],
    };
}
