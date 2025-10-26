using System.Collections;
using UnityEngine;

public class bullet : MonoBehaviour
{
    [SerializeField] private float speedBullet = 1000;

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
        yield return new WaitForSeconds(2.5f);
        gameObject.SetActive(false);    
    }
}
