using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
public class PanelStages : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI _panelWorldNameText;
    [SerializeField] private TextMeshProUGUI _panelStagesNameText;
    [SerializeField] private Stage[] _stages;


    private string _currentWorld;


    private void Awake()
    {
        _currentWorld = "World" + transform.GetSiblingIndex();
        PlayerPrefs.SetInt("CurrentWorld", transform.GetSiblingIndex());
        _panelStagesNameText.text = _panelWorldNameText.text;
    }
    private void OnEnable()
    {
        Setup();
    }

   


    private void Setup()
    {
        SetupStages();
    }

    private void SetupStages()
    {
        int flowersAmount;
        int i;
        for ( i = 0; i < _stages.Length - 1; i++)
        {
             flowersAmount = PlayerPrefs.GetInt(_currentWorld + "FlowersStage" + _stages[i].stageNumber, -1);
            if (flowersAmount == -1)
            {
                return;
            }
            else
            {
                _stages[i + 1].btn.interactable = true;
                _stages[i].DisplayFlowers(flowersAmount);
            }
        }

        flowersAmount = PlayerPrefs.GetInt(_currentWorld + "FlowersStage" + _stages[i].stageNumber, -1);
       if(flowersAmount!=-1)
            _stages[i].DisplayFlowers(flowersAmount);
    }

    public void ClosePanelStages(GameObject openPanel)
    {
        AdsManager.Instance.ShowInterstitial_ByChance(10);
        TransitionManager.Instance.OpenPanel(openPanel);
        TransitionManager.Instance.ClosePanel(this.gameObject);
    }


    public void LoadStage(Stage stage)
    {
        AdsManager.Instance.HideBanner();
        LoadScene(stage);
    }

    private void LoadScene(Stage stage)
    {
        PlayerPrefs.SetInt("CurrentStage", stage.stageNumber);
        TransitionManager.Instance.LoadScene(_currentWorld);
    }
}
