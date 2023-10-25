using System.Text.RegularExpressions;
using Avalonia.Controls;
using Avalonia.Interactivity;
using Project2.Classes;
using Project2.Windows;

namespace Project2;

public partial class MainWindow : Window
{
    public MainWindow()
    {
        InitializeComponent();
    }

    private void Button_OnClick_StudentsWin(object? sender, RoutedEventArgs e)
    {
        StudentsWin win = new StudentsWin();
        win.Show();
        this.Close();
    }

    private void Button_OnClick_StudentGroupsWin(object? sender, RoutedEventArgs e)
    {
        StudentGroupsWin win = new StudentGroupsWin();
        win.Show();
        this.Close();
    }

    private void Button_OnClick_GradesWin(object? sender, RoutedEventArgs e)
    {
        GradesWin win = new GradesWin();
        win.Show();
        this.Close();
    }

    private void Button_OnClick_ScheduleWin(object? sender, RoutedEventArgs e)
    {
        ScheduleWin win = new ScheduleWin();
        win.Show();
        this.Close();
    }

    private void Button_OnClick_SubjectWin(object? sender, RoutedEventArgs e)
    {
        SubjectWin win = new SubjectWin();
        win.Show();
        this.Close();
    }

    private void Button_OnClick_LibrariesWin(object? sender, RoutedEventArgs e)
    {
        OnlineLibrariesWin win = new OnlineLibrariesWin();
        win.Show();
        this.Close();
    }

    private void Button_OnClick_CoursesWin(object? sender, RoutedEventArgs e)
    {
        OnlineCoursesWin win = new OnlineCoursesWin();
        win.Show();
        this.Close();
    }

    private void Button_OnClick_DistantSystemsWin(object? sender, RoutedEventArgs e)
    {
        DistantSystemsWin win = new DistantSystemsWin();
        win.Show();
        this.Close();
    }

    private void Button_OnClick_OnlineLibraryDistSysWin(object? sender, RoutedEventArgs e)
    {
        OnlineLibraryDistSysWin win = new OnlineLibraryDistSysWin();
        win.Show();
        this.Close();
    }

    private void Button_OnClick_OnlineCourseDistSysWin(object? sender, RoutedEventArgs e)
    {
        OnlineCourseDistSysWin win = new OnlineCourseDistSysWin();
        win.Show();
        this.Close();
    }
}