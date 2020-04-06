using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Task
{
    public abstract void run();
    public bool succeeded;

    protected int eventId;
    const string EVENT_NAME_PREFIX = "FinishedTask";
    public string TaskFinished
    {
        get
        {
            return EVENT_NAME_PREFIX + eventId;
        }
    }
    public Task()
    {
        eventId = EventBus.GetEventID();
    }
}




public class Sequence : Task
{
    List<Task> children;
    Task currentTask;
    int currentTaskIndex = 0;

    public Sequence(List<Task> taskList)
    {
        children = taskList;
    }

    public override void run()
    {
        currentTask = children[currentTaskIndex];
        EventBus.StartListening(currentTask.TaskFinished, OnChildTaskFinished);
        currentTask.run();
    }

    void OnChildTaskFinished()
    {
        if (currentTask.succeeded)
        {
            EventBus.StopListening(currentTask.TaskFinished, OnChildTaskFinished);
            currentTaskIndex++;
            if (currentTaskIndex < children.Count)
            {
                this.run();
            }
            else
            {
                succeeded = true;
                EventBus.TriggerEvent(TaskFinished);
            }

        }
        else
        {
            succeeded = false;
            EventBus.TriggerEvent(TaskFinished);
        }
    }
}

public class Selector : Task
{
    List<Task> children;
    Task currentTask;
    int currentTaskIndex = 0;

    public Selector(List<Task> taskList)
    {
        children = taskList;
    }
    public override void run()
    {
        currentTask = children[currentTaskIndex];
        EventBus.StartListening(currentTask.TaskFinished, OnChildTaskFinished);
        currentTask.run();
    }

    void OnChildTaskFinished()
    {
        if (currentTask.succeeded)
        {
            succeeded = true;
            EventBus.TriggerEvent(TaskFinished);
        }
        else
        {
            EventBus.StopListening(currentTask.TaskFinished, OnChildTaskFinished);
            currentTaskIndex++;
            if (currentTaskIndex < children.Count)
            {
                this.run();
            }
            else
            {
                succeeded = false;
                EventBus.TriggerEvent(TaskFinished);
            }
        }
    }
	
	}



public class IsTrue : Task
{
    bool varToTest;

    public IsTrue(bool someBool)
    {
        varToTest = someBool;
        
    }

    public override void run()
    {
        succeeded = varToTest;
        EventBus.TriggerEvent(TaskFinished);
    }
}


public class IsFalse : Task
{
    bool varToTest;

    public IsFalse(bool someBool)
    {
        varToTest = someBool;
    }

    public override void run()
    {
        succeeded = !varToTest;
        EventBus.TriggerEvent(TaskFinished);
    }
}


public class shootAir: Task
{
	Creature creature;
	
	public shootAir(Creature refCreature)
	{
		creature = refCreature;
	}
	public override void run()
	{
		Debug.Log("Practice shooting in the air");
		succeeded = true;
		EventBus.TriggerEvent(TaskFinished);

	}
}
public class saveHuman: Task
{
	Creature creature;
	
	public saveHuman(Creature refCreature)
	{
		creature = refCreature;
	}
	public override void run()
	{
		Debug.Log("Saving Human");
		succeeded = true;
		EventBus.TriggerEvent(TaskFinished);

	}
}
public class walkAround: Task
{
	Creature creature;
	
	public walkAround(Creature refCreature)
	{
		creature = refCreature;
	}
	public override void run()
	{
		Debug.Log("No danger detected... walking around");
		succeeded = true;
		EventBus.TriggerEvent(TaskFinished);

	}
}
public class shootHuman : Task
{
	Creature creature;
	public shootHuman(Creature refCreature)
	{
		creature = refCreature;
	}
	public override void run()
	{
		Debug.Log("Shooting Human");
		succeeded = true;
		EventBus.TriggerEvent(TaskFinished);
	}
}
public class shootCreature: Task
{
	Creature creature;
	public shootCreature(Creature refCreature)
	{
		creature = refCreature;
	}
	public override void run()
	{
		Debug.Log("Shooting at Creature");
		succeeded = true;
		EventBus.TriggerEvent(TaskFinished);

	}
}
public class petDog: Task
{
	Creature creature;
	public petDog(Creature refCreature)
	{
		creature = refCreature;
	}
	public override void run()
	{
		Debug.Log("petting Dog");
		succeeded = true;
		EventBus.TriggerEvent(TaskFinished);
	}
}
public class takeDog: Task
{
	Creature creature;
	public takeDog(Creature refCreature)
	{
		creature = refCreature;
	}
	public override void run()
	{
		Debug.Log("taking Dog");
		succeeded = true;
		EventBus.TriggerEvent(TaskFinished);
	}
}

//
//public interface ITask 
//{
//	 bool run();
//}
//public class  Selector : ITask
//{
//	List<ITask> children;
//	public Selector(List<ITask> taskList)
//    {
//        children = taskList;
//    }
//	public bool run()
//	{
//		foreach (ITask c in children)
//		{
//			if(c.run())
//			{
//				return true;
//			}
//		}
//		return false;
//
//		
//	}
//	
//}
//public class  Sequence : ITask
//{
//	List<ITask> children;
//	public Sequence(List<ITask> taskList)
//    {
//        children = taskList;
//    }
//	public bool run()
//	{
//		foreach (ITask c in children)
//		{
//			if(c.run()==false)
//			{
//				return false;
//			}
//		}
//		return true;
//
//		
//	}
//	
//}
//
//public class isHuman: ITask
//{
//	Creature creature;
//	public isHuman(Creature refCreature)
//	{
//		creature = refCreature;
//	}
//	public bool run()
//	{
//		Debug.Log("Checking For Human: " + creature.isHuman);
//		return creature.isHuman;
//	}
//}
//public class isNotHuman : ITask
//{
//	Creature creature;
//	public isNotHuman(Creature refCreature)
//	{
//		creature = refCreature;
//	}
//	public bool run()
//	{
//		Debug.Log("Checking For non-human: " + !creature.isHuman);
//		return !creature.isHuman;
//	}
//}
//
//public class isRobotDogPresent : ITask
//{
//	Creature creature;
//	public isRobotDogPresent(Creature refCreature)
//	{
//		creature = refCreature;
//	}
//	public bool run()
//	{
//		Debug.Log("Checking For Robot Dog: " + creature.isDogPresent);
//		return creature.isDogPresent;
//	}
//}
//
//public class shootAir: ITask
//{
//	Creature creature;
//	public shootAir(Creature refCreature)
//	{
//		creature = refCreature;
//	}
//	public bool run()
//	{
//		Debug.Log("Shooting Air");
//		return creature.isHuman;
//	}
//}
//
//public class shootHuman : ITask
//{
//	Creature creature;
//	public shootHuman(Creature refCreature)
//	{
//		creature = refCreature;
//	}
//	public bool run()
//	{
//		Debug.Log("Shooting Human");
//		return creature.isDogPresent;
//	}
//}
//public class shootCreature: ITask
//{
//	Creature creature;
//	public shootCreature(Creature refCreature)
//	{
//		creature = refCreature;
//	}
//	public bool run()
//	{
//		Debug.Log("Shooting at Creature");
//		return !creature.isHuman;
//	}
//}
//public class petDog: ITask
//{
//	Creature creature;
//	public petDog(Creature refCreature)
//	{
//		creature = refCreature;
//	}
//	public bool run()
//	{
//		Debug.Log("petting Dog");
//		return creature.isDogPresent;
//	}
//}
//
