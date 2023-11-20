using Avalonia;
using Avalonia.Controls;
using Avalonia.Interactivity;
using Avalonia.Markup.Xaml;
using MySql.Data.MySqlClient;

namespace Project2.Windows;

public partial class GradeAddWin : Window
{
    private string _constring = "SERVER=sql12.freesqldatabase.com;DATABASE=sql12659906;UID=sql12659906;PASSWORD=fkfP8wwq9S;";
    private MySqlConnection _connection;
    public GradeAddWin()
    {
        InitializeComponent();
    }

    private void AddButton(object? sender, RoutedEventArgs e)
    {
        string sql = "INSERT INTO Grades VALUES('" + IDTextBox.Text + "' , '" + NameTextBox.Text + "' , '" + SecondNameTextBox.Text + "' , '" + ThirdNameTextBox.Text  + "')";
        _connection = new MySqlConnection(_constring);
        _connection.Open();
        MySqlCommand command = new MySqlCommand(sql, _connection);
        command.ExecuteNonQuery();
        _connection.Close();
        this.Close();
    }
}