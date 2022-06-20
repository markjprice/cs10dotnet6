int numberOfApples = 12;
decimal pricePerApple = 0.35M;

Console.WriteLine(
  format: "{0} apples costs {1:C}",
  arg0: numberOfApples,
  arg1: pricePerApple * numberOfApples);

//The WriteToFile method is a nonexistent method used to illustrate the idea.

//Next exercise
string applesText = "Apples";
int applesCount = 1234;
string bananasText = "Bananas";
int bananasCount = 56789;

//Inputs the table headers
Console.WriteLine(
  format: "{0,-10} {1,6:N0}",
  arg0: "Name",
  arg1: "Count");

//Specifies the rest of the tabular format
Console.WriteLine(
  format: "{0,-10} {1,6:N0}",
  arg0: applesText,
  arg1: applesCount);
Console.WriteLine(
  format: "{0,-10} {1,6:N0}",
  arg0: bananasText,
  arg1: bananasCount);