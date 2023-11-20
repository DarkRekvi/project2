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

public partial class OnlineLibraryDistSysWin : Window
{
    private string _constring = "SERVER=sql12.freesqldatabase.com;DATABASE=sql12659906;UID=sql12659906;PASSWORD=fkfP8wwq9S;";
    private List<OnlineLibraryDistSysClass> _onlinelibrarydistsystems;
    private MySqlConnection _connection;
    private string fulltable = "SELECT OnlineLibraryDistSys.ID, OnlineLibraryDistSys.OnlineLibrary, OnlineLibraries.LibraryName, OnlineLibraryDistSys.DistantSystem, DistantSystems.SystemName FROM OnlineLibraryDistSys " +
    "JOIN OnlineLibraries on OnlineLibraryDistSys.OnlineLibrary = OnlineLibraries.ID " +
    "JOIN DistantSystems on OnlineLibraryDistSys.DistantSystem = DistantSystems.ID";


    public OnlineLibraryDistSysWin()
    {
        InitializeComponent();
        ShowTable(fulltable);
    }
    
    public void ShowTable(string sql)
    {
        _onlinelibrarydistsystems = new List<OnlineLibraryDistSysClass>();
        _connection = new MySqlConnection(_constring);
        _connection.Open();
        MySqlCommand command = new MySqlCommand(sql, _connection);
        MySqlDataReader reader = command.ExecuteReader();
        while (reader.Read() && reader.HasRows)
        {
            var current = new OnlineLibraryDistSysClass()
            {
                OnlineLibraryDistSys_ID = reader.GetInt32("ID"),
                OnlineLibraryDistSys_OnlineLibrary = reader.GetString("LibraryName"),
                OnlineLibraryDistSys_DistantSystem = reader.GetString("SystemName")
            };
            _onlinelibrarydistsystems.Add(current);
        }

        _connection.Close();
        OnlineLibraryDistSysGrid.ItemsSource = _onlinelibrarydistsystems;
    }

    private void Button_OnClick_PrevWin(object? sender, RoutedEventArgs e)
    {
        MainWindow win = new MainWindow();
        win.Show();
        this.Close();
    }

    private void Button_OnClick_AddStudentWin(object? sender, RoutedEventArgs e)
    {
        OnlineLibraryDistSysAddWin win = new OnlineLibraryDistSysAddWin();
        win.Show();
    }

    private void Button_OnClick_DeleteStudent(object? sender, RoutedEventArgs e)
    {
        using (var conn = new MySqlConnection(_constring))
        {
            conn.Open();
            using (var cmd = new MySqlCommand("DELETE FROM OnlineLibraryDistSys WHERE ID LIKE " + StudentIDTextBox.Text, conn))
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