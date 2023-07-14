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
using System.Windows.Shapes;
using Hotline_Miami_Tasks.Models;

namespace Hotline_Miami_Tasks;

public partial class Window1 : Window, INotifyPropertyChanged
{
    MainWindow? mw;
    User user;

    Models.Task CurentTask;

    public string? taskLabel;

    public string? TaskLabel
    {
        get => taskLabel;
        set 
        {
            this.PropertyChangeMethod(out this.taskLabel, value);
        }
    }
    protected void PropertyChangeMethod<T>(out T field, T value, [CallerMemberName] string propName = "")
    {
        field = value;

        if (this.PropertyChanged != null)
        {
            this.PropertyChanged.Invoke(this, new PropertyChangedEventArgs(propName));
        }
    }
    public void choseTask()
    {
        if (user.TasksThoseLeftToDo is null || user.TasksThoseLeftToDo.Count == 0)
        {
            this.Hide();
            MessageBox.Show("There is no tasks left!");
            this.Close();
            mw?.Close();
        }

        Func<Models.Task, bool> func;

        if (user.TasksThoseLeftToDo.Any(task => task.Lvl == lvlEnum.flvl))
        {
            func = task => task.Lvl == lvlEnum.flvl;
        }
        else if (user.TasksThoseLeftToDo.Any(task => task.Lvl == lvlEnum.slvl))
        {
            func = task => task.Lvl == lvlEnum.slvl;
        }
        else 
        {
            func = task => task.Lvl == lvlEnum.tlvl;
        }

        List<Models.Task> correctTasks = user.TasksThoseLeftToDo.Where(func).ToList();

        CurentTask = correctTasks[new Random().Next(0, correctTasks.Count)];
        TaskLabel = CurentTask.TaskDescription;
    }

    private void TaskDeleter()          
    {
        user?.TasksThoseLeftToDo?.Remove(this.CurentTask);
    }

    private void ButtonClick(object sender, RoutedEventArgs e)
    {
        if(sender is Button button)
        {
            lvlEnum? lvl = null; 

            //if (button.Content != CurentTask.Lvl) //i don't know why it doesn't work this way. i have a thought that the problem is in encoding
            //    return;

            switch (button.Content)
            {
                case "flvl":
                    lvl = lvlEnum.flvl;
                    break;

                case "slvl":
                    lvl = lvlEnum.slvl;
                    break;

                case "tlvl":
                    lvl = lvlEnum.tlvl;
                    break;
            }

            if (lvl is null || lvl != CurentTask.Lvl)
                return;

            this.TaskDeleter();
            this.choseTask();
        }
    }

    public Window1()
    {
        InitializeComponent();
        this.DataContext = this;
    }

    public Window1(MainWindow mw, User user): this()
    {
        this.mw = mw;
        this.user = user;
    }

    protected override void OnClosing(CancelEventArgs e)
    {
        base.OnClosing(e);
        mw?.Close();
    }

    public event PropertyChangedEventHandler? PropertyChanged;
}
