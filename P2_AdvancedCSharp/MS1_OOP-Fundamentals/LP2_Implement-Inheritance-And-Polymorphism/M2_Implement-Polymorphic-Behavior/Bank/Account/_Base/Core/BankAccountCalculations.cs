namespace M2_Polymorphism;

public static class BankAccountCalculations
{
  public static decimal CompoundInterest(decimal principal, decimal annualRate, decimal years) =>
    principal * (decimal)Math.Pow(1 + (double)annualRate, (double)years) - principal;

  public static bool IsValidAccountNumber(int accountNumber) => accountNumber.ToString().Length == 8;

  public static decimal TransactionFee(decimal amount, decimal transactionRate, decimal maxTransactionFee) =>
    Math.Min(amount * transactionRate, maxTransactionFee);

  public static decimal OverdraftFee(decimal amountOverdrawn, decimal overdraftFee, decimal maxOverdraftFee) =>
    Math.Min(amountOverdrawn * overdraftFee, maxOverdraftFee);

  public static decimal CurrencyConversion(decimal amount, decimal exchangeRate) => amount * exchangeRate;
}
