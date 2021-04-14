using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PanelReward : MonoBehaviour
{

    [SerializeField] private Animator _anim;


    public StagesManager stageManager;

    private string _reward;


    private void OnEnable()
    {
        AdsManager.Instance.OnCompleteWatchRewardedVideo += OnCompleteWatchRewardVideo;
    }

    private void OnDisable()
    {
        AdsManager.Instance.OnCompleteWatchRewardedVideo -= OnCompleteWatchRewardVideo;
    }


    public void ClosePanel()
    {
        _anim.SetTrigger("Close");
    }
    public void CloseAnimationFinished()
    {
        GameUIManager.Instance.OnCloseOtherPanel();
        this.gameObject.SetActive(false);
    }

    public void WatchVideo(string reward)
    {
        this._reward = reward;
        Debug.Log("Watching video");
        AdsManager.Instance.ShowRewardVideo();
    }

    private void OnCompleteWatchRewardVideo()
    {
        if (_reward.Equals("3Flowers"))
        {
            // save the stage as complete 3 flowers
            GameUIManager.Instance.flowersAmount = 3;
            SaveStats();
        }
        else
        {
            if (_reward.Equals("Draw"))
            {
                //add one more draw
                GameController GC= stageManager.transform.GetChild(stageManager.currentStage - 1).GetComponent<GameController>();
                GC.amountDraw = 1;
                GameUIManager.Instance.UpdateAmountDrawText(1);
            }
        }
        GameUIManager.Instance.isWatchedVideo = true;
        ClosePanel();

    }

    private void SaveStats()
    {
        int previousFlowersAmount = PlayerPrefs.GetInt(GameUIManager.Instance.GetCurrentSceneName() + "FlowersStage" + stageManager.currentStage, -1);
        int newFlowersAmount = GameUIManager.Instance.flowersAmount;

        if (newFlowersAmount > previousFlowersAmount)
        {
            PlayerPrefs.SetInt(GameUIManager.Instance.GetCurrentSceneName() + "FlowersStage" + stageManager.currentStage, newFlowersAmount); // == EXAMPLE: key= world1FlowerStage, value=-1,0,1,2,3
            Debug.Log("Amount Flowers of stage " + stageManager.currentStage + "saved = " + newFlowersAmount);


            if (previousFlowersAmount < 0)
                previousFlowersAmount = 0;

            int flowersAmount = PlayerPrefs.GetInt("FlowersAmount", 0);
            flowersAmount += newFlowersAmount - previousFlowersAmount;
            PlayerPrefs.SetInt("FlowersAmount", flowersAmount);
        }
    }
}
