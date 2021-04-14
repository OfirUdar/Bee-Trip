using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
public class CreatePlayer : MonoBehaviour
{
    [SerializeField] private GameObject _pfbPlayerBee;
    [SerializeField] private GameObject _pfbPlayerMosquito;
    [SerializeField] private GameObject _pfbPlayerBeetle;
    [SerializeField] private GameObject _pfbPlayerButterfly;
    [SerializeField] private GameObject _pfbPlayerAnt;
    [SerializeField] private GameObject _pfbPlayerWasp;
    [SerializeField] private GameObject _pfbPlayerDragonfly;
    [SerializeField] private GameObject _pfbPlayerBat;
    [SerializeField] private GameObject _pfbPlayerParrot;
    [SerializeField] private GameObject _pfbPlayerMantis;
    [SerializeField] private GameObject _pfbPlayerInsect1;
    [SerializeField] private GameObject _pfbPlayerInsect2;
    [SerializeField] private GameObject _pfbPlayerInsect3;
    [SerializeField] private GameObject _pfbPlayerInsect4;
    [SerializeField] private GameObject _pfbPlayerInsect5;  
    [SerializeField] private GameObject _pfbPlayerDinosaur;


    [Space]
    [SerializeField] private HiveManager _hive;

    public bool setCircle;
    public GameObject pfbCircle;
    public Color circleColor=Color.white;

    [Space]
    public string playerName;


    public delegate void DestroyCreatePlayer(GameObject player);
    public event DestroyCreatePlayer OnDestroyCreatePlayer;
    
    private void Awake()
    {
        if (playerName == "")
            playerName = PlayerPrefs.GetString("CurrentPlayer", "Bee");
        GameObject playerOB = GetCurrentPlayer(playerName);
        playerOB.GetComponent<PlayerController>().goalPoint = _hive.goalPoint;
       GameObject player= Instantiate(playerOB, transform.position, Quaternion.identity,transform.parent);
        _hive.goalPoint.player = player;

        if (setCircle)
        {
            SetCircle(player,new Vector3(0.75f,0.75f,1)); //set circle for player
            SetCircle(_hive.gameObject, new Vector3(1.3f, 1.3f, 1));// set circle for hive
        }
           

        Destroy(this.gameObject);
    }

    private GameObject GetCurrentPlayer(string currentPlayer)
    {
        switch(currentPlayer)
        {
            case "Bee":
                {
                    return _pfbPlayerBee;
                }
            case "Mosquito":
                {
                    return _pfbPlayerMosquito;
                }

            case "Beetle":
                {
                    return _pfbPlayerBeetle;
                }
            case "Butterfly":
                {
                    return _pfbPlayerButterfly;
                }
            case "Ant":
                {
                    return _pfbPlayerAnt;
                }
            case "Wasp":
                {
                    return _pfbPlayerWasp;
                }
            case "Dragonfly":
                {
                    return _pfbPlayerDragonfly;
                }
            case "Bat":
                {
                    return _pfbPlayerBat;
                } 
            case "Parrot":
                {
                    return _pfbPlayerParrot;
                }
            case "Mantis":
                {
                    return _pfbPlayerMantis;
                }
            case "Insect1":
                {
                    return _pfbPlayerInsect1;
                }
            case "Insect2":
                {
                    return _pfbPlayerInsect2;
                }
            case "Insect3":
                {
                    return _pfbPlayerInsect3;
                }
            case "Insect4":
                {
                    return _pfbPlayerInsect4;
                }
            case "Insect5":
                {
                    return _pfbPlayerInsect5;
                }
            case "Dinosaur":
                {
                    return _pfbPlayerDinosaur;
                }
        }
        return _pfbPlayerBee;
    }


    private void SetCircle(GameObject parent,Vector3 scale)
    {
        GameObject circle = Instantiate(pfbCircle, parent.transform);
        circle.transform.GetComponent<SpriteRenderer>().color = circleColor;
        circle.transform.localScale = scale;
    }

    private void OnDestroy()
    {
        if (OnDestroyCreatePlayer != null)
            OnDestroyCreatePlayer(_hive.goalPoint.player);
    }
}
