using NUnit.Framework;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.UIElements;

public class Player : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI numChip;
    [SerializeField] private TextMeshProUGUI UiGun;
    [SerializeField] private int lifeLostByError = 1;
    [SerializeField] private int lifeWin = 1;
    [SerializeField] private bool godMod;
    [SerializeField] private Canvas looseCanvas;
    [SerializeField] private RoundManager roundManager;

    public int chipNum = 5;
    private float margin = .05f;
    private int baseChip = 5;
    private int nbOfShoot;

    [SerializeField] private GameObject chip;
    [SerializeField] private GameObject chipContainer;

    private List<GameObject> activeChip = new List<GameObject>();
    private List<GameObject> disactiveChip = new List<GameObject>();

    private void OnEnable()
    {
        ActionManager.onWin += WinLife;
        ActionManager.onLoose += LooseLife;
        ActionManager.numShootToGive += SetNumShot;
    }

    private void OnDisable()
    {
        ActionManager.onWin -= WinLife;
        ActionManager.onLoose -= LooseLife;
        ActionManager.numShootToGive -= SetNumShot;
    }

    private void Start()
    {
        numChip.text = chipNum.ToString();

        AddStartChip();
    }

    private void AddStartChip()
    {
        for (int i = 0; i < chipNum; i++)
        {
            GameObject lChip = Instantiate(chip, GetRandomPosition(), Quaternion.identity, chipContainer.transform);
            activeChip.Add(lChip);
        }
    }

    private Vector3 GetRandomPosition()
    {
        Vector3 lPosStart = chipContainer.transform.position;
        float lRandomX = Random.Range(lPosStart.x - margin, lPosStart.x + margin);
        float lRandomZ = Random.Range(lPosStart.z - margin, lPosStart.z + margin);
        return new Vector3(lRandomX, lPosStart.y, lRandomZ);
    }

    private void LooseLife()
    {
        if (godMod)
            return;

        chipNum -= lifeLostByError;
        numChip.text = chipNum.ToString();
        GameObject lChip = activeChip[0];
        lChip.SetActive(false);

        disactiveChip.Add(lChip);
        activeChip.Remove(lChip);

        if (chipNum <= 0)
        {
            looseCanvas.gameObject.SetActive(true);
            GameManager.instance.CurrentGameState = GameState.InShop;
            roundManager.StopAllCoroutines();
            roundManager.enabled = false;
        }
    }

    public void OnPlayAgain()
    {
        ActionManager.endOfRound.Invoke();
        chipNum = baseChip;
        numChip.text = chipNum.ToString();
        looseCanvas.gameObject.SetActive(false);
        GameManager.instance.CurrentGameState = GameState.InRound;
        roundManager.enabled = true;
        AddStartChip();
    }

    private void WinLife()
    {
        if (godMod)
            return;
        chipNum += lifeWin;
        numChip.text = chipNum.ToString();

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

    private void SetNumShot(int pNum)
    {
        nbOfShoot = pNum;
        DisplayBullets();
    }

    private void DisplayBullets() => UiGun.text = nbOfShoot.ToString();
    
    public void TryShoot()
    {
        if (nbOfShoot == 0) return;

        nbOfShoot--;
        DisplayBullets();
        ActionManager.playerShoot?.Invoke();
    }

}
