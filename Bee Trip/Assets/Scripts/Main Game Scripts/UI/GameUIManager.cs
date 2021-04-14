
using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;
using UnityEngine.UI;

public class GameUIManager : MonoBehaviour
{
    public static GameUIManager Instance { get; private set; }


    [SerializeField] private StagesManager _stagesManager;






    [Space]
    [Header("Panels")]

    [SerializeField] private GameObject _panelCompleteStage;
    [SerializeField] private GameObject _panelPauseStage;
    [SerializeField] private GameObject _panelGet3Flowers;
    [SerializeField] private GameObject _panelGetMoreDraw;
    [SerializeField] private GameObject _panelRateUs;


    [Space]

    [Header("UI Properties")]
    [SerializeField] private GameObject[] _flowers;
    [SerializeField] private TextMeshProUGUI _amountDrawText;
    [SerializeField] private Animator _amountDrawAnim;
    public Slider drawBar;

    public int flowersAmount=0;


    private bool _isGameOver;
    public bool isWatchedVideo=false;


    private bool _isRatedUs=false;

    private void Awake()
    {
        Instance = this;
        _isRatedUs = PlayerPrefs.GetInt("IsRateUs", 0)==1;
    }


    public void UpdateAmountDrawText(int amount)
    {
        if (amount < 1)
            _amountDrawText.color = Color.red;
        _amountDrawText.text = amount.ToString();
        _amountDrawAnim.Rebind();
    }


    public void FlowerCollected()
    {
        this.flowersAmount++;
        if (flowersAmount <= _flowers.Length)
            _flowers[flowersAmount - 1].SetActive(true);
        else
            Debug.LogError("There is " + _flowers.Length + " on the CANVAS and flowers collected is " + this.flowersAmount); ;
    }


    public void InvokeMethod(string method,float timeDelay)
    {
        Invoke(method, timeDelay);
    }


    //Panels

    public void OpenPanelPauseStage()
    {
       _panelPauseStage.SetActive(true);
    }

    public void CompleteStage()
    {
        AdsManager.Instance.ShowInterstitial_ByChance(8);
        _isGameOver = false;
        if(!isWatchedVideo)
        {
            if (Random.Range(0, 8) == 5)
            {
                if (flowersAmount < 3)
                {
                    _panelGet3Flowers.SetActive(true);
                    return;
                }
            }
        }
        
        if (!_isRatedUs && _stagesManager.currentStage>=6&&Random.Range(0, 20) == 5)
        {
            _panelRateUs.SetActive(true);
            return;
        }
        _panelCompleteStage.SetActive(true);
    }

    public void GameOver()
    {
        AdsManager.Instance.ShowInterstitial_ByChance(4);
        _isGameOver =true;
        if (!isWatchedVideo && Random.Range(0, 10) == 5)
        {
            _panelGetMoreDraw.SetActive(true);
            return;
        }
         RestartScene();
    }

    public void OnCloseOtherPanel()
    {
        if(_isGameOver)
        {
            if (_amountDrawText.text.Equals("1"))
                _isGameOver = false;
            else
                RestartScene();
        }
            
        else
            _panelCompleteStage.SetActive(true);

    }




    public string GetCurrentSceneName()
    {
        return SceneManager.GetActiveScene().name;
    }



    //buttons 



    public void RestartScene()
    { 
         TransitionManager.Instance.LoadScene(SceneManager.GetActiveScene().name);
    }

    public void LoadStages()
    {
        TransitionManager.Instance.LoadScene("Worlds");
        Debug.Log("load Worlds");
    }

    public void NextStage()
    {
        if (_stagesManager.SetNextStage() == null)// it means the world has finished/last stage has completed
        {
            PlayerPrefs.SetInt("CurrentWorld", PlayerPrefs.GetInt("CurrentWorld", 1) +1); //setting the current world on the scroll rect in Worlds scene
            LoadStages();
        }
        else
            RestartScene();

    }
}
