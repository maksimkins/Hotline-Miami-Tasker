using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace Hotline_Miami_Tasks.Models;

public class User
{
    const string tasksPath = "assets/Tasks.json";

    public string? Login { get; set; }
    public string? Password { get; set; }
    public List<Task>? TasksThoseLeftToDo { get; set; }

    private User(string? login, string? password)
    {
        Login = login;
        Password = password;
        this.DownloadTasks();
    }

    public User() { }

    private void DownloadTasks()
    {
        string allTasks = File.ReadAllText(tasksPath);
        TasksThoseLeftToDo = JsonSerializer.Deserialize<List<Task>>(allTasks);
    }

    private static bool IsLoginGood(string? login)
    {
        if (login is null)
            throw new ArgumentNullException("login cant't be null");

        if (login.All(s => char.IsDigit(s)))
            throw new ArgumentException("login can't consist only of digits");

        if (login.All(s => char.IsLetter(s)))
            throw new ArgumentException("login can't consist only of letters");

        foreach (var symbol in login)
        {
            if (char.IsDigit(symbol) || char.IsLetter(symbol))
                continue;

            throw new ArgumentException("login must consist of letters and digits");
        }

        return true;
    }

    private static bool IsPasswordGood(string? password) 
    {       
        if (password is null)
            throw new ArgumentNullException("password cant't be null");

        var passwordWithoutSpecialSymbols = password.Where(s => char.IsDigit(s) || char.IsLetter(s));

        var passwordWithOnlySpecialSymbols = password.Where(s => !char.IsDigit(s) && !char.IsLetter(s));

        if (passwordWithOnlySpecialSymbols is null)
            throw new ArgumentException("no special symbol in password");

        if (passwordWithoutSpecialSymbols.All(s => char.IsDigit(s)))
            throw new ArgumentException("password can't consist only of digits");

        if (passwordWithoutSpecialSymbols.All(s => char.IsLetter(s)))
            throw new ArgumentException("password can't consist only of letters");

        var specialSymbols = "*$%";

        foreach (var s in passwordWithOnlySpecialSymbols)
        {
            if (!specialSymbols.Contains(s))
                throw new ArgumentException("contains smth different");
        }

        return true;
    }

    public static User CreateUser(string? login, string? password)
    {

        var isLoginGood = User.IsLoginGood(login);
        var isPasswordGood = User.IsPasswordGood(password);

        if(!isLoginGood || !isPasswordGood)
        {
            throw new ArgumentException("error occured");
        }

        return new User(login, password);
    }

}
