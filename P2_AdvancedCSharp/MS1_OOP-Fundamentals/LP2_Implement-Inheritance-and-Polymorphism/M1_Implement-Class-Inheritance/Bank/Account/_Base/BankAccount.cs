namespace M1_Inheritance;

public abstract partial class BankAccount : IBankAccount
{
  public string AccountType { get; set; }
  public decimal Balance { get; protected set; } = 0;

  public int AccountNumber { get; }
  public string CustomerId { get; }
  public virtual decimal InterestRate { get; protected set; }
  public static decimal TransactionRate { get; protected set; }
  public static decimal MaxTransactionFee { get; protected set; }
  public static decimal OverdraftRate { get; protected set; }
  public static decimal MaxOverdraftFee { get; protected set; }

  private static int s_nextAccountNumber = 1;

  static BankAccount()
  {
    s_nextAccountNumber = Random.Shared.Next(Constants.IdRange.low, Constants.IdRange.high);
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

  public BankAccount(string customerId, string accountType)
  {
    AccountNumber = s_nextAccountNumber++;
    CustomerId = customerId;
    AccountType = accountType;
  }

  public BankAccount(string customerId, decimal balance = 200, string accountType = "Checking")
  {
    AccountNumber = s_nextAccountNumber++;
    CustomerId = customerId;
    Balance = balance;
    AccountType = accountType;
  }
}
