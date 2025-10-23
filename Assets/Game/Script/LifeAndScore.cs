using NUnit.Framework;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.UIElements;

public class LifeAndScore : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI numChip;
    [SerializeField] private int lifeLostByError = 1;
    [SerializeField] private int lifeWin = 1;
    [SerializeField] private bool godMod;
    [SerializeField] private Canvas looseCanvas;
    [SerializeField] private RoundManager roundManager;
    [SerializeField] private int startChip = 5;

    [SerializeField] private GameObject chip;
    [SerializeField] private GameObject chipContainer;

    private List<GameObject> activeChip = new List<GameObject>();
    private List<GameObject> disactiveChip = new List<GameObject>();

    private void OnEnable()
    {
        ActionManager.onWin += WinLife;
        ActionManager.onLoose += LooseLife;
    }

    private void OnDisable()
    {
        ActionManager.onWin -= WinLife;
        ActionManager.onLoose -= LooseLife;
    }

    private void Start()
    {
        numChip.text = startChip.ToString();

        AddStartChip();
    }

    private void AddStartChip()
    {
        for (int i = 0; i < startChip; i++)
        {
            GameObject lChip = Instantiate(chip, GetRandomPosition(), Quaternion.identity, chipContainer.transform);
            activeChip.Add(lChip);
        }
    }

    private Vector3 GetRandomPosition()
    {
        Vector3 lPosStart = chipContainer.transform.position;
        float lRandomX = Random.Range(lPosStart.x - 0.05f, lPosStart.x + 0.05f);
        float lRandomZ = Random.Range(lPosStart.z - 0.05f, lPosStart.z + 0.05f);
        return new Vector3(lRandomX, lPosStart.y, lRandomZ);
    }

    private void LooseLife()
    {
        if (godMod)
            return;

        startChip -= lifeLostByError;
        numChip.text = startChip.ToString();
        GameObject lChip = activeChip[0];
        lChip.SetActive(false);

        disactiveChip.Add(lChip);
        activeChip.Remove(lChip);

        if (startChip <= 0)
        {
            looseCanvas.gameObject.SetActive(true);
            GameManager.instance.CurrentGameState = GameState.InShop;
            roundManager.StopAllCoroutines();
            roundManager.enabled = false;
        }
    }

    public void OnPlayAgain()
    {
        startChip = 5;
        numChip.text = startChip.ToString();
        looseCanvas.gameObject.SetActive(false);
        GameManager.instance.CurrentGameState = GameState.InRound;
        roundManager.enabled = true;
    }

    private void WinLife()
    {
        if (godMod)
            return;
        startChip += lifeWin;
        numChip.text = startChip.ToString();

        GameObject lChip;
        if (disactiveChip.Count == 0)
        {
            lChip = Instantiate(chip, GetRandomPosition(), Quaternion.identity, chipContainer.transform);
            activeChip.Add(lChip);
            return;
        }
        lChip = disactiveChip[0];
        lChip.transform.position = GetRandomPosition();
        disactiveChip.Remove(lChip);
        activeChip.Add(lChip);
        lChip.SetActive(true);



    }
}
