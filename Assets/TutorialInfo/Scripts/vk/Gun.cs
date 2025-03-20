using UnityEngine;
using System.Collections.Generic;
using System.Collections;
using UnityEngine.InputSystem;
using System;
using TMPro;

public class Gun : MonoBehaviour
{
    //seni is for shotgun because it has limited amount of ammo
    public enum GunType { Seni, Auto, Semi};

    public LayerMask collisionMask;
    public float gunID;
    public GunType gunType;
    public float rpm;
    public int damage = 1;

    public float reloadTime;
    public int magazineSize, bulletsLeft, magazinesLeft;
    public bool isReloading;

    //ui
    public TextMeshProUGUI ammoDisplay;

    public Transform spawn;
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
        magazinesLeft = bulletsLeft + magazineSize;
        isReloading = false;
        if (Ammodisplay.Instance.ammoDisplay != null)
        {
            Ammodisplay.Instance.ammoDisplay.text = $"{ bulletsLeft}/{magazineSize}={magazinesLeft}";
        }
    }

    public void Shoot()
    {
        if (Ammodisplay.Instance.ammoDisplay != null)
        {
            Ammodisplay.Instance.ammoDisplay.text = $"{ bulletsLeft}/{magazineSize}";
        }

        if (CanShoot())
        {
            bulletsLeft--;
            magazinesLeft--;
            playerAnimator.SetTrigger("shoot");
            //lokation wher the bulet comes uout of
            Ray ray = new Ray(spawn.position, spawn.forward);
            RaycastHit hit;

            float shotDistance = 200f;

            /*if (Physics.Raycast(ray, out hit, shotDistance, collisionMask))
            {
                shotDistance = hit.distance;
                //i made it to work whit enemy health
                if (hit.transform.GetComponent<Health>())
                {
                    hit.transform.GetComponent<Health>().TakeDamage(damage);
                }
            }*/

            //Debug.DrawRay(ray.origin, ray.direction * shotDistance, Color.red, 1f);

            nextPossibleShootTime = (Time.time + secondsBetweenShots);

            if (tracer)
            {
                StartCoroutine("RenderTracer", ray.direction * shotDistance);
            }
        }

    }

    public void ShootComtinuous()
    {
        if (gunType == GunType.Seni && bulletsLeft > 0)
        {
            Shoot();
        }
        if (gunType == GunType.Auto && bulletsLeft > 0)
        {
            Shoot();
        }
    }
    //This loks that it cant shoot if ther are no bullets left
    private bool CanShoot()
    {
        var canShoot = true && Time.time >= nextPossibleShootTime;

        if (bulletsLeft == 0 | isReloading)
        {
            canShoot = false;
        }
        if (magazinesLeft == 0 | isReloading)
        {
            isReloading = false;
        }

        return canShoot;
    }

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
        magazinesLeft = bulletsLeft + magazineSize;
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
    }
}