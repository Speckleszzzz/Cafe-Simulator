using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class levelupstats : MonoBehaviour
{
    public int Level = 1;
    public float experience { get; private set; }
    public TMP_Text LvlText;
    public Image expBarImage;

    public static int ExpNeedToLevelUp(int currentLevel) 
    {
        if (currentLevel == 0) 
            return 0;

        return (currentLevel * currentLevel + currentLevel) * 5;
    }

    public void SetExperience(float exp)
    {
        experience += exp;

        float expNeeded = ExpNeedToLevelUp(Level);
        float previousExperience = ExpNeedToLevelUp(Level - 1);

        Debug.Log("Experience: " + experience);
        Debug.Log("ExpNeeded: " + expNeeded);
        Debug.Log("PreviousExperience: " + previousExperience);

        if(experience >= expNeeded)
        {
            levelUp();
            expNeeded = ExpNeedToLevelUp(Level);
            previousExperience = ExpNeedToLevelUp(Level - 1);
        }

        if (expNeeded == 0)
        {
            expBarImage.fillAmount = 1;
        }
        else
        {
            float fillAmount = (experience - previousExperience) / (expNeeded - previousExperience);
            Debug.Log("Fill Amount: " + fillAmount);
            expBarImage.fillAmount = fillAmount;
        }
    }

    public void levelUp()
    {   
        Level++;
        Debug.Log("Level increased to: " + Level);
        LvlText.text = Level.ToString(); // Corrected to update the text with the new level
    }
}
