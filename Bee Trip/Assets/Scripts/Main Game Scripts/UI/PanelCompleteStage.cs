using TMPro;
using UnityEngine;

public class PanelCompleteStage : MonoBehaviour
{
    [SerializeField] private StagesManager _stagesManager;

    [Space]
    [SerializeField] private GameObject[] _flowers;
    [SerializeField] private TextMeshProUGUI titleText;


    private void OnEnable()
    {
        Setup();
    }

    private void Setup()
    {
        SetFlowersCollected();
        SetStageText();
        SoundsManager.Instance.PlayStageComplete();
        Blink.stopBlink = true;
    }

    private void SetFlowersCollected()
    {
        for (int i = 0; i < GameUIManager.Instance.flowersAmount; i++)
            _flowers[i].SetActive(true);
    }
    private void SetStageText()
    {
        titleText.text = "Stage "+_stagesManager.currentStage +" Complete!";
    }




    //Buttons

    public void RestartStage()
    {
        GameUIManager.Instance.RestartScene();
    }

    public void NextStage()
    {
        GameUIManager.Instance.NextStage();
    }

    public void LoadStages()
    {
        GameUIManager.Instance.LoadStages();
    }

}
