using UnityEngine.SceneManagement;
using UnityEngine;

public class TransitionManager : MonoBehaviour
{
    //Singleton
     public static TransitionManager Instance { get; private set; }

    private void Awake()
    {
        Instance = this;
    }

    private string _sceneName;
    private GameObject _openPanel,_closePanel;
    [SerializeField] private Animator _anim;


    private bool _isLoadScene;

    public void LoadScene(string sceneName)
    {
        _isLoadScene = true;
        _sceneName = sceneName;
        _anim.SetTrigger("End");
    }

    public void OpenPanel(GameObject openPanel)
    {
        _isLoadScene = false;
        _openPanel = openPanel;     
        _anim.SetTrigger("End");
    }
    public void ClosePanel(GameObject closePanel)
    {
        _closePanel = closePanel;
    }


    public void EndAnim()
    {
        if(_isLoadScene)
             SceneManager.LoadScene(_sceneName);
        else
        {
            _closePanel.SetActive(false);
            _openPanel.SetActive(true);
            _anim.SetTrigger("Start");
        }
    }



}
