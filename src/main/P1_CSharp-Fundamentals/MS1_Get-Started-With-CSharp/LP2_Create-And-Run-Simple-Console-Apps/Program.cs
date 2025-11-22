Console.Clear();

const int STUDENT_COL_PADDING = -10;
const int EXAM_SCORE_COL_PADDING = -15;
const int OVERALL_SCORE_AVERAGE_SPACE = -10;
const int OVERALL_SCORE_COL_PADDING = -20;
const int EXTRA_CREDIT_COL_PADDING = -20;

/*
  Calculates and displays details on a class's students performance given a predefined set of student names and assignment grades. Displays the students name, final numeric grade (decimal), and final letter grade.
*/
(string name, int[] grades, int[]? extraCredit)[] students =
[
  ("Sophia", [90, 86, 87, 98, 100], [94, 90]),
  ("Andrew", [92, 89, 81, 96, 90], [89]),
  ("Emma", [90, 85, 87, 98, 68], [89, 89, 89]),
  ("Logan", [90, 95, 87, 88, 96], [96]),
];
Console.WriteLine(
  $"{"Student", STUDENT_COL_PADDING}{"Exam Score", EXAM_SCORE_COL_PADDING}{"Overall Grade", OVERALL_SCORE_COL_PADDING}{"Extra Credit", EXAM_SCORE_COL_PADDING}\n"
);
foreach (var (name, grades, extraCredit) in students)
{
  int gradedAssignmentCount = grades.Length;
  int examScoresSum = grades.Sum();
  decimal examScore = (decimal)examScoresSum / gradedAssignmentCount;
  decimal extraCreditPoints = 0;
  decimal extraCreditAverage = 0;
  if (extraCredit is not null)
  {
    extraCreditPoints = (decimal)extraCredit.Sum() / 10 / gradedAssignmentCount;
    extraCreditAverage = (decimal)extraCredit.Average();
  }
  decimal finalGrade = examScore + extraCreditPoints;
  string letterGrade = finalGrade switch
  {
    >= 97 => "A+",
    >= 93 => "A",
    >= 90 => "A-",
    >= 87 => "B+",
    >= 83 => "B",
    >= 80 => "B-",
    >= 77 => "C+",
    >= 73 => "C",
    >= 70 => "C-",
    >= 67 => "D+",
    >= 63 => "D",
    >= 60 => "D-",
    _ => "F",
  };
  Console.WriteLine(
    $"{$"{name}:", STUDENT_COL_PADDING}{examScore, EXAM_SCORE_COL_PADDING}{$"{finalGrade, OVERALL_SCORE_AVERAGE_SPACE}{letterGrade}", OVERALL_SCORE_COL_PADDING}{$"{extraCreditAverage} ({extraCreditPoints} pts)", EXTRA_CREDIT_COL_PADDING}"
  );
}
Console.WriteLine("Press the Enter key to continue");
Console.ReadLine();
