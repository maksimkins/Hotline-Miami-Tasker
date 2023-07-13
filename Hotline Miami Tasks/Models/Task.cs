using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hotline_Miami_Tasks.Models;

public enum lvlEnum
{
	f1v1 = 1,
	s1v1 = 2,
	t1v1 = 3,
}

public class Task
{
	public string? TaskDescription { set; get; }

    public lvlEnum Lvl { set; get; }

	public Task() { }

    public Task(string? TaskDescription, lvlEnum lvl) 
	{
		this.TaskDescription = TaskDescription;
		this.Lvl = lvl;

    }	

}
