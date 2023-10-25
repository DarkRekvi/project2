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

public partial class OnlineLibrariesWin : Window
{
    private string _constring = "SERVER=localhost;" +
                                "DATABASE=Project2;" +
                                "UID=root;" +
                                "PASSWORD=admin1;";
    private List<OnlineLibrariesClass> _libraries;
    private MySqlConnection _connection;
    private string fulltable = "SELECT * FROM OnlineLibraries";

    public OnlineLibrariesWin()
    {
        InitializeComponent();
        ShowTable(fulltable);
    }
    
    public void ShowTable(string sql)
    {
        _libraries = new List<OnlineLibrariesClass>();
        _connection = new MySqlConnection(_constring);
        _connection.Open();
        MySqlCommand command = new MySqlCommand(sql, _connection);
        MySqlDataReader reader = command.ExecuteReader();
        while (reader.Read() && reader.HasRows)
        {
            var current = new OnlineLibrariesClass()
            {
                OnlineLibrary_ID = reader.GetInt32("ID"),
                OnlineLibrary_Name = reader.GetString("Name"),
                OnlineLibrary_Descryption = reader.GetString("Descryption"),
                OnlineLibrary_Link = reader.GetString("Link")
            };
            _libraries.Add(current);
        }

        _connection.Close();
        OnlineLibrariesGrid.ItemsSource = _libraries;
    }

    private void Button_OnClick_PrevWin(object? sender, RoutedEventArgs e)
    {
        MainWindow win = new MainWindow();
        win.Show();
        this.Close();
    }

    private void Button_OnClick_AddStudentWin(object? sender, RoutedEventArgs e)
    {
        LibraryAddWin win = new LibraryAddWin();
        win.Show();
    }

    private void Button_OnClick_DeleteStudent(object? sender, RoutedEventArgs e)
    {
        using (var conn = new MySqlConnection(_constring))
        {
            conn.Open();
            using (var cmd = new MySqlCommand("DELETE FROM OnlineLibraries WHERE ID LIKE " + StudentIDTextBox.Text, conn))
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
        string searchSql = "SELECT * FROM OnlineLibraries " +
                           "WHERE Name LIKE '%" +
                           txtSearch.Text + "%';";
        ShowTable(searchSql);
    }
}