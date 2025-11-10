using System.Collections;
using TMPro;
using UnityEngine;
using UnityEngine.Rendering.UI;

public class GunPlayer : MonoBehaviour
{
    private int numMaxShootSequence;

    [SerializeField] private TextMeshProUGUI UiGun;

    public int NumMaxShootSequence
    {
        get
        {
            return numMaxShootSequence;
        }
        set
        {
            numMaxShootSequence = value;
            UiGun.text = value.ToString();
            hasShootNum = 0;
        }
    }

    private int hasShootNum;

    private void OnEnable()
    {
        ActionManager.playerShoot += Shoot;
        
    }

    private void OnDisable()
    {
        ActionManager.playerShoot -= Shoot;
    }
    protected void Shoot()
    {
        hasShootNum++;
        int lBulletLeft = (numMaxShootSequence - hasShootNum);
        if(lBulletLeft >= 0) 
            UiGun.text = lBulletLeft.ToString();
    }
}
