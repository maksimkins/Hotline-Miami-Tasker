using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hotline_Miami_Tasks.Models;

public enum lvlEnum
{
	flvl = 1,
    slvl = 2,
    tlvl = 3,
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
