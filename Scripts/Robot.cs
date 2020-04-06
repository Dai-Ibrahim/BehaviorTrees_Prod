using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Robot :  MonoBehaviour
{
    public Creature creature;
	bool executingBehavior = false;
    Task myCurrentTask;


    // Update is called once per frame
    public void Update()
    {
		if (Input.GetKeyDown(KeyCode.Return))
        {
              if (!executingBehavior)
            {
                executingBehavior = true;
                myCurrentTask = ConfigureBehavior();

                EventBus.StartListening(myCurrentTask.TaskFinished, OnTaskFinished);
                myCurrentTask.run();
            }

        }

    }

    void OnTaskFinished()
    {
        EventBus.StopListening(myCurrentTask.TaskFinished, OnTaskFinished);
        executingBehavior = false;
    }


    Task ConfigureBehavior()
    {
		List<Task> taskList = new List<Task>();

		Task Human = new IsTrue(creature.isHuman);
		Task Dog = new IsTrue(creature.isDogPresent);
		Task NotHuman = new IsFalse(creature.isHuman);
		Task NoDog = new IsFalse(creature.isDogPresent);
		Task saveHuman = new saveHuman(creature);
		Task walkAround = new walkAround(creature);
		Task ShootAir = new shootAir(creature);
		Task ShootHuman = new shootHuman(creature);
		Task ShootCreature = new shootCreature(creature);
		Task petDog = new petDog(creature);
		Task takeDog = new takeDog(creature);

		
		taskList.Add(Human);
		taskList.Add(NoDog);
		taskList.Add(walkAround);
		taskList.Add(ShootAir);

		Sequence SpareTheHuman = new Sequence(taskList);
		taskList = new List<Task>();

		taskList.Add(NotHuman);
		taskList.Add(NoDog);
		taskList.Add(saveHuman);
		taskList.Add(ShootCreature);

		Sequence SaveTheHuman = new Sequence(taskList);
		taskList = new List<Task>();
		
		taskList.Add(Dog);
		taskList.Add(Human);
		taskList.Add(ShootHuman);
		taskList.Add(petDog);
		taskList.Add(takeDog);
		
		Sequence SaveTheDog = new Sequence(taskList);
		taskList = new List<Task>();
		
		taskList.Add(Dog);
		taskList.Add(NotHuman);
		taskList.Add(ShootCreature);
		taskList.Add(petDog);
		taskList.Add(takeDog);
		
		Sequence SaveTheDogFromMonster = new Sequence(taskList);
		taskList = new List<Task>();
		
		taskList.Add(SaveTheDog);
		taskList.Add(SpareTheHuman);
		taskList.Add(SaveTheHuman);
		taskList.Add(SaveTheDogFromMonster);
		

        Selector selector = new Selector(taskList);
        return selector;
      
    }
}
//public class Robot : Kinematic
//{
//    public Arrive arrive;
//    public Creature creature;
//	bool executingBehavior = false;
//    Task myCurrentTask;
//
//    void Start()
//    {
//        arrive = new Arrive();
//        arrive.character = this;
//        arrive.target = newTarget;
//
// 
//      
//    }
//
//    // Update is called once per frame
//    public void Update()
//    {
//		if (Input.GetKeyDown(KeyCode.Return))
//        {
//              if (!executingBehavior)
//            {
//                executingBehavior = true;
//                myCurrentTask = ConfigureBehavior();
//
//                EventBus.StartListening(myCurrentTask.TaskFinished, OnTaskFinished);
//                myCurrentTask.run();
//            }
//
//        }
//
//    }
//
//    void OnTaskFinished()
//    {
//        EventBus.StopListening(myCurrentTask.TaskFinished, OnTaskFinished);
//        //Debug.Log("Behavior complete! Success = " + myCurrentTask.succeeded);
//        executingBehavior = false;
//    }
//
//
//    public Task ConfigureBehavior()
//    {
//		List<Task> taskList = new List<Task>();
//
//		Task Human = new isHuman(creature);
//		Task HumanAndDog = new isHuman(creature);
//		Task NotHuman = new isNotHuman(creature);
//		Task CheckIfDogIsPresent = new isRobotDogPresent(creature);
//		Task ShootAir = new shootAir(creature);
//		Task ShootHuman = new shootHuman(creature);
//		Task ShootCreature = new shootCreature(creature);
//		Task petDog = new petDog(creature);
//		
//		//not a human, shoot the creature
//		taskList.Add(Human);
//		taskList.Add(ShootAir);
//
//		Sequence SpareTheHuman = new Sequence(taskList);
//		taskList = new List<Task>();
//
//		taskList.Add(NotHuman);
//		taskList.Add(ShootCreature);
//
//		Sequence SaveTheHuman = new Sequence(taskList);
//		taskList = new List<Task>();
//		
//		taskList.Add(CheckIfDogIsPresent);
//		taskList.Add(Human);
//		taskList.Add(ShootHuman);
//		taskList.Add(petDog);
//		
//		
//		Sequence SaveTheDog = new Sequence(taskList);
//		taskList = new List<Task>();
//		
//		taskList.Add(SaveTheDog);
//		taskList.Add(SpareTheHuman);
//		taskList.Add(SaveTheHuman);
//		
//
//        Selector selector = new Selector(taskList);
//        return selector;
//      
//    }
//}