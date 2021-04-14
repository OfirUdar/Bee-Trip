using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameController : MonoBehaviour
{
    private List<HiveManager> _listGoalPoints;
    private int countPlayersReachedGoalPoint;


    public int amountDraw;
    public bool isCameraFollowPlayer = false;
    public bool isCameraMoveX;
    public bool isCameraMoveY;


    private bool _isCompleteStage;


    private void Awake()
    {
        _listGoalPoints = Helper.FindChildsByType<HiveManager>(this.transform);
    }

    private void OnEnable()
    {
        SubscribeEvents(true);
    }
    private void OnDisable()
    {
        SubscribeEvents(false);
    }

    private void Start()
    {
        GameUIManager.Instance.UpdateAmountDrawText(amountDraw);
        if(isCameraFollowPlayer)
        {
            CameraFollow camFollow = GameObject.FindObjectOfType<CameraFollow>();
            camFollow.enabled = true;
            camFollow.gameController = this;
            camFollow.moveX = isCameraMoveX;
            camFollow.moveY = isCameraMoveY;
            
        }

        
    }



    //Events

    private void SubscribeEvents (bool sub)
    {
        if(sub)
        {
            foreach (HiveManager h in _listGoalPoints)
                h.goalPoint.OnPlayerReachedGoalPoint += PlayerReachedGoalPoint;
            DrawLine.OnPlayerEndDraw += PlayerEndDraw;
            DrawLine.OnPlayerEndFollow += PlayerEndFollow;
        }
        else
        {
            foreach (HiveManager h in _listGoalPoints)
                h.goalPoint.OnPlayerReachedGoalPoint -= PlayerReachedGoalPoint;
            DrawLine.OnPlayerEndDraw -= PlayerEndDraw;
            DrawLine.OnPlayerEndFollow -= PlayerEndFollow;
        }
       
    }


    private void PlayerReachedGoalPoint()
    {
        countPlayersReachedGoalPoint++;
        if(countPlayersReachedGoalPoint==_listGoalPoints.Count)
        {
            _isCompleteStage = true;
            SaveStats();
            Debug.Log("Stage Completed");
            Invoke("CompleteStage", 1.6f);
        }
        else // check if there is more draws for the second Player and if not- GAME OVER
        {
            if ( amountDraw <= 0)
            {
                if (!PoolManager.Instance.IsEnableLines())
                    Invoke("GameOver", 2.2f);
            }
        }
    }



    private void PlayerEndDraw()
    {
        amountDraw--;
        GameUIManager.Instance.UpdateAmountDrawText(amountDraw);
    }
    private void PlayerEndFollow()
    {
        if(!_isCompleteStage&&amountDraw <= 0)
        {
            if (!PoolManager.Instance.IsEnableLines())
                Invoke("GameOver", 0.4f);
        }
    }



    private void GameOver()
    {
        if(!_isCompleteStage)
             GameUIManager.Instance.GameOver();
    }


    private void CompleteStage()
    {
        GameUIManager.Instance.CompleteStage();
    }

    private void SaveStats()
    {
        StagesManager stageManager = transform.parent.GetComponent<StagesManager>();

        int previousFlowersAmount= PlayerPrefs.GetInt(GameUIManager.Instance.GetCurrentSceneName() + "FlowersStage" + stageManager.currentStage, -1);
        int newFlowersAmount = GameUIManager.Instance.flowersAmount;

        if(newFlowersAmount>previousFlowersAmount)
        {
            PlayerPrefs.SetInt(GameUIManager.Instance.GetCurrentSceneName() + "FlowersStage" + stageManager.currentStage, newFlowersAmount); // == EXAMPLE: key= world1FlowerStage, value=-1,0,1,2,3
            Debug.Log("Amount Flowers of stage " + stageManager.currentStage + "saved = " + newFlowersAmount);



            if (previousFlowersAmount < 0)
                previousFlowersAmount = 0;

            int flowersAmount=PlayerPrefs.GetInt("FlowersAmount", 0);
            flowersAmount += newFlowersAmount - previousFlowersAmount;
            PlayerPrefs.SetInt("FlowersAmount", flowersAmount);
        }

    }
}
