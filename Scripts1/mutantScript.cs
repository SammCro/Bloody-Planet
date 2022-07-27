using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.UI;

public class mutantScript : MonoBehaviour
{

    [SerializeField] private GameObject target;
    [SerializeField] private GameObject[] points;
    [SerializeField] private Animator _animator;

    private Vector3 nextPosition;
    private Vector3 currentPosition;

    [SerializeField] private NavMeshAgent monsterAgent;

    private int positionNumber;

    private bool isRoaming;
    private bool isChasing;
    private bool isAttacking;
    [SerializeField] private ParticleSystem effect;

    [SerializeField]
    private float healthOfMonster;

    [SerializeField] private bool isDead = false ;

    [SerializeField] private float distanceToAttack;

    [SerializeField] private string monsterType;

    [SerializeField] private AudioClip attack;
    [SerializeField] private AudioClip die;

    private string meleeType;

    [SerializeField] private GameObject questCanvas;
    [SerializeField] private GameObject sectorCompleted;

    private bool isBoss = false ;

    [SerializeField] private Image BossFightImage;

    // Start is called before the first frame update
    void Start()
    {
        switch (monsterType)
        {
            case "Parazite":
                meleeType = "ParazitMelee";
                break;
            case "Mutant":
                meleeType = "MutantMelee";
                break;
            case "Boss":
                isBoss = true;
                meleeType = "BossMelee";
                break;
        }





        positionNumber = 0;
        currentPosition = points[positionNumber].transform.position;
    }

    // Update is called once per frame
    void Update()
    {
        if (!isDead)
        {


            TargetCheck();

            if (isRoaming)
            {
                monsterAgent.SetDestination(currentPosition);
                NextPosition();
            }
            else if (isAttacking)
            {
                monsterAgent.transform.LookAt(target.transform.position);
                monsterAgent.ResetPath();
                monsterAgent.SetDestination(target.transform.position);
                


            }
            else if (isChasing)
            {
                monsterAgent.ResetPath();
                monsterAgent.SetDestination(target.transform.position);

            }
        }
    }

    void NextPosition()
    {
        if (Vector3.Distance(currentPosition, transform.position) < 6f)
        {


            if (positionNumber >= points.Length-1)
            {
                positionNumber = 0;
                currentPosition = points[positionNumber].transform.position;

            }
            else
            {
                positionNumber++;
                currentPosition = points[positionNumber].transform.position;

            }
        }
    }

    void TargetCheck()
    {
        if (Vector3.Distance(target.transform.position, transform.position) >= 20f)
        {
            _animator.SetBool("isWalking",true);
            _animator.SetBool("isChasing",false);
            _animator.SetBool("isAttacking",false);


            isRoaming = true;
            isAttacking = false;
            isChasing = false;
        }
        else if (Vector3.Distance(target.transform.position,transform.position)<20f && (Vector3.Distance(target.transform.position, transform.position) > distanceToAttack))
        {
            _animator.SetBool("isWalking", false);
            _animator.SetBool("isChasing", true);
            _animator.SetBool("isAttacking", false);

            isChasing = true;
            isAttacking = false;
            isRoaming = false;
        }

        else if (GameObject.FindGameObjectWithTag("Player").GetComponent<moveController>().isDie)
        {
            _animator.SetBool("isWalking", true);
            _animator.SetBool("isChasing", false);
            _animator.SetBool("isAttacking", false);


            isRoaming = true;
            isAttacking = false;
            isChasing = false;
        }

        else if(Vector3.Distance(target.transform.position, transform.position) <= distanceToAttack && !GameObject.FindGameObjectWithTag("Player").GetComponent<moveController>().isDie)
        {
            
            _animator.SetBool("isWalking", false);
            _animator.SetBool("isChasing", false);
            _animator.SetBool("isAttacking", true);

            isAttacking = true;
            isRoaming = false;
            isChasing = false;
        }
    }


    void OnParticleCollision(GameObject coll)
    {
        if (!isDead)
        {
            if (isBoss)
            {
                BossFightImage.fillAmount -= .02f;
            }

            healthOfMonster -= 1;

            if (healthOfMonster <= 0)
            {
                if (isBoss)
                {
                    GameObject[] areasToDestroy = GameObject.FindGameObjectsWithTag("AfterBoss");
                    for (int i = 0; i < areasToDestroy.Length; i++)
                    {
                        Destroy(areasToDestroy[i]);
                    }

                    GameObject.FindGameObjectWithTag("Player").GetComponent<moveController>().DisableCanvas();
                    sectorCompleted.SetActive(true);
                    questCanvas.SetActive(false);
                }


                if (!isBoss)
                {
                    if (GameObject.FindGameObjectWithTag("QuestsCanvas") != null)
                    {


                        GameObject.FindGameObjectWithTag("QuestsCanvas").GetComponent<QuestsCanvas>().DeathMonster();
                    }
                }


                DamagePassive();
                _animator.SetTrigger("isDying");
                isDead = true;
                gameObject.GetComponent<Rigidbody>().isKinematic = true;

                
            }
        }
    }


    void DamageActive()
    {
        GameObject[] melees = GameObject.FindGameObjectsWithTag(meleeType);

        for (int i = 0; i <= melees.Length-1; i++)
        {
            melees[i].GetComponent<CapsuleCollider>().isTrigger = true;
        }

    }

    void DamagePassive()
    {
        GameObject[] melees = GameObject.FindGameObjectsWithTag(meleeType);

        for (int i = 0; i <= melees.Length-1; i++)
        {
            melees[i].GetComponent<CapsuleCollider>().isTrigger = false;
        }

    }

    public void GetDamage(float damage)
    {
        if (!isDead)
        {


            healthOfMonster -= damage;

            if (isBoss)
            {
                BossFightImage.fillAmount -= (damage/healthOfMonster);
            }

            if (healthOfMonster <= 0)
            {
                if (isBoss)
                {
                    GameObject[] areasToDestroy = GameObject.FindGameObjectsWithTag("AfterBoss");
                    for (int i = 0; i < areasToDestroy.Length; i++)
                    {
                        Destroy(areasToDestroy[i]);
                    }
                    GameObject.FindGameObjectWithTag("Player").GetComponent<moveController>().DisableCanvas();
                    sectorCompleted.SetActive(true);
                    questCanvas.SetActive(false);
                }

                if (!isBoss)
                {


                    if (GameObject.FindGameObjectWithTag("QuestsCanvas") != null)
                    {


                        GameObject.FindGameObjectWithTag("QuestsCanvas").GetComponent<QuestsCanvas>().DeathMonster();
                    }
                }

                _animator.SetTrigger("isDying");
                isDead=true;
                DamagePassive();

                
            }
        }
    }

    public void AttackSound()
    {
        gameObject.GetComponent<AudioSource>().PlayOneShot(attack);
    }

    public void DieSound()
    {
        gameObject.GetComponent<AudioSource>().PlayOneShot(die);
    }

}