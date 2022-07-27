using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using UnityEngine.Rendering.PostProcessing;
using UnityEngine.UI;

public class moveController : MonoBehaviour
{



    // Start is called before the first frame update

    [SerializeField] private float moveSpeed;
    [SerializeField] private float runSpeed;
    [SerializeField] private float walkSpeed;

    private CharacterController controller;

    [SerializeField] private bool isGrounded;
    [SerializeField] private float checkGround;
    [SerializeField] private LayerMask layerMask;

    [SerializeField] private float gravity;
    private Vector3 velocity;

    [SerializeField] private float jumpForce;

    [SerializeField] private Animator animator;
    [SerializeField] private bool isLanded;

    [SerializeField] private bool isFiring;
    [SerializeField] private bool cannotFire;
    [SerializeField] private bool cannotDefend;
    [SerializeField] private float health;

    [SerializeField] private float timerForFast;

    [SerializeField] private ParticleSystem fire;

    [SerializeField] private PostProcessVolume postVolume;

    [SerializeField] private Image healthBar;

    [SerializeField] private GameObject fastBar;

    [SerializeField] private AudioClip[] clipOfHero;

    [SerializeField] public bool isDie;

    [SerializeField] private GameObject UICanvas;
    [SerializeField] private GameObject DeathCanvas;
    [SerializeField] private GameObject bossCanvas;


    private float walkSpeeed;
    private float runSpeeed;

    [SerializeField] private GameObject Boss;
    [SerializeField] private GameObject platformlose;
    [SerializeField] private GameObject platformSpawn;

    private AudioSource walkingSource;
    private AudioSource runningSource;
    private AudioSource walkingSource2;
    private AudioSource runningSource2;
    private AudioSource jumpingSource;
    private AudioSource landingSource;
    private AudioSource swordSource;
    private AudioSource fireSource;

    [SerializeField] private GameObject collectCanvas;
    [SerializeField] private Image fireBar;



    void Start()
    {
        Time.timeScale = 1;

        walkingSource = gameObject.AddComponent<AudioSource>();
        runningSource = gameObject.AddComponent<AudioSource>();
        walkingSource2 = gameObject.AddComponent<AudioSource>();
        runningSource2 = gameObject.AddComponent<AudioSource>();
        jumpingSource = gameObject.AddComponent<AudioSource>();
        landingSource = gameObject.AddComponent<AudioSource>();
        swordSource = gameObject.AddComponent<AudioSource>();
        fireSource = gameObject.AddComponent<AudioSource>();

        swordSource.loop = false;
        walkingSource.loop = false;
        runningSource.loop = false;
        walkingSource2.loop = false;
        runningSource2.loop = false;
        jumpingSource.loop = false;
        landingSource.loop = false;
        fireSource.loop = false;

        walkingSource.clip = clipOfHero[0];
        runningSource.clip = clipOfHero[1];
        jumpingSource.clip = clipOfHero[2];
        landingSource.clip = clipOfHero[3];
        walkingSource2.clip = clipOfHero[4];
        runningSource2.clip = clipOfHero[5];
        swordSource.clip = clipOfHero[6];
        fireSource.clip = clipOfHero[7];


        walkingSource.volume = .5f;
        runningSource.volume = .5f;
        jumpingSource.volume = .5f;
        landingSource.volume = .5f;
        walkingSource2.volume = .5f;
        runningSource2.volume = .5f;





        controller = GetComponent<CharacterController>();
        cannotFire = true;
        cannotDefend = true;
        isDie = false;

        walkSpeeed = walkSpeed;
        runSpeeed = runSpeed;
    }

    // Update is called once per frame
    void Update()
    {

       
        
        if (Vector3.Distance(Boss.transform.position, transform.position) <= 20f)
        {
            if (bossCanvas != null)
            {
                bossCanvas.SetActive(true);
            }

            platformSpawn.SetActive(true);
            platformlose.SetActive(false);
        }

        if (timerForFast > 0)
        {
            fastBar.GetComponent<Image>().fillAmount -= Time.deltaTime / 10;
            timerForFast -= Time.deltaTime;
        }

        else
        {
            fastBar.SetActive(false);
            timerForFast = 0;
            walkSpeed = walkSpeeed;
            runSpeed = runSpeeed;


        }

        if (!isDie)
        {



            Move();

            if (Input.GetMouseButtonDown(0))
            {

                Attack(0);
            }
            else if (Input.GetKeyDown(KeyCode.Tab))
            {

                Attack(1);
            }
        }
    }

