using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
public class World : MonoBehaviour
{
    public GameObject lockImage;
    public Button btn;
    public GameObject priceOB;
    public TextMeshProUGUI priceText;
    [Space]
    public int price;

    private void Start()
    {
        priceText.text = price.ToString();
    }

}
