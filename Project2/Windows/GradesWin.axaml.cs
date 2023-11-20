using System.Collections.Generic;
using Avalonia;
using Avalonia.Controls;
using Avalonia.Input;
using Avalonia.Interactivity;
using Avalonia.Markup.Xaml;
using MySql.Data.MySqlClient;
using Project2.Classes;

namespace Project2.Windows;

public partial class GradesWin : Window
{
    private string _constring = "SERVER=sql12.freesqldatabase.com;DATABASE=sql12659906;UID=sql12659906;PASSWORD=fkfP8wwq9S;";
    private List<GradesClass> _grades;
    private MySqlConnection _connection;
    private string fulltable = "SELECT Grades.ID, Student, Subject, Grade, " +
                               "Students.SecondName, Subjects.Name " +
                               "FROM Grades " +
                               "JOIN Students on Grades.Student = Students.ID " +
                               "JOIN Subjects on Grades.Subject = Subjects.ID";

    public GradesWin()
    {
        InitializeComponent();
        ShowTable(fulltable);
    }
    
    public void ShowTable(string sql)
    {
        _grades = new List<GradesClass>();
        _connection = new MySqlConnection(_constring);
        _connection.Open();
        MySqlCommand command = new MySqlCommand(sql, _connection);
        MySqlDataReader reader = command.ExecuteReader();
        while (reader.Read() && reader.HasRows)
        {
            var current = new GradesClass()
            {
                Grades_ID = reader.GetInt32("ID"),
                Grades_Student = reader.GetString("SecondName"),
                Grades_Subject = reader.GetString("Name"),
                Grades_Grade = reader.GetInt32("Grade")
            };
            _grades.Add(current);
        }

        _connection.Close();
        DGrid.ItemsSource = _grades;
    }

    private void Button_OnClick_PrevWin(object? sender, RoutedEventArgs e)
    {
        MainWindow win = new MainWindow();
        win.Show();
        this.Close();
    }

    private void Button_OnClick_AddWin(object? sender, RoutedEventArgs e)
    {
        GradeAddWin win = new GradeAddWin();
        win.Show();
    }

    private void Button_OnClick_Delete(object? sender, RoutedEventArgs e)
    {
        using (var conn = new MySqlConnection(_constring))
        {
            conn.Open();
            using (var cmd = new MySqlCommand("DELETE FROM Grades WHERE ID LIKE " + IDTextBox.Text, conn))
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