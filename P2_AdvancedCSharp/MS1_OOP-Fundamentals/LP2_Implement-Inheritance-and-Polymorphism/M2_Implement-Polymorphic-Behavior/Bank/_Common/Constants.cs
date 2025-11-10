namespace M2_Polymorphism;

public static class Constants
{
  public static readonly (int low, int high) IdRange = (1000000, 2000000);
  public static readonly int CustomerIdLength = 10;
  public static readonly string CustomerIdStringFormat = $"D{CustomerIdLength}";
  public static readonly int CustomerAccountIdLength = 8;
  public static readonly string CustomerAccountIdStringFormat = $"D{CustomerAccountIdLength}";
}
