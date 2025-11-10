namespace M2_Classes;

public class BankAccount
{
  public string AccountType { get; set; } = "Checking";
  public decimal Balance { get; private set; } = 0;

  public int AccountNumber { get; }
  public string CustomerId { get; }
  public static decimal InterestRate;

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

  public void Deposit(decimal amount)
  {
    if (amount > 0)
      Balance += amount;
  }

  public bool Withdraw(decimal amount)
  {
    if (amount > 0 && Balance >= amount)
    {
      Balance -= amount;
      return true;
    }
    return false;
  }

  public bool Transfer(BankAccount targetAccount, decimal amount)
  {
    if (Withdraw(amount))
    {
      targetAccount.Deposit(amount);
      return true;
    }
    return false;
  }

  public void ApplyInterest()
  {
    Balance += Balance * InterestRate;
  }

  public string ReturnAccountInfo() =>
    $"AccountNumber: {AccountNumber}, Type: {AccountType}, Balance: {Balance}, Interest Rate: {InterestRate}, Customer ID: {CustomerId}";
}
