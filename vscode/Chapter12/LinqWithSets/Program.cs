using static System.Console;

string[] cohort1 = new string[]
  { "Rachel", "Gareth", "Jonathan", "George" };

string[] cohort2 = new string[]
  { "Jack", "Stephen", "Daniel", "Jack", "Jared" };

string[] cohort3 = new string[]
  { "Declan", "Jack", "Jack", "Jasmine", "Conor" };

Output(cohort1, "Cohort 1");
Output(cohort2, "Cohort 2");
Output(cohort3, "Cohort 3");

Output(cohort2.Distinct(), "cohort2.Distinct():");
Output(cohort2.DistinctBy(name => name.Substring(0, 2)),
  "cohort2.DistinctBy(name => name.Substring(0, 2)):");
Output(cohort2.Union(cohort3), "cohort2.Union(cohort3):");
Output(cohort2.Concat(cohort3), "cohort2.Concat(cohort3):");
Output(cohort2.Intersect(cohort3), "cohort2.Intersect(cohort3):");
Output(cohort2.Except(cohort3), "cohort2.Except(cohort3):");
Output(cohort1.Zip(cohort2, (c1, c2) => $"{c1} matched with {c2}"),
  "cohort1.Zip(cohort2):");

static void Output(IEnumerable<string> cohort, string description = "")
{
  if (!string.IsNullOrEmpty(description))
  {
    WriteLine(description);
  }
  Write("  ");
  WriteLine(string.Join(", ", cohort.ToArray()));
  WriteLine();
}
