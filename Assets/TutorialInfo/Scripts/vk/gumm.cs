/*
using UnityEngine;
using System.Collections.Generic;
using System.Collections;
using UnityEngine.InputSystem;
using System;
using TMPro;

public class Gumm : MonoBehaviour
{
    public enum GunType { Seni, Auto };

    public LayerMask collisionMask;
    public float gunID;
    public GunType gunType;
    public float rpm;
    public int damage = 1;

    public float reloadTime;
    public int magazineSize, bulletsLeft;
    public bool isReloading;

    //ui
    public TextMeshProUGUI ammoDisplay;

    public Transform spawn;
    public Transform shellEjectionPoint;
    public Rigidbody shell;
    private LineRenderer tracer;

    public float secondsBetweenShots;
    public float nextPossibleShootTime;
    [SerializeField] private Animator playerAnimator;
    InputAction reloadAction;

    public void Start()
    {
        secondsBetweenShots = (60 / rpm);
        reloadAction = InputSystem.actions.FindAction("Reload");
        if (GetComponent<LineRenderer>())
        {
            tracer = GetComponent<LineRenderer>();
        }
    }

    public void Reload()
    {
        isReloading = true;
        Invoke("ReloadCompleted", reloadTime);
    }

    public void ReloadCompleted()
    {
        bulletsLeft = magazineSize;
        isReloading = false;
        if (AmmoManager.Instance.ammoDisplay != null)
        {
            AmmoManager.Instance.ammoDisplay.text = $"{ bulletsLeft}/{magazineSize}";
        }
    }

    public void Shoot()
    {
        if (AmmoManager.Instance.ammoDisplay != null)
        {
            AmmoManager.Instance.ammoDisplay.text = $"{ bulletsLeft}/{magazineSize}";
        }

        if (CanShoot())
        {
            bulletsLeft--;
            playerAnimator.SetTrigger("shoot");

            Ray ray = new Ray(spawn.position, spawn.forward);
            RaycastHit hit;

            float shotDistance = 200f;

            if (Physics.Raycast(ray, out hit, shotDistance, collisionMask))
            {
                shotDistance = hit.distance;
                if (hit.transform.GetComponent<HasHealth>())
                {
                    hit.transform.GetComponent<HasHealth>().TakeDamage(damage);
                }


                // if (Physics.Raycast(ray,out hit, shotDistance, collisionMask))
                // {
                //     shotDistance = hit.distance; 
                // }
            }

            //Debug.DrawRay(ray.origin, ray.direction * shotDistance, Color.red, 1f);

            nextPossibleShootTime = (Time.time + secondsBetweenShots);

            if (tracer)
            {
                StartCoroutine("RenderTracer", ray.direction * shotDistance);
            }
            // Rigidbody newShell = Instantiate(shell, shellEjectionPoint.position, Quaternion.identity) as Rigidbody;
            // newShell.AddForce(shellEjectionPoint.forward * UnityEngine.Random.Range(150f, 200f) + spawn.forward * UnityEngine.Random.Range(-10f, 10f));
        }

    }


    public void ShootComtinuous()
    {
        if (gunType == GunType.Auto)
        {
            Shoot();
        }
    }
    private bool CanShoot()
    {
        var canShoot = true && Time.time >= nextPossibleShootTime;

        if (bulletsLeft == 0 | isReloading)
        {
            canShoot = false;
        }

        return canShoot;
    }
    /* private bool CanShoot()
     {
         bool canShoot = true;

         if (Time.time < nextPossibleShootTime)
         {
             canShoot = false;
         }



         return canShoot;
     }*//*
    IEnumerator RenderTracer(Vector3 hitPoint)
    {
        tracer.enabled = true;
        tracer.SetPosition(0, spawn.position);
        tracer.SetPosition(1, spawn.position + hitPoint);
        yield return new WaitForSeconds(0.1f);
        tracer.enabled = false;
    }
    private void Awake()
    {
        bulletsLeft = magazineSize;
    }
    public void Update()
    {
        //change to new input system
        if (reloadAction.WasPressedThisFrame() && bulletsLeft < magazineSize && isReloading == false)
        {
            Reload();
        }
        // auto reloade
        if (CanShoot() == false && isReloading == false && bulletsLeft <= 0)
        {
            Reload();
        }
        /*if (bulletsLeft == 0)
        {
            isReloading = true;
        }*/

        // 1 magazinesize was secondsbetweenshots
  /*  }
}*/