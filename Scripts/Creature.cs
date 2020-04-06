using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Creature : MonoBehaviour
{
    public bool isHuman;
    public bool isDogPresent;
	public Toggle humanToggle;
    public Toggle dogToggle;


	public void ToggleHuman()
    {
        isHuman = !isHuman;
    }

    public void ToggleDog()
    {
        isDogPresent = !isDogPresent;
    }

}