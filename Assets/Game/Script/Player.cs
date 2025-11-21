using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Player : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI numChip;
    [SerializeField] private TextMeshProUGUI UiGun;
    [SerializeField] private int lifeLostByError = 1;
    [SerializeField] private int lifeWin = 1;
    [SerializeField] private bool godMod;
    [SerializeField] private Canvas looseCanvas;
    [SerializeField] private RoundManager roundManager;
    [SerializeField] private ParticleSystem particleGun;

    public int chipNum = 5;
    private float margin = .05f;
    private int baseChip = 5;
    private int nbOfShoot;
    private int money = 0;


    [SerializeField] private GameObject chip;
    [SerializeField] private GameObject chipContainer;

    private List<GameObject> activeChip = new List<GameObject>();
    private List<GameObject> disactiveChip = new List<GameObject>();

    private void OnEnable()
    {
        ActionManager.onWin += WinLife;
        ActionManager.onLoose += LooseLife;
        ActionManager.numShootToGive += SetNumShot;
        ActionManager.AddMoney += UpdateTotalMoney;
    }

    private void OnDisable()
    {
        ActionManager.onWin -= WinLife;
        ActionManager.onLoose -= LooseLife;
        ActionManager.numShootToGive -= SetNumShot;
        ActionManager.AddMoney -= UpdateTotalMoney;
    }

    private void Start()
    {
        numChip.text = chipNum.ToString();

        AddStartChip();
    }

    private void UpdateTotalMoney(int pNumToGive)
    {
        money += pNumToGive;
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
            GameOver();
        }
    }

    public void GameOver()
    {
        looseCanvas.gameObject.SetActive(true);
        GameManager.CurrentGameState = GameState.InUI;
        roundManager.StopAllCoroutines();
        roundManager.enabled = false;
    }
    public void OnPlayAgain()
    {
        chipNum = baseChip;
        numChip.text = chipNum.ToString();
        looseCanvas.gameObject.SetActive(false);
        GameManager.CurrentGameState = GameState.InRound;
        roundManager.enabled = true;
        AddStartChip();
        ActionManager.onGameOver.Invoke();
    }

    private void WinLife()
    {
        if (godMod)
            return;
        chipNum += lifeWin;
        numChip.text = chipNum.ToString();
        ChallengeManager.Instance.Notify(chipNum, typeof(ChipChallenge));

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
        particleGun.Play();
        nbOfShoot--;
        DisplayBullets();
        ActionManager.playerShoot?.Invoke();
    }
}
