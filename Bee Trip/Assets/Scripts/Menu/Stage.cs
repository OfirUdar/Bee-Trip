using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;


public class Stage :MonoBehaviour
{
    public Button btn;
    public TextMeshProUGUI stageNumberText;
    public GameObject[] flowers;

    [Space]
    public int stageNumber;

    private void Awake()
    {
        stageNumber = transform.GetSiblingIndex() + 1;
       
    }
    private void OnEnable()
    {
        Setup();
    }



    private void Setup()
    {
       
        SetStageNumberText();

    }



    public void SetStageNumberText()
    {
        stageNumberText.text = stageNumber.ToString();
    }

    public void DisplayFlowers(int flowersAmount) // Setting active the amount of the flowers that achived by the player in this stage
    {
        for (int i = 0; i < flowersAmount; i++)
            flowers[i].SetActive(true);
    }
}
