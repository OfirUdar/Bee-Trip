using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PoolManager : MonoBehaviour
{
    public static PoolManager Instance { get; private set; }

    private void Awake()
    {
        Instance = this;
    }


    #region Lines
    [Header("Lines")]

    [SerializeField] private GameObject _pfbLine;
    [SerializeField] private GameObject _LinesContainer;
    private List<GameObject> _linesList = new List<GameObject>();



    public GameObject RequestLine()
    {
        foreach (var line in _linesList)
        {
            if (!line.activeSelf)
            {
                line.SetActive(true);
                return line;
            }
        }

        GameObject lineOB = Instantiate(_pfbLine, _LinesContainer.transform);
        _linesList.Add(lineOB);
        return lineOB;
    }

    public bool IsEnableLines()
    {
        foreach (var line in _linesList)
            if (line.activeSelf)
                return true;
        return false;
    }
    #endregion



}
