using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shooting : MonoBehaviour
{
    public Joystick joystick;
    public GameObject bulletPrefab;
    public float shootForce;
    public float shootDelay;
    public Transform spawn;
    public Walk player;
    private float lastShootTime;

    private void Update()
    {
        lastShootTime -= Time.deltaTime;
    }
    public void ShootBullet()
    {
        if (lastShootTime <= 0f)
        {
            player.StartShootingAnimation();
            Vector3 forwardDirection = transform.TransformDirection(Vector3.forward);

            // Create a new bullet
            GameObject bullet = Instantiate(bulletPrefab, spawn.position, Quaternion.identity);

            // Get the bullet's rigidbody component
            Rigidbody bulletRb = bullet.GetComponent<Rigidbody>();

            // Disable gravity and adjust other physics settings
            bulletRb.drag = 0f;
            bulletRb.angularDrag = 0f;

            // Apply constant velocity to the bullet in the forward direction
            bulletRb.velocity = -forwardDirection.normalized * shootForce;
            lastShootTime = shootDelay;

        }
    }
}
