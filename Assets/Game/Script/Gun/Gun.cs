using UnityEngine;

public class Gun : MonoBehaviour
{
    [SerializeField] private GameObject bulletPrefab;
    [SerializeField] private Transform anchorLaunchBullet;
    [SerializeField] protected bool isPlayer;
    private GameObject bullet;

    protected virtual void Shoot()
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
