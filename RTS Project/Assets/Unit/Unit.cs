using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Unit : MonoBehaviour
{
    
    NavMeshAgent agent;
    Animator animator;
    Vector3 positionToMove;
    //
    bool usercommand = false;
    //

    ////////////////////Attack////////////////////
    bool firstArrive = true;
    float attackConter = 0.0f;
    float sqrAttackRange = 0.0f;
    GameObject targetAttack;
    [SerializeField] int attackRange = 5;
    [SerializeField] int DetectionRange = 20;
    [SerializeField] int attackDamage = 25;
    [SerializeField] float attackSpeed = 3.0f;
    [SerializeField] LayerMask enemiesLayerMask = 1 << 8;
    //////////////////////////////////////////////

    ////////////////////HEALTH////////////////////
    [SerializeField] float maxHealthPoints = 100f;
    float currentHealthPoints = 100f;
    public float healthAsPercentage
    { get { return currentHealthPoints / maxHealthPoints; } }
    //////////////////////////////////////////////

    void Start()
    {
        targetAttack = null;
        attackConter = attackSpeed;
        sqrAttackRange = attackRange * attackRange;
        agent = GetComponent<NavMeshAgent>();
        animator = GetComponentInChildren<Animator>();

        //TODO change to network
        if (gameObject.layer == 8)
        {
            return;
        }

        PlayerUnits.playerUnits.Add(gameObject);        
    }

    public void Select()
    {
        PlayerUnits.selectedUnits.Add(gameObject);
        GetComponentInChildren<Canvas>().enabled = true;
    }
    public void Deselect()
    {
        GetComponentInChildren<Canvas>().enabled = false;
    }
    public void takeDamage(int damage)
    {
        currentHealthPoints -= damage;
        if (currentHealthPoints <= 0)
        {
            Destroy(gameObject);
        }
    }




    public void Move(Vector3 point,bool commandUser)
    {
        animator.SetTrigger("isWalking");
        positionToMove = point;

        if (commandUser)
        {
            usercommand = true;
            targetAttack = null;
            agent.stoppingDistance = 0;
            agent.destination = positionToMove;
        }
        else
        {
            usercommand = false;
            agent.stoppingDistance = attackRange;
            agent.destination = positionToMove;
        }
    }




    public void detect()
    {
        Collider[] Enemies = Physics.OverlapSphere(gameObject.transform.position, DetectionRange, enemiesLayerMask);
        if (Enemies.Length != 0)
        {
            //positionToMove = Enemies[0].gameObject.transform.position;
            targetAttack = Enemies[0].gameObject;
            Move(targetAttack.transform.position,false);
        }
    }

    

    private void Update()
    {
        //TODO change to network
        if (gameObject.layer == 8)
        {
            return;
        }









        if (usercommand == true)
        {
            //if i arrived
            if ((positionToMove - gameObject.transform.position).sqrMagnitude <= 1)
            {
                usercommand = false;
            }
        }
        














        else
        {
            ///if no enemies search for one and move affter him
            if (targetAttack == null)
            {
                animator.SetTrigger("isIdeal");
                detect();
            }


            // if there is an enemy
            else
            {
                //if enemy changed position , follow him
                if (positionToMove != targetAttack.transform.position)
                {
                    animator.SetTrigger("isWalking");
                    Move(targetAttack.transform.position, false);
                    firstArrive = true;
                }

                else
                {
                    //if i reached enemy , attack him
                    if ((positionToMove - gameObject.transform.position).sqrMagnitude <= sqrAttackRange)
                    {
                        if (firstArrive)
                        {
                            animator.SetTrigger("isIdeal");
                            firstArrive = false;
                        }
                        if (attackConter <= 0.0f)
                        {
                            animator.SetTrigger("isAttacking");
                            Unit n = targetAttack.GetComponent<Unit>();
                            //TODO CHANGE TO NETWORK
                            n.takeDamage(attackDamage);
                            attackConter = attackSpeed;
                        }
                        else
                        {
                            attackConter -= Time.deltaTime;
                        }
                    }
                }
            }




















        }
        
    }
}