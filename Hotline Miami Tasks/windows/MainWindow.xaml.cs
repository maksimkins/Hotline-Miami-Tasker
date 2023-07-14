using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.IO;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using Hotline_Miami_Tasks.Models;

namespace Hotline_Miami_Tasks;

/// <summary>
/// Interaction logic for MainWindow.xaml
/// </summary>
public partial class MainWindow : Window, INotifyPropertyChanged
{
    const string usersPath = "assets/Users.json";

    public event PropertyChangedEventHandler? PropertyChanged;

    private List<User>? users;

    private string? userLogin;
    public string? UserLogin
    {
        get => userLogin;
        set
        {
            this.PropertyChangeMethod(out this.userLogin, value);
        }
    }

    private string? userPassword;
    public string? UserPassword
    {
        get => userPassword;
        set
        {
            this.PropertyChangeMethod(out this.userPassword, value);
        }
    }

    private string? message;
    public string? Message
    {
        get => message;
        set
        {
            this.PropertyChangeMethod(out this.message, value);
        }
    }

    public MainWindow()
    {
        InitializeComponent();

        this.DataContext = this;
        this.DownloadUsers();

    }

    protected void PropertyChangeMethod<T>(out T field, T value, [CallerMemberName] string propName = "")
    {
        field = value;

        if (this.PropertyChanged != null)
        {
            this.PropertyChanged.Invoke(this, new PropertyChangedEventArgs(propName));
        }
    }

    private void DownloadUsers()
    {
        try
        {
            string usersStr = File.ReadAllText(usersPath);
            users = JsonSerializer.Deserialize<List<User>>(usersStr);
        }
        catch
        {
            users = new List<User>();
        }          
    }

    protected override void OnClosing(CancelEventArgs e)
    {
        this.SaveUsers();
        base.OnClosing(e);
    }

    public void SaveUsers()
    {
        string usersStr = JsonSerializer.Serialize(users);
        File.WriteAllText(usersPath, usersStr);
    }

    private void ButtonClickLogin(object sender, RoutedEventArgs e)
    {
        if(users is null || !users.Any(user => (user.Password == UserPassword && user.Login == UserLogin)))
        {
            this.Message = "there are no such user";
            return;
        }

        MessageBox.Show("logined!!");
        User user =  users.First((user => (user.Password == UserPassword && user.Login == UserLogin)));
        Window1 window = new Window1(this, user);

        window.Show();
        window.choseTask();

        this.Hide();
    }

    private void ButtonClickRegister(object sender, RoutedEventArgs e)
    {
        try
        {            
            if (this.IsUserAlreadyRegistered(UserLogin))
                throw new ArgumentException("user already registered");

            User newUser = User.CreateUser(UserLogin, UserPassword);

            if (users is null)
                users = new List<User>();

            users.Add(newUser);
            
            MessageBox.Show("registered!!");

            Window1 window = new Window1(this, newUser);
            window.Show();
            window.choseTask();
            this.Hide();           
        }
        catch (Exception ex)
        {
            this.Message = ex.Message;
        }
    }

    public bool IsUserAlreadyRegistered(string? login)
    {
        if (users is null)
            return false;

        bool IsUserAlreadyRegistered = false;

        if (users.Any(user => user.Login == login))
            IsUserAlreadyRegistered = true;

        return IsUserAlreadyRegistered;
    }

    private void ButtonClickCancel(object sender, RoutedEventArgs e)
    {
        this.SaveUsers();
        this.Close();
    }
}