    private void Move()
    {
        isGrounded = Physics.CheckSphere(transform.position, checkGround, layerMask);

        if (isGrounded)
        {

            animator.SetTrigger("Landed");
        }




        float inputHorizontal = Input.GetAxis("Horizontal");
        float inputVertical = Input.GetAxis("Vertical");


        Vector3 direction = new Vector3(inputHorizontal, 0, inputVertical);
        direction = transform.TransformDirection(direction);

        if (isGrounded)
        {
            if (Input.GetMouseButton(1)&& !isFiring)
            {
                cannotDefend = false;
                animator.SetBool("isDefending",true);
            }

            if (Input.GetMouseButtonUp(1))
            {
                cannotDefend = true;
                animator.SetBool("isDefending",false);
            }



            if (Input.GetKeyDown(KeyCode.Mouse2) && cannotDefend)
            {
                fireSource.Play();
                
            }

            if (Input.GetKey(KeyCode.Mouse2) && cannotDefend)
            {


                if (fireBar.fillAmount != 0)
                {


                    fireBar.fillAmount -= Time.deltaTime / 10;
                    animator.SetBool("Firing", true);
                    isFiring = true;
                    cannotFire = false;
                }
                else
                {
                    fireSource.Stop();
                    animator.SetBool("Firing", false);
                    isFiring = false;
                    cannotFire = true;
                }
            }

            if (Input.GetKeyUp(KeyCode.Mouse2))
            {
                if (fireBar.fillAmount==0)
                {
                    InvokeRepeating("Filler",5f,Time.deltaTime);
                }
                fireSource.Stop();
                animator.SetBool("Firing", false);
                isFiring = false;
                cannotFire = true;
            }

            if (isFiring)
            {
                
                fire.Play();
            }
            else if (!isFiring)
            {
                fire.Pause();
                fire.Clear();
            }


            if (cannotFire&&cannotDefend)
            {


                if (direction != Vector3.zero && !Input.GetKey(KeyCode.LeftShift))
                {
                    Walk();
                }
                else if (direction != Vector3.zero && Input.GetKey(KeyCode.LeftShift))
                {
                    Run();
                }
                else if (direction == Vector3.zero)
                {

                    Idle();
                }

                if (Input.GetKeyDown(KeyCode.Space))
                {
                    Jump();
                }

                controller.Move(direction * Time.deltaTime * moveSpeed);
            }

        }




        velocity.y += gravity * Time.deltaTime;

        controller.Move(velocity * Time.deltaTime);

        DowningChecker();

    }

    public void OnTriggerEnter(Collider coll)
    {
        if (coll.gameObject.CompareTag("Material"))
        {
            collectCanvas.GetComponent<QuestCollectCanvas>().GetCollect();
            Destroy(coll.gameObject);
        }
    }

    void Filler()
    {
        fireBar.fillAmount += Time.deltaTime / 75;
    }

    void Idle()
    {

        animator.SetFloat("Run Blend", 0f, 0.1f, Time.deltaTime);
    }

    void Run()
    {

        animator.SetFloat("Run Blend", 1f, 0.1f, Time.deltaTime);
        moveSpeed = runSpeed;
    }


    void Walk()
    {

        animator.SetFloat("Run Blend", 0.5f, 0.1f, Time.deltaTime);
        moveSpeed = walkSpeed;
    }




    void Jump()
    {

        animator.SetBool("Upping", true);
        velocity.y = Mathf.Sqrt(gravity * -1.5f * jumpForce);

    }

    void DowningChecker()
    {
        if (velocity.y < 0 && !isGrounded)
        {
            animator.SetBool("Upping", false);
            animator.SetBool("Downing", true);
        }
    }

    void Attack(int attackType)
    {
        switch (attackType)
        {
            case 0:
                animator.SetTrigger("LightAttack");
                break;
            case 1:
                animator.SetTrigger("HeavyAttack");
                break;
        }
    }

    void SwordActive()
    {
        GameObject.FindGameObjectWithTag("sword").GetComponent<BoxCollider>().isTrigger = true;
    }

    void SwordPassive()
    {
        GameObject.FindGameObjectWithTag("sword").GetComponent<BoxCollider>().isTrigger = false;
    }

    public void GetDamage(float damage,string defendable)
    {
        if (!isDie)
        {
            switch (defendable)
            {
                case "No":
                    GetRealDamage(damage);
                    break;
                case "Yes":
                    if (Input.GetMouseButton(1))
                    {
                        animator.SetTrigger("isBlocking");
                    }
                    else
                    {
                        GetRealDamage(damage);
                    }

                    break;
            }

            
        }
    }

    public void DisableCanvas()
    {
        Destroy(bossCanvas);
    }

    public void GetRealDamage(float damage)
    {
        health -= damage;

        healthBar.fillAmount -= (damage / 100);

        if (health <= 20)
        {
            postVolume.enabled = true;
        }

        if (health <= 0)
        {
            isDie = true;
            SwordPassive();
            swordSource.PlayOneShot(clipOfHero[8]);
            animator.SetTrigger("isDie");
        }
    }

    public void SetDeathScene()
    {
        UICanvas.SetActive(false);
        DeathCanvas.SetActive(true);
    }

    public void GetHealth(float healthValue)
    {
        swordSource.PlayOneShot(clipOfHero[9]);

        health += healthValue;
        healthBar.fillAmount += (healthValue / 100);

        if (health > 20)
        {
            postVolume.enabled = false;
        }

        if (health > 100)
        {
            health = 100;
        }

    }

    public void Boost(float boostValue)
    {
        swordSource.PlayOneShot(clipOfHero[10]);

        fastBar.SetActive(true);
        fastBar.GetComponent<Image>().fillAmount = 1;
        runSpeed += boostValue;
        walkSpeed += boostValue;
        timerForFast = 10f;
    }

    public void Audios(int index)
    {
        switch (index)
        {
            case 0:
                walkingSource.Play();
                break;
            case 1:
                runningSource.Play();
                break;
            case 2:
                jumpingSource.Play();
                break;
            case 3:
                landingSource.Play();
                break;
            case 4:
                walkingSource2.Play();
                break;
            case 5:
                runningSource2.Play();
                break;
            case 6:
                swordSource.Play();
                break;
        }
    }
}
