namespace M2_Polymorphism;

public interface IMonthlyReportGenerator
{
  void GenerateMonthlyReport();
  void GenerateCurrentMonthToDateReport();
  void GeneratePrevious30DayReport();
}
