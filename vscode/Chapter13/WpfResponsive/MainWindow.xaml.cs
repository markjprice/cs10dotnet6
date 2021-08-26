using Microsoft.Data.SqlClient;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace WpfResponsive
{
  /// <summary>
  /// Interaction logic for MainWindow.xaml
  /// </summary>
  public partial class MainWindow : Window
  {
    public MainWindow()
    {
      InitializeComponent();
    }

    private const string connectionString = 
      "Data Source=.;" +
      "Initial Catalog=Northwind;" +
      "Integrated Security=true;" +
      "MultipleActiveResultSets=true;";

    private const string sql =
      "WAITFOR DELAY '00:00:05';" +
      "SELECT EmployeeId, FirstName, LastName FROM Employees";

    private void GetEmployeesSyncButton_Click(object sender, RoutedEventArgs e)
    {
      Stopwatch timer = Stopwatch.StartNew();
      using (SqlConnection connection = new(connectionString))
      {
        connection.Open();
        SqlCommand command = new(sql, connection);
        SqlDataReader reader = command.ExecuteReader();

        while (reader.Read())
        {
          string employee = string.Format("{0}: {1} {2}",
            reader.GetInt32(0), reader.GetString(1), reader.GetString(2));

          EmployeesListBox.Items.Add(employee);
        }
        reader.Close();
        connection.Close();
      }
      EmployeesListBox.Items.Add($"Sync: {timer.ElapsedMilliseconds:N0}ms");
    }

    private async void GetEmployeesAsyncButton_Click(object sender, RoutedEventArgs e)
    {
      Stopwatch timer = Stopwatch.StartNew();
      using (SqlConnection connection = new(connectionString))
      {
        await connection.OpenAsync();
        SqlCommand command = new(sql, connection);
        SqlDataReader reader = await command.ExecuteReaderAsync();

        while (await reader.ReadAsync())
        {
          string employee = string.Format("{0}: {1} {2}",
            await reader.GetFieldValueAsync<int>(0), 
            await reader.GetFieldValueAsync<string>(1), 
            await reader.GetFieldValueAsync<string>(2));

          EmployeesListBox.Items.Add(employee);
        }
        await reader.CloseAsync();
        await connection.CloseAsync();
      }
      EmployeesListBox.Items.Add($"Async: {timer.ElapsedMilliseconds:N0}ms");
    }
  }
}