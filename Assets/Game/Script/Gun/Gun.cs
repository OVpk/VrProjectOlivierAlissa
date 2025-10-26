using UnityEngine;

public class Gun : MonoBehaviour
{
    [SerializeField] private GameObject bulletPrefab;
    [SerializeField] private Transform anchorLaunchBullet;
    [SerializeField] private bool isPlayer;
    private GameObject bullet;

    private void Start()
    {
        Shoot();
    }
    private void Shoot()
    {
        if(bullet != null)
        {
            bullet.transform.position = anchorLaunchBullet.transform.position;
            bullet.SetActive(true);
        }
        else
        {
            bullet = Instantiate(bulletPrefab, anchorLaunchBullet.position, Quaternion.identity);
        }

        bullet.transform.rotation = gameObject.transform.rotation;
    }
}
