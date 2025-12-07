using DG.Tweening;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class Player : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI numChip;
    [SerializeField] private TextMeshProUGUI uiGun;
    [SerializeField] private int lifeLostByError = 1;
    [SerializeField] private int lifeWin = 1;
    [SerializeField] private bool godMod;
    [SerializeField] private ParticleSystem particleGun;
    [SerializeField] private GameObject chip;
    [SerializeField] private GameObject chipContainer;
    [SerializeField] private Camera mainCamera;
    [SerializeField] private AudioClip gunSound;
    [SerializeField] private Image damageOverlay;  

    private float margin = .05f;
    private int baseChip = 5;
    private int nbOfShoot;
    public int money = 0;
    public int chipNum = 5;

    private List<GameObject> activeChip = new List<GameObject>();
    private List<GameObject> disactiveChip = new List<GameObject>();

    private void OnEnable()
    {
        ActionManager.onWin += WinLife;
        ActionManager.onLoose += LooseLife;
        ActionManager.onLoose += TakeDamageFeedback;
        ActionManager.numShootToGive += SetNumShot;
        ActionManager.updateMoneyLoss += UpdateChipWin;
        ActionManager.playerShoot += ShootFeedback;
        ActionManager.returnToHub += ResetPlayer;
    }

    private void OnDisable()
    {
        ActionManager.onWin -= WinLife;
        ActionManager.onLoose -= LooseLife;
        ActionManager.onLoose -= TakeDamageFeedback;
        ActionManager.numShootToGive -= SetNumShot;
        ActionManager.updateMoneyLoss -= UpdateChipWin;
        ActionManager.playerShoot -= ShootFeedback;
        ActionManager.returnToHub -= ResetPlayer;
    }

    private void Start()
    {
        numChip.text = chipNum.ToString();

        AddStartChip();
    }

    public void CashOut()
    {
        money += chipNum;
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

    private void TakeDamageFeedback()
    {
        damageOverlay.DOFade(1f, 0.3f).OnComplete(() =>
        {
            damageOverlay.DOFade(0f, 0.3f);
        });
    }

    private void LooseLife()
    {
        if (godMod) return;
       
        chipNum -= lifeLostByError;
        numChip.text = chipNum.ToString();
        GameObject lChip = activeChip[0];
        lChip.SetActive(false);

        disactiveChip.Add(lChip);
        activeChip.Remove(lChip);

        if (chipNum <= 0)
            ActionManager.gameOver.Invoke();
    }

    private void UpdateChipWin()
    {
        lifeLostByError += 1;
    }

    private void ResetPlayer()
    {
        chipNum = baseChip;
        numChip.text = chipNum.ToString();
        AddStartChip();
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
    
    

    private void DisplayBullets() => uiGun.text = nbOfShoot.ToString();
    
    public void TryShoot()
    {
        if (nbOfShoot == 0 && !godMod) return;
        particleGun.Play();
        nbOfShoot--;
        DisplayBullets();
        ActionManager.playerShoot?.Invoke();
    }

    private void ShootFeedback()
    {
        ActionManager.playSound(gunSound);
        Vector3 lCurrentPos = transform.position;

        transform.DOShakePosition(0.25f, .05f ,30, 90).OnComplete(() =>
        {
            transform.position = lCurrentPos;
        }); 
    }
}
