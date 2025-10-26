using System.Collections;
using DG.Tweening;
using UnityEngine;

public class GunEnemy : Gun
{
    private Vector3 origineRotation;
    [SerializeField] private float howLongToRotate = .5f;
    [SerializeField] private float waitBeforeGoingDown = .2f;

    private void OnEnable()
    {
        ActionManager.enemyShoot += DoRotationBeforeShoot;
    }

    private void OnDisable()
    {
        ActionManager.enemyShoot -= DoRotationBeforeShoot;
    }
    private void Start()
    {
        origineRotation = transform.rotation.eulerAngles;
    }

    private void DoRotationBeforeShoot(float pHowLongToRotate)
    {
        howLongToRotate = pHowLongToRotate;
        transform
            .DORotate(new Vector3(0f, origineRotation.y, origineRotation.z), howLongToRotate)
            .OnComplete(() => StartShooting());
    }

    private void StartShooting()
    {
        Shoot();
        StartCoroutine(WaitBeforeDown());
    }
    private IEnumerator WaitBeforeDown()
    {
        yield return new WaitForSeconds(waitBeforeGoingDown);
        transform.DORotate(
            new Vector3(origineRotation.x, origineRotation.y, origineRotation.z),
            howLongToRotate
        );
    }
}
