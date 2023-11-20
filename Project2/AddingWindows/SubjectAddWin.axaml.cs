using Avalonia;
using Avalonia.Controls;
using Avalonia.Interactivity;
using Avalonia.Markup.Xaml;
using MySql.Data.MySqlClient;

namespace Project2.Windows;

public partial class SubjectAddWin : Window
{
    private string _constring = "SERVER=sql12.freesqldatabase.com;DATABASE=sql12659906;UID=sql12659906;PASSWORD=fkfP8wwq9S;";
    private MySqlConnection _connection;
    public SubjectAddWin()
    {
        InitializeComponent();
        
        using (var connection = new MySqlConnection(_constring))
        {
            connection.Open();
            var query = "SELECT ID FROM DistantSystems";
            using (var command = new MySqlCommand(query, connection))
            {
                using (var reader = command.ExecuteReader())
                {
                    //Iterate through the rows and add it to the combobox's items
                    while (reader.Read())
                    {
                        DistantSystemsComboBox.Items.Add(reader.GetInt32("ID"));    
                    }
                }    
            }
        }
    }

    private void AddButton(object? sender, RoutedEventArgs e)
    {
        string sql = "INSERT INTO Subjects VALUES('" + IDTextBox.Text + "' , '" + NameTextBox.Text + "' , '" + AuditoryTextBox.Text +"' , '" + DistantSystemsComboBox.SelectedValue + "')";
        _connection = new MySqlConnection(_constring);
        _connection.Open();
        MySqlCommand command = new MySqlCommand(sql, _connection);
        command.ExecuteNonQuery();
        _connection.Close();
        this.Close();
    }
}