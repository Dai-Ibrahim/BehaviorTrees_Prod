using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum steeringBehaviors
{
     Arrive, None
}

public class Kinematic : MonoBehaviour
{

    public Vector3 linear;
    public float angular; //In degrees
    public GameObject newTarget;
    public float maxSpeed = 10.0f;
    public float maxAngularVelocity = 45.0f; // degrees


    public steeringBehaviors choiceOfBehavior;
    public GameObject[] path;
    public Kinematic[] targets;
	public SteeringOutput steeringUpdate = new SteeringOutput();
	//public GameObject myCohereTarget;

    // Update is called once per frame
    public void Update()
    {
        switch (choiceOfBehavior)
        {
            case steeringBehaviors.None:
                ResetOrientation();
                break;
            default:
                MainSteeringBehaviors();
                break;
        }
    }


    void MainSteeringBehaviors()
    {
        ResetOrientation();

        switch (choiceOfBehavior)
        {
            case steeringBehaviors.Arrive:
                Arrive arrive = new Arrive();
                arrive.character = this;
                arrive.target = newTarget;
                SteeringOutput arriving = arrive.getSteering();
                if (arriving != null)
                {
                    linear += arriving.linear * Time.deltaTime;
                    angular += arriving.angular * Time.deltaTime;
                }
                break;
           
                

        }

    }


    void ResetOrientation()
    {
        //Update position and rotation
        transform.position += linear * Time.deltaTime;
        Vector3 angularIncrement = new Vector3(0, angular * Time.deltaTime, 0);
        transform.eulerAngles += angularIncrement;
    }

}