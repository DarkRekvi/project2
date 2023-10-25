using Avalonia;
using Avalonia.Controls;
using Avalonia.Interactivity;
using Avalonia.Markup.Xaml;
using MySql.Data.MySqlClient;

namespace Project2.Windows;

public partial class OnlineLibraryDistSysAddWin : Window
{
    private string _constring = "SERVER=localhost;" +
                                "DATABASE=project2;" +
                                "UID=root;" +
                                "PASSWORD=admin1;";
    private MySqlConnection _connection;
    public OnlineLibraryDistSysAddWin()
    {
        InitializeComponent();
        
        using (var connection = new MySqlConnection(_constring))
        {
            connection.Open();
            var query = "SELECT ID FROM OnlineLibraries";
            using (var command = new MySqlCommand(query, connection))
            {
                using (var reader = command.ExecuteReader())
                {
                    //Iterate through the rows and add it to the combobox's items
                    while (reader.Read())
                    {
                        OnlineLibrariesComboBox.Items.Add(reader.GetInt32("ID"));    
                    }
                }    
            }
        }
        
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
        string sql = "INSERT INTO OnlineLibraryDistSys VALUES('" + IDTextBox.Text + "' , '" + OnlineLibrariesComboBox.SelectedValue + "' , '" + DistantSystemsComboBox.SelectedValue + "')";
        _connection = new MySqlConnection(_constring);
        _connection.Open();
        MySqlCommand command = new MySqlCommand(sql, _connection);
        command.ExecuteNonQuery();
        _connection.Close();
        this.Close();
    }
}