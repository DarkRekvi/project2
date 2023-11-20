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

public partial class StudentGroupsWin : Window
{
    private string _constring = "SERVER=sql12.freesqldatabase.com;DATABASE=sql12659906;UID=sql12659906;PASSWORD=fkfP8wwq9S;";
    private List<StudentGroupsClass> _studentgroups;
    private MySqlConnection _connection;
    private string fulltable = "SELECT * FROM StudentGroups";

    public StudentGroupsWin()
    {
        InitializeComponent();
        ShowTable(fulltable);
    }
    
    public void ShowTable(string sql)
    {
        _studentgroups = new List<StudentGroupsClass>();
        _connection = new MySqlConnection(_constring);
        _connection.Open();
        MySqlCommand command = new MySqlCommand(sql, _connection);
        MySqlDataReader reader = command.ExecuteReader();
        while (reader.Read() && reader.HasRows)
        {
            var current = new StudentGroupsClass()
            {
                StudentGroup_ID = reader.GetInt32("ID"),
                StudentGroup_Name = reader.GetString("Name"),
                StudentGroup_FullName = reader.GetString("FullName")
            };
            _studentgroups.Add(current);
        }

        _connection.Close();
        StudentGroupsGrid.ItemsSource = _studentgroups;
    }

    private void Button_OnClick_PrevWin(object? sender, RoutedEventArgs e)
    {
        MainWindow win = new MainWindow();
        win.Show();
        this.Close();
    }

    private void Button_OnClick_AddStudentWin(object? sender, RoutedEventArgs e)
    {
        StudentGroupAddWin win = new StudentGroupAddWin();
        win.Show();
    }

    private void Button_OnClick_Delete(object? sender, RoutedEventArgs e)
    {
        using (var conn = new MySqlConnection(_constring))
        {
            conn.Open();
            using (var cmd = new MySqlCommand("DELETE FROM StudentGroups WHERE ID LIKE " + IDTextBox.Text, conn))
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
        string searchSql = "SELECT * FROM StudentGroups " +
                           "WHERE Name LIKE '%" +
                           txtSearch.Text + "%';";
        ShowTable(searchSql);
    }
}