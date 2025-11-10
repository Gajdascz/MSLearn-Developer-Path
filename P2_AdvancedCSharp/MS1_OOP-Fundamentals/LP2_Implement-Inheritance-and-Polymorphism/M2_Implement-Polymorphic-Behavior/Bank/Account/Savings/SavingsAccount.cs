namespace M2_Polymorphism;

public class SavingsAccount : BankAccount
{
  public override decimal InterestRate { get; protected set; } = DefaultInterestRate;
  public int WithdrawalsThisMonth
  {
    get;
    private set =>
      field =
        value < WithdrawalLimit
          ? value
          : throw new ArgumentException($"Cannot exceed {WithdrawalLimit} withdrawals per month.");
  }
  public static decimal DefaultWithdrawalLimit { get; private set; }
  public static decimal DefaultMinimumOpeningBalance { get; private set; }
  public static decimal DefaultInterestRate { get; private set; }

  public int WithdrawalLimit { get; set; }
  public decimal MinimumOpeningBalance { get; set; }

  static SavingsAccount()
  {
    DefaultWithdrawalLimit = 6;
    DefaultMinimumOpeningBalance = 500;
    DefaultInterestRate = 0.02m;
  }

  public SavingsAccount(string customerId, decimal initialBalance = 500, int withdrawalLimit = 6)
    : base(customerId, initialBalance, "Savings")
  {
    if (initialBalance < DefaultMinimumOpeningBalance)
      throw new ArgumentException($"Balance must be at least {DefaultMinimumOpeningBalance}");
    if (withdrawalLimit < 0)
      throw new ArgumentException($"Monthly withdrawal limit cannot be less than zero.");
    WithdrawalLimit = withdrawalLimit;
    WithdrawalsThisMonth = 0;
  }

  public void ResetWithdrawalLimit() => WithdrawalsThisMonth = 0;

  public override bool Withdraw(decimal amount)
  {
    try
    {
      if (amount > 0 && Balance >= amount && WithdrawalsThisMonth < WithdrawalLimit)
      {
        Balance -= amount;
        WithdrawalsThisMonth++;
        return true;
      }
      return false;
    }
    catch (ArgumentException e)
    {
      Console.WriteLine($"[{AccountNumber}] Failed to withdraw from savings account: {e.Message}");
      return false;
    }
  }

  public override string ReturnAccountInfo() =>
    base.ReturnAccountInfo() + $", Withdrawal Limit: {WithdrawalLimit}, Withdrawals This Month: {WithdrawalsThisMonth}";
}
