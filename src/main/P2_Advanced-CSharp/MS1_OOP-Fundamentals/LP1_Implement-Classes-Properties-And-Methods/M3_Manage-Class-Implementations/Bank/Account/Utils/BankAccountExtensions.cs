namespace M3_Classes;

public static class BankAccountExtensions
{
  public static bool IsOverdrawn(this BankAccount account) => account.Balance < 0;

  public static bool CanWithdraw(this BankAccount account, decimal amount) => account.Balance >= amount;
}
