using System.Collections.Generic;
using Avalonia;
using Avalonia.Controls;
using Avalonia.Input;
using Avalonia.Interactivity;
using Avalonia.Markup.Xaml;
using MySql.Data.MySqlClient;
using Project2.Classes;

namespace Project2.Windows;

public partial class ScheduleWin : Window
{
    private string _constring = "SERVER=sql12.freesqldatabase.com;DATABASE=sql12659906;UID=sql12659906;PASSWORD=fkfP8wwq9S;";
    private List<ScheduleClass> _schedules;
    private MySqlConnection _connection;
    private string fulltable = "SELECT Schedule.ID, StudentGroup, Subject, Schedule.Date, " +
                               "StudentGroups.FullName, Subjects.Name " +
                               "FROM Schedule " +
                               "JOIN StudentGroups on Schedule.StudentGroup = StudentGroups.ID " +
                               "JOIN Subjects on Schedule.Subject = Subjects.ID";

    public ScheduleWin()
    {
        InitializeComponent();
        ShowTable(fulltable);
    }
    
    public void ShowTable(string sql)
    {
        _schedules = new List<ScheduleClass>();
        _connection = new MySqlConnection(_constring);
        _connection.Open();
        MySqlCommand command = new MySqlCommand(sql, _connection);
        MySqlDataReader reader = command.ExecuteReader();
        while (reader.Read() && reader.HasRows)
        {
            var current = new ScheduleClass()
            {
                Schedule_ID = reader.GetInt32("ID"),
                Schedule_StudentGroup = reader.GetString("FullName"),
                Schedule_Subject = reader.GetString("Name"),
                Schedule_Date = reader.GetDateTime("Date")
            };
            _schedules.Add(current);
        }

        _connection.Close();
        DGrid.ItemsSource = _schedules;
    }

    private void Button_OnClick_PrevWin(object? sender, RoutedEventArgs e)
    {
        MainWindow win = new MainWindow();
        win.Show();
        this.Close();
    }

    private void Button_OnClick_AddWin(object? sender, RoutedEventArgs e)
    {
        ScheduleAddWin win = new ScheduleAddWin();
        win.Show();
    }

    private void Button_OnClick_Delete(object? sender, RoutedEventArgs e)
    {
        using (var conn = new MySqlConnection(_constring))
        {
            conn.Open();
            using (var cmd = new MySqlCommand("DELETE FROM Schedule WHERE ID LIKE " + IDTextBox.Text, conn))
            {
                cmd.ExecuteNonQuery();
                ShowTable(fulltable);
            }
        }
    }

    private void InputElement_OnGotFocus(object? sender, GotFocusEventArgs e)
    {
        ShowTable(fulltable);
    }
}