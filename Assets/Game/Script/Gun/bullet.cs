using UnityEngine;

public class bullet : MonoBehaviour
{
    [SerializeField] private float speedBullet = 1000;

    void Update()
    {
        transform.Translate(Vector3.forward * speedBullet * Time.deltaTime);
    }
}
