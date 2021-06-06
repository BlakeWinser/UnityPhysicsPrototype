using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GunSystem : MonoBehaviour
{
    public UI_AmmoCounter uiAmmoCounter;

    public float damage;
    public float pauseBetweenShooting, spread, range, reloadTime, pauseBetweenShots;
    public float magSize, bulletsPerTap;
    public bool triggerHeld;

    float bulletsLeft, bulletsShot;

    bool shooting, readyToShoot, reloading;

    public Camera cam;
    public RaycastHit hit;

    private void Start()
    {
        bulletsLeft = magSize;
        readyToShoot = true;
    }

    private void Update()
    {
        MyInput();

        uiAmmoCounter.setCurrentAmmo(bulletsLeft + "/" + magSize);
    }

    private void MyInput()
    {
        if (triggerHeld) 
        { 
            shooting = Input.GetKey(KeyCode.Mouse0); 
        }
        else
        {
            shooting = Input.GetKeyDown(KeyCode.Mouse0);
        }

        if (Input.GetKeyDown(KeyCode.R) && bulletsLeft < magSize && !reloading)
        {
            Reload();
        }

        if (readyToShoot && shooting && !reloading && bulletsLeft > 0)
        {
            bulletsShot = bulletsPerTap; 
            Shoot();
        }
    }

    private void Shoot()
    {
        readyToShoot = false;

        float x = Random.Range(-spread, spread);
        float y = Random.Range(-spread, spread);

        Vector3 direction = cam.transform.forward + new Vector3(x, y, 0);

        if (Physics.Raycast(cam.transform.position, direction, out hit, range))
        {
            Debug.Log(hit.collider.name);
            Debug.DrawRay(cam.transform.position, cam.transform.forward * 100f, Color.red, 20f);
            
            if (hit.collider.CompareTag("Enemy"))
            {
                //Deal damage to enemy;
                hit.collider.GetComponent<PlayerHealth>().TakeDamage(damage);
            }

            if (hit.collider.CompareTag("Test"))
            {
                //break cubes on hit;
                hit.collider.GetComponent<DestructibleObjects>().DestroyCube();
            }

            //Apply explosion force on raycast hit position
            Collider[] colliders = Physics.OverlapSphere(hit.transform.position, 1f);

            foreach (Collider near in colliders)
            {
                Rigidbody rb = near.GetComponent<Rigidbody>();
                if (rb != null)
                {
                    rb.AddForce(cam.transform.forward * 1000, ForceMode.Force);                 
                }
            }
        }

        bulletsLeft--;
        bulletsShot--;

        //Do Muzzle flash graphics here.

        Invoke("ResetShot", pauseBetweenShooting);

        if(bulletsShot > 0 && bulletsLeft > 0)
        {
            Invoke("Shoot", pauseBetweenShots);
        }        
    }

    private void ResetShot()
    {
        readyToShoot = true;
    }

    private void Reload()
    {
        reloading = true;
        Invoke("ReloadFinished", reloadTime);
    }

    private void ReloadFinished()
    {
        bulletsLeft = magSize;
        reloading = false;
    }

}
