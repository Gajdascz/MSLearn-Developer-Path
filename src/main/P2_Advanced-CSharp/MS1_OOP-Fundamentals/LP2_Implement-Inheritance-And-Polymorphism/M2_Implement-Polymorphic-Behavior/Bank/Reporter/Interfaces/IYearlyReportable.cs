namespace M2_Polymorphism;

public interface IYearlyReportGenerator
{
  void GeneratePreviousYearReport();
  void GenerateCurrentYearToDateReport();
  void GenerateLast365DaysReport();
}
