using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class KnifeUIController : MonoBehaviour
{
    private TextMeshProUGUI knifeText;
    public PlayerController playerController;
    public int knifeCount;

    private void Awake()
    {
        knifeText = GetComponent<TextMeshProUGUI>();
    }

    private void Start()
    {
        RefreshUI();
    }

    public void DecreaseKnifeCount(int decreament)
    {
        knifeCount -= decreament;
        RefreshUI();
    }

    private void RefreshUI()
    {
        knifeText.text = "" + knifeCount;
    }
}
