namespace M1_GetStartedWithDatesTimesAndTimeZones;

public class MoneyMarketAccount : BankAccount
{
  public override decimal InterestRate { get; protected set; } = DefaultInterestRate;
  public static decimal DefaultInterestRate { get; private set; }
  public static decimal DefaultMinimumBalance { get; private set; }
  public static decimal DefaultMinimumOpeningBalance { get; private set; }

  public decimal MinimumBalance { get; set; }
  public decimal MinimumOpeningBalance { get; set; }

  static MoneyMarketAccount()
  {
    DefaultInterestRate = 0.04m;
    DefaultMinimumBalance = 1000m;
    DefaultMinimumOpeningBalance = 2000m;
  }

  public MoneyMarketAccount(string customerId, decimal initialBalance = 2000, decimal minimumBalance = 1000)
    : base(customerId, initialBalance, "MoneyMarket")
  {
    {
      if (initialBalance < DefaultMinimumOpeningBalance)
        throw new ArgumentException($"Balance must be at least {DefaultMinimumOpeningBalance}");
      MinimumBalance = minimumBalance;
    }
  }

  public override bool Withdraw(decimal amount)
  {
    if (amount > 0 && Balance - amount >= MinimumBalance)
    {
      Balance -= amount;
      return true;
    }
    return false;
  }

  public override string ReturnAccountInfo() =>
    base.ReturnAccountInfo() + $", Minimum Balance: {MinimumBalance}, Minimum Opening Balance: {MinimumOpeningBalance}";
}
