using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
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

/// <summary>
/// Interaction logic for Window1.xaml
/// </summary>
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
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(TaskLabel)));
        }
    }

   /* private void choseTask()              выбирает задание и записывает в лейбл
    {
        if (user.TasksThoseLeftToDo is null || user.TasksThoseLeftToDo.Count == 0)
        {
            MessageBox.Show("There is no tasks left!");
            this.Close();
            mw?.Close();
        }

        Func<Models.Task, bool> func;

        if (user.TasksThoseLeftToDo.Any(task => task.Lvl == lvlEnum.f1v1))
        {
            func = task => task.Lvl == lvlEnum.f1v1;
        }
        else if(user.TasksThoseLeftToDo.Any(task => task.Lvl == lvlEnum.s1v1))
        {
            func = task => task.Lvl == lvlEnum.s1v1;
        }
        else
        {
            func = task => task.Lvl == lvlEnum.t1v1;
        }

        List<Models.Task> correctTasks = user.TasksThoseLeftToDo.Where(func).ToList();

        CurentTask = correctTasks[new Random().Next(0, correctTasks.Count)];
        TaskLabel = CurentTask.TaskDescription;
    } */


    /*private void TaskDelter()           удаляет текущее задание (будет вызывать если игрок пракильно угадал lvl задания)
    {
        user?.TasksThoseLeftToDo?.Remove(this.CurentTask);
    }*/


    // сделать пересохранение всех юзеров (изменение того юзера который сейчас играет)

    // реализовать эвенты кнопок (если выбрал верно, обновляй лейбл, удаляй задание из оставшихся)


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
