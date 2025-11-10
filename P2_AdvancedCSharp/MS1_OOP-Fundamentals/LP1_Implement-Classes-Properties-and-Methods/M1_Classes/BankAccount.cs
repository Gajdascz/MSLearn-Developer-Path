namespace M1_Classes;

public class BankAccount
{
  public int AccountNumber;
  public decimal Balance = 0;
  public static decimal InterestRate;
  public string AccountType = "Checking";
  public readonly string CustomerId;

  private static int s_nextAccountNumber = 1;

  static BankAccount()
  {
    s_nextAccountNumber = Random.Shared.Next(1000000, 2000000);
    InterestRate = 0;
  }

  public BankAccount(string customerId)
  {
    AccountNumber = s_nextAccountNumber++;
    CustomerId = customerId;
  }

  public BankAccount(string customerId, decimal balance, string accountType)
  {
    AccountNumber = s_nextAccountNumber++;
    CustomerId = customerId;
    Balance = balance;
    AccountType = accountType;
  }
}
