using UnityEngine;
using System.Collections;

public class AttributeManager : MonoBehaviour {

    // Use this for initialization
    private int agility = 0;
    private int brawn = 0;
    private int intellect = 0;
	
	// external class will call updateAgility to change agility stat. 
	public void updateStatsFromGear (GameObject gear) {
        /*if (gear.type == "Agilty")
            updateAgility(gear.getStats());
        elseif (gear.type == "Brawn")
            updateBrawn(gear.getStats());
        else
            updateIntellect(gear.getStats());
        */
	}
    public void updateStatsFromAbility(string trait, int amount)
    {
        if (trait == "Agility")
        {
            updateAgility(amount);
        }
        else if (trait == "Brawn")
        {
            updateBrawn(amount);
        }
        else
            updateIntellect(amount);
    }

    private void updateAgility(int amount)
    {
        agility += amount;
    }

    private void updateBrawn(int amount)
    {
        brawn += amount;
    }

    private void updateIntellect(int amount)
    {
        intellect += amount;
    }

    public int getAgility()
    {
        return agility;
    }

    public int getBrawn()
    {
        return brawn;
    }

    public int getIntellect()
    {
        return intellect;
    }
}
