using static System.Console;

await foreach (int number in GetNumbersAsync())
{
  WriteLine($"Number: {number}");
}

async static IAsyncEnumerable<int> GetNumbersAsync()
{
  Random r = new();

  // simulate work
  await Task.Delay(r.Next(1500, 3000));
  yield return r.Next(0, 1001);

  await Task.Delay(r.Next(1500, 3000));
  yield return r.Next(0, 1001);

  await Task.Delay(r.Next(1500, 3000));
  yield return r.Next(0, 1001);
}
