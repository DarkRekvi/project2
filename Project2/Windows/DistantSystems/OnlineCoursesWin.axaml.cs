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

public partial class OnlineCoursesWin : Window
{
    private string _constring = "SERVER=sql12.freesqldatabase.com;DATABASE=sql12659906;UID=sql12659906;PASSWORD=fkfP8wwq9S;";
    private List<OnlineCoursesClass> _courses;
    private MySqlConnection _connection;
    private string fulltable = "SELECT * FROM OnlineCourses";

    public OnlineCoursesWin()
    {
        InitializeComponent();
        ShowTable(fulltable);
    }
    
    public void ShowTable(string sql)
    {
        _courses = new List<OnlineCoursesClass>();
        _connection = new MySqlConnection(_constring);
        _connection.Open();
        MySqlCommand command = new MySqlCommand(sql, _connection);
        MySqlDataReader reader = command.ExecuteReader();
        while (reader.Read() && reader.HasRows)
        {
            var current = new OnlineCoursesClass()
            {
                OnlineCourse_ID = reader.GetInt32("ID"),
                OnlineCourse_Name = reader.GetString("CourseName"),
                OnlineCourse_Descryption = reader.GetString("Descryption"),
                OnlineCourse_Link = reader.GetString("Link")
            };
            _courses.Add(current);
        }

        _connection.Close();
        OnlineCoursesGrid.ItemsSource = _courses;
    }

    private void Button_OnClick_PrevWin(object? sender, RoutedEventArgs e)
    {
        MainWindow win = new MainWindow();
        win.Show();
        this.Close();
    }

    private void Button_OnClick_AddStudentWin(object? sender, RoutedEventArgs e)
    {
        CourseAddWin win = new CourseAddWin();
        win.Show();
    }

    private void Button_OnClick_DeleteStudent(object? sender, RoutedEventArgs e)
    {
        using (var conn = new MySqlConnection(_constring))
        {
            conn.Open();
            using (var cmd = new MySqlCommand("DELETE FROM OnlineCourses WHERE ID LIKE " + StudentIDTextBox.Text, conn))
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