using Avalonia;
using Avalonia.Controls;
using Avalonia.Interactivity;
using Avalonia.Markup.Xaml;
using MySql.Data.MySqlClient;

namespace Project2.Windows;

public partial class DistantSystemAddWin : Window
{
    private string _constring = "SERVER=localhost;" +
                                "DATABASE=project2;" +
                                "UID=root;" +
                                "PASSWORD=admin1;";
    private MySqlConnection _connection;
    public DistantSystemAddWin()
    {
        InitializeComponent();
    }

    private void AddButton(object? sender, RoutedEventArgs e)
    {
        string sql = "INSERT INTO DistantSystems VALUES('" + IDTextBox.Text + "' , '" + NameTextBox.Text + "' , '" + SecondNameTextBox.Text + "' , '" + ThirdNameTextBox.Text  + "')";
        _connection = new MySqlConnection(_constring);
        _connection.Open();
        MySqlCommand command = new MySqlCommand(sql, _connection);
        command.ExecuteNonQuery();
        _connection.Close();
        this.Close();
    }
}