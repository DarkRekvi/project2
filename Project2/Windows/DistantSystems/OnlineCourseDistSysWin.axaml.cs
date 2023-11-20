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

public partial class OnlineCourseDistSysWin : Window
{
    private string _constring = "SERVER=sql12.freesqldatabase.com;DATABASE=sql12659906;UID=sql12659906;PASSWORD=fkfP8wwq9S;";
    private List<OnlineCourseDistSysClass> _onlinecourcedistsystems;
    private MySqlConnection _connection;
    private string fulltable = "SELECT OnlineCourseDistSys.ID, OnlineCourseDistSys.OnlineCourse, OnlineCourses.CourseName, OnlineCourseDistSys.DistantSystem, DistantSystems.SystemName FROM OnlineCourseDistSys " +
    "JOIN OnlineCourses on OnlineCourseDistSys.OnlineCourse = OnlineCourses.ID " +
    "JOIN DistantSystems on OnlineCourseDistSys.DistantSystem = DistantSystems.ID";


    public OnlineCourseDistSysWin()
    {
        InitializeComponent();
        ShowTable(fulltable);
    }
    
    public void ShowTable(string sql)
    {
        _onlinecourcedistsystems = new List<OnlineCourseDistSysClass>();
        _connection = new MySqlConnection(_constring);
        _connection.Open();
        MySqlCommand command = new MySqlCommand(sql, _connection);
        MySqlDataReader reader = command.ExecuteReader();
        while (reader.Read() && reader.HasRows)
        {
            var current = new OnlineCourseDistSysClass()
            {
                OnlineCourseDistSys_ID = reader.GetInt32("ID"),
                OnlineCourseDistSys_OnlineCourse = reader.GetString("CourseName"),
                OnlineCourseDistSys_DistantSystem = reader.GetString("SystemName")
            };
            _onlinecourcedistsystems.Add(current);
        }

        _connection.Close();
        OnlineCourceDistSysGrid.ItemsSource = _onlinecourcedistsystems;
    }

    private void Button_OnClick_PrevWin(object? sender, RoutedEventArgs e)
    {
        MainWindow win = new MainWindow();
        win.Show();
        this.Close();
    }

    private void Button_OnClick_AddStudentWin(object? sender, RoutedEventArgs e)
    {
        OnlineCourseDistSysAddWin win = new OnlineCourseDistSysAddWin();
        win.Show();
    }

    private void Button_OnClick_DeleteStudent(object? sender, RoutedEventArgs e)
    {
        using (var conn = new MySqlConnection(_constring))
        {
            conn.Open();
            using (var cmd = new MySqlCommand("DELETE FROM OnlineCourseDistSys WHERE ID LIKE " + StudentIDTextBox.Text, conn))
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