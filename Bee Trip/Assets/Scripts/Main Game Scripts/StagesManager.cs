using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StagesManager : MonoBehaviour
{
    public int currentStage;

    private void Start()
    {
        SetCurrentStage();
    }




    private void SetCurrentStage()
    {
        currentStage = PlayerPrefs.GetInt("CurrentStage", 1);
        transform.GetChild(currentStage - 1).gameObject.SetActive(true);
    }

    public int? SetNextStage()
    {
        int nextStage = currentStage + 1;
        if (nextStage <= transform.childCount)
        {
            PlayerPrefs.SetInt("CurrentStage", nextStage);
            return nextStage;
        }
           
        return null;// it means the world has finished/last stage has completed
    }

}
