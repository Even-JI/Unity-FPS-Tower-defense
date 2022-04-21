using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GunScript : MonoBehaviour
{
    [Header ("Gun Stats")]
    public float damage;
    public float range;
    public float recoil;
    public float reloadTime;
    public float delayBetweenShots;
    public int magSize, bulletsInMag, bulletsShot;

    [Header("Gun References")]
    public GameObject player;
    public Camera mainCamera;
    public Camera fpsCam;
    public ParticleSystem MuzzleFlash;
    public AudioSource singleShotAudio;
    public AudioSource magEjectAudio;
    public AudioSource magInsertAudio;

    Animator animator;
    Animator fpsCamAnimator;
    bool busyReload = false;
    bool busyShoot = false;
    bool aiming = false;
    public bool isIdle = false;
    public bool isWalking = false;
    public bool isRunning = false;



    private void Start()
    {
        animator = GetComponent<Animator>();
        fpsCamAnimator = fpsCam.GetComponent<Animator>();
    }

    private void Update()
    {
        isWalking = player.transform.gameObject.GetComponent<PlayerMovement>().isWalking;
        isRunning = player.transform.gameObject.GetComponent<PlayerMovement>().isRunning;

        bulletsInMag = magSize - bulletsShot;

        if (!busyReload)
        {
            if (Input.GetButtonDown("Fire1") && bulletsInMag > 0 && !busyShoot)
            {
                Shoot();
                if (aiming == false) { animator.SetTrigger("Shoot"); }
                //fpsCam.transform.rotation *= Quaternion.Euler(180, 0 ,0);
                //fpsCam.transform.eulerAngles = new Vector3 (fpsCam.transform.rotation.x + recoil, fpsCam.transform.rotation.y, fpsCam.transform.rotation.z);
                //mainCamera.transform.eulerAngles = new Vector3(fpsCam.transform.rotation.x + recoil, fpsCam.transform.rotation.y, fpsCam.transform.rotation.z);
                //mainCamera.transform.rotation.x 
                //Debug.Log("Rot: "+ fpsCam.transform.rotation);

            }
            if (Input.GetButton("Fire2"))
            {
                aiming = true;
                animator.SetBool("Aiming", true);
                fpsCamAnimator.SetBool("Aiming", true);
            }
            else
            {
                aiming = false;
                animator.SetBool("Aiming", false);
                fpsCamAnimator.SetBool("Aiming", false);
            }
            if ((Input.GetKeyDown(KeyCode.R) && bulletsShot != 0) || bulletsInMag == 0)
            {
                StartCoroutine(Reload(reloadTime));
            }
            CheckMovement();
        }
    }

    void Shoot()
    {
        busyShoot = true;
        MuzzleFlash.Play();
        PlayShotAudio();
        bulletsShot++;

        
        RaycastHit hitInfo;

        if (Physics.Raycast(fpsCam.transform.position, fpsCam.transform.forward, out hitInfo, range))
        {
            //Debug.Log(hitInfo.transform.root.name + " hit for " + damage + "damage.");

            TargetScript target = hitInfo.transform.GetComponent<TargetScript>();
            if(target != null)
            {
                target.TakeDamage(damage);
            }
        }
        if (bulletsShot == magSize) { animator.SetBool("Empty", true); }
        Invoke("ReadyToShoot", delayBetweenShots);
    }

    IEnumerator Reload(float busyTime)
    {
        busyReload = true;
        animator.SetTrigger("Reload");

        yield return new WaitForSeconds(busyTime);

        bulletsShot = 0;
        bulletsInMag = magSize;
        animator.SetBool("Empty", false);

        busyReload = false;
    }

    void ReadyToShoot()
    {
        busyShoot = false;
    }

    void PlayShotAudio()
    {
        singleShotAudio.time = 0.2f;
        singleShotAudio.Play();
    }

    void PlayEjectMagAudio()
    {
        magEjectAudio.Play();
    }

    void PlayInsertMagAudio()
    {
        magInsertAudio.Play();
    }

    void CheckMovement()
    {
        if (!isWalking && !isRunning)
        {
            animator.SetBool("Walking", false);
            animator.SetBool("Running", false);
        }
        if (isWalking)
        {
            animator.SetBool("Walking", true);
            animator.SetBool("Running", false);
        }
        if (isRunning)
        {
            animator.SetBool("Walking", false);
            animator.SetBool("Running", true);
        }
    }


































    /*public int damage;
    public float TimeBetweenShots, Accuracy, Range, ReloadTime, ROF;
    public int MagSize, ShotsPerTrigger;
    public bool AllowButtonHold;
    int BulletsInMag, BulletsShot;

    bool shooting, readyToShoot, reload;

    public Camera FPSCamera;
    Transform AttackPoint;
    public RaycastHit rayHit;
    public LayerMask Target;

    private Animator GunAnim;


    private void Start()
    {
        GunAnim = GetComponent<Animator>();
        BulletsInMag = MagSize;
        readyToShoot = true;
    }
    private void Update()
    {
        MyInput();
    }

    private void MyInput()
    {
        if (AllowButtonHold) shooting = Input.GetKey(KeyCode.Mouse0);
        else shooting = Input.GetKeyDown(KeyCode.Mouse0);

        if(readyToShoot && shooting && !reload && BulletsInMag > 0)
        {
            BulletsShot = ShotsPerTrigger;
            Shoot();
        }
    }

    private void Shoot()
    {
        readyToShoot = false;

        float x = Random.Range(-Accuracy, Accuracy);
        float y = Random.Range(-Accuracy, Accuracy);

        Vector3 direction = FPSCamera.transform.forward + new Vector3(x, y, 0);

        if (Physics.Raycast(FPSCamera.transform.position, direction, out rayHit, Range, Target))
        {
            Debug.Log(rayHit.collider.name);

            if (rayHit.collider.CompareTag("Enemy"))
                rayHit.collider.GetComponent<EnemyAI>().TakeDamage(damage);
        }

        BulletsInMag--;

        Invoke("ResetShot", TimeBetweenShots);

        if (BulletsShot > 0 && BulletsInMag > 0) ;
        Invoke("Shoot", TimeBetweenShots);
    }
    private void ResetShot()
    {
        readyToShoot = true;
    }
    private void Reload()
    {
        reload = true;
        Invoke("ReloadFinished", ReloadTime);
    }
    private void ReloadFinished()
    {
        BulletsInMag = MagSize;
        reload = false;
    }*/

}   
