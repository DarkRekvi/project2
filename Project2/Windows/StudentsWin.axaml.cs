using System;
using System.Collections.Generic;
using System.ComponentModel;
using Avalonia;
using Avalonia.Controls;
using Avalonia.Input;
using Avalonia.Interactivity;
using Avalonia.Markup.Xaml;
using MySql.Data.MySqlClient;
using Org.BouncyCastle.Asn1.Ocsp;
using Project2.Classes;

namespace Project2.Windows;

public partial class StudentsWin : Window
{
    private string _constring = "SERVER=sql12.freesqldatabase.com;DATABASE=sql12659906;UID=sql12659906;PASSWORD=fkfP8wwq9S;";
    private List<StudentClass> _students;
    private MySqlConnection _connection;
    private string fulltable = "SELECT * FROM Students";

    public StudentsWin()
    {
        InitializeComponent();
        ShowTable(fulltable);
    }
    
    public void ShowTable(string sql)
    {
        _students = new List<StudentClass>();
        _connection = new MySqlConnection(_constring);
        _connection.Open();
        MySqlCommand command = new MySqlCommand(sql, _connection);
        MySqlDataReader reader = command.ExecuteReader();
        while (reader.Read() && reader.HasRows)
        {
            var current = new StudentClass()
            {
                Student_ID = reader.GetInt32("ID"),
                Student_Name = reader.GetString("Name"),
                Student_SecondName = reader.GetString("SecondName"),
                Student_ThirdName = reader.GetString("ThirdName")
            };
            _students.Add(current);
        }

        _connection.Close();
        StudentsGrid.ItemsSource = _students;
    }

    private void Button_OnClick_PrevWin(object? sender, RoutedEventArgs e)
    {
        MainWindow win = new MainWindow();
        win.Show();
        this.Close();
    }

    private void Button_OnClick_AddStudentWin(object? sender, RoutedEventArgs e)
    {
        StudentAddWin win = new StudentAddWin();
        win.Show();
    }

    private void Button_OnClick_Delete(object? sender, RoutedEventArgs e)
    {
        using (var conn = new MySqlConnection(_constring))
        {
            conn.Open();
            using (var cmd = new MySqlCommand("DELETE FROM Students WHERE ID LIKE " + IDTextBox.Text, conn))
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
    
    private void TxtSearch_OnTextChanged(object? sender, TextChangedEventArgs e)
    {
        string searchSql = "SELECT * FROM Students " +
                           "WHERE SecondName LIKE '%" +
                           txtSearch.Text + "%';";
        ShowTable(searchSql);
    }
}