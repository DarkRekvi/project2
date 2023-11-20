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

public partial class SubjectWin : Window
{
    private string _constring = "SERVER=sql12.freesqldatabase.com;DATABASE=sql12659906;UID=sql12659906;PASSWORD=fkfP8wwq9S;";
    private List<SubjectClass> _subjects;
    private MySqlConnection _connection;
    private string fulltable = "SELECT Subjects.ID, Name, Auditory, Subjects.DistantSystem, DistantSystems.SystemName FROM Subjects " +
    "JOIN DistantSystems on Subjects.DistantSystem = DistantSystems.ID";


    public SubjectWin()
    {
        InitializeComponent();
        ShowTable(fulltable);
    }
    
    public void ShowTable(string sql)
    {
        _subjects = new List<SubjectClass>();
        _connection = new MySqlConnection(_constring);
        _connection.Open();
        MySqlCommand command = new MySqlCommand(sql, _connection);
        MySqlDataReader reader = command.ExecuteReader();
        while (reader.Read() && reader.HasRows)
        {
            var current = new SubjectClass()
            {
                Subject_ID = reader.GetInt32("ID"),
                Subject_Name = reader.GetString("Name"),
                Subject_Auditory = reader.GetString("Auditory"),
                Subject_DistantSystem = reader.GetString("SystemName")
            };
            _subjects.Add(current);
        }

        _connection.Close();
        SubjectsGrid.ItemsSource = _subjects;
    }

    private void Button_OnClick_PrevWin(object? sender, RoutedEventArgs e)
    {
        MainWindow win = new MainWindow();
        win.Show();
        this.Close();
    }

    private void Button_OnClick_AddStudentWin(object? sender, RoutedEventArgs e)
    {
        SubjectAddWin win = new SubjectAddWin();
        win.Show();
    }

    private void Button_OnClick_Delete(object? sender, RoutedEventArgs e)
    {
        using (var conn = new MySqlConnection(_constring))
        {
            conn.Open();
            using (var cmd = new MySqlCommand("DELETE FROM Subjects WHERE ID LIKE " + IDTextBox.Text, conn))
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