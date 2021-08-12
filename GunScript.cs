using System.Collections;
using UnityEngine;
using TMPro;

public class GunScript : MonoBehaviour
{
    public float damage = 50f;
    public float range = 100f;
    public float impactForce = 30;
    public float fireRate = 15;

    public int maxAmmo = 1;
    public int currentAmmo;
    public float reloadTime = 1f;
    public bool isReloading = false;
    public bool isShooting = false;
    public Camera fpsCam;
    public ParticleSystem muzzleFlash;
    public GameObject impactEffect;
    public Animator animator;
    public TextMeshProUGUI ammo;
    public AudioSource gunshot;
    public AudioSource reload;

    private float nextTimeToFire = 0f;
    public GameObject light1;
    public GameObject light2;
    public AudioSource lightOn;
    public AudioSource lightOff;
    public GameObject shotLight;

    private void Start()
    {
        currentAmmo = maxAmmo;
    }

    private void OnEnable()
    {
        isReloading = false;
        animator.SetBool("Reloading", false);
    }
    // Update is called once per frame
    void Update()
    {
        ammo.SetText(currentAmmo.ToString() + "/" + maxAmmo.ToString());
        if (isReloading)
            return;
        if(currentAmmo <= 0 || (Input.GetButtonDown("Reload")&& currentAmmo < maxAmmo))
        {
            StartCoroutine(Reload());
            return;
        }
        if (Input.GetButtonDown("Fire1") && Time.time >= nextTimeToFire)
        {
            nextTimeToFire = Time.time + 1f / fireRate;
            StartCoroutine(Shoot());
            return;
        }
        if (Input.GetButtonDown("Flashlight"))
        {
            if (light1.activeSelf == true)
            {
                lightOff.Play();
            }
            else
                lightOn.Play();
            light1.SetActive(!light1.activeSelf);
            light2.SetActive(!light2.activeSelf);
        }
        
    }

    IEnumerator Reload()
    {
        isReloading = true;
        Debug.Log("Reloading...");
        yield return new WaitForSeconds(.2f);
        reload.Play();
        animator.SetBool("Reloading", true);

        yield return new WaitForSeconds(reloadTime -.55f);

        animator.SetBool("Reloading", false);

        yield return new WaitForSeconds(.25f);

        currentAmmo = maxAmmo;
        yield return new WaitForSeconds(.1f);
        isReloading = false;
    }
    IEnumerator Shoot()
    {
        isShooting = true;
        gunshot.Play();
        muzzleFlash.Play();
        shotLight.SetActive(true);
        Debug.Log("Current Ammo: " + currentAmmo);
        RaycastHit hit;
        currentAmmo--;
        if (Physics.Raycast(fpsCam.transform.position, fpsCam.transform.forward, out hit, range))
        {
            Debug.Log(hit.transform.name);
            Target target = hit.transform.GetComponent<Target>();
            if (target != null)
            {
                target.TakeDamage(damage);
            }
            if (hit.rigidbody != null)
            {
                hit.rigidbody.AddForce(fpsCam.transform.forward * impactForce);
            }
            Instantiate(impactEffect, hit.point, Quaternion.LookRotation(hit.normal));
        }
        yield return new WaitForSeconds(.1f);
        shotLight.SetActive(false);
        animator.SetTrigger("Shot 0");
        yield return new WaitForSeconds(.2f);
        animator.SetTrigger("Shot 0");
        isShooting = false;
    }
}
