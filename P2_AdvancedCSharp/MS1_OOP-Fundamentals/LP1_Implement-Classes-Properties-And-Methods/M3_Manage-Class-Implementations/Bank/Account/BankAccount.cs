namespace M3_Classes;

public partial class BankAccount
{
  public string AccountType { get; set; } = "Checking";
  public decimal Balance { get; private set; } = 0;

  public int AccountNumber { get; }
  public string CustomerId { get; }
  public static decimal InterestRate { get; private set; }
  public static decimal TransactionRate { get; private set; }
  public static decimal MaxTransactionFee { get; private set; }
  public static decimal OverdraftRate { get; private set; }
  public static decimal MaxOverdraftFee { get; private set; }

  private static int s_nextAccountNumber = 1;

  static BankAccount()
  {
    s_nextAccountNumber = Random.Shared.Next(Constants.IdRange.low, Constants.IdRange.high);
    InterestRate = 0.00m;
    TransactionRate = 0.01m;
    MaxTransactionFee = 10;
    OverdraftRate = 0.05m;
    MaxOverdraftFee = 10;
  }

  public BankAccount(BankAccount existingAccount)
  {
    AccountNumber = s_nextAccountNumber++;
    CustomerId = existingAccount.CustomerId;
    Balance = existingAccount.Balance;
    AccountType = existingAccount.AccountType;
  }

  public BankAccount(string customerId)
  {
    AccountNumber = s_nextAccountNumber++;
    CustomerId = customerId;
  }

  public BankAccount(string customerId, decimal balance = 200, string accountType = "Checking")
  {
    AccountNumber = s_nextAccountNumber++;
    CustomerId = customerId;
    Balance = balance;
    AccountType = accountType;
  }
}
