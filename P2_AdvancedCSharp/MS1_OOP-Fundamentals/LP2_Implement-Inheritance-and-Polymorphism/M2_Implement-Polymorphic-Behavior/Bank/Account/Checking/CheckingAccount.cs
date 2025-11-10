namespace M2_Polymorphism;

public class CheckingAccount(string customerId, decimal initialBalance, decimal overdraftLimit = 500m)
  : BankAccount(customerId, initialBalance, "Checking")
{
  public override decimal InterestRate { get; protected set; } = DefaultInterestRate;
  public static decimal DefaultOverdraftLimit { get; private set; }
  public static decimal DefaultInterestRate { get; private set; }

  public decimal OverdraftLimit { get; set; } = overdraftLimit;

  static CheckingAccount()
  {
    DefaultOverdraftLimit = 500m;
    DefaultInterestRate = 0.0m;
  }

  public override bool Withdraw(decimal amount)
  {
    if (amount > 0 && Balance + OverdraftLimit >= amount)
    {
      Balance -= amount;

      // Check if the account is overdrawn
      if (Balance < 0)
      {
        decimal overdraftFee = BankAccountCalculations.OverdraftFee(Math.Abs(Balance), OverdraftRate, MaxOverdraftFee);
        Balance -= overdraftFee;
        Console.WriteLine($"Overdraft fee of ${overdraftFee} applied.");
      }

      return true;
    }
    return false;
  }

  public override string ReturnAccountInfo() => base.ReturnAccountInfo() + $", Overdraft Limit: {OverdraftLimit}";
}
