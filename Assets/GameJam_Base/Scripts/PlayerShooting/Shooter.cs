using UnityEngine;

public class Shooter : MonoBehaviour
{
    public GameObject projectilePrefab;
    public Transform firePoint;
    public float fireRate = 0.3f;

    float nextFireTime;

    public void Shoot()
    {
        if (Time.time < nextFireTime) return;

        Instantiate(projectilePrefab, firePoint.position, firePoint.rotation);
        nextFireTime = Time.time + fireRate;
    }
}
