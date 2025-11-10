using NUnit.Framework.Constraints;
using System.Collections;
using UnityEngine;

public class bullet : MonoBehaviour
{
    [SerializeField] private float speedBullet = 1000;

    private float waitTime = 2.5f;

    private void OnEnable()
    {
        StartCoroutine(WaitBeforeDisactivate());
    }
    void Update()
    {
        transform.Translate(Vector3.forward * speedBullet * Time.deltaTime);
    }

    private IEnumerator WaitBeforeDisactivate()
    {
        yield return new WaitForSeconds(waitTime);
        gameObject.SetActive(false);    
    }
}
