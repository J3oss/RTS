using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Unit : MonoBehaviour
{
    [SerializeField]GameObject targetAttack;
    NavMeshAgent agent;
    //Animator animator;

    ////////////////////Attack////////////////////
    float attackConter = 0.0f;
    float sqrAttackRange = 0.0f;
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
        agent = GetComponent<NavMeshAgent>();
        //animator = this.transform.Find("toonRTS").GetComponent<Animator>();
        attackConter = attackSpeed;
        sqrAttackRange = attackRange * attackRange;

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

    public void Move(Vector3 point)
    {
        targetAttack = null;
        agent.stoppingDistance = 0;

        agent.destination = point;
    }
    public void detect()
    {
        Collider[] Enemies = Physics.OverlapSphere(gameObject.transform.position, DetectionRange, enemiesLayerMask);
        Attack(Enemies[0].gameObject);
    }
    public void Attack(GameObject targetToAttack)
    {
        targetAttack = targetToAttack;
        agent.stoppingDistance = attackRange;
    }
    public void takeDamage(int damage)
    {
        currentHealthPoints -= damage;
        if (currentHealthPoints <= 0)
        {
            Destroy(gameObject);
        }
    }

    private void Update()
    {
        if (gameObject.layer == 8)
        {
            return;
        }
        if (targetAttack == null)
        {
            detect();
            return;
        }

        if ((gameObject.transform.position - targetAttack.transform.position).sqrMagnitude >= sqrAttackRange)
        {
            agent.destination = targetAttack.transform.position;
        }
        else
        {
            if (attackConter <= 0.0f)
            {
                Unit n = targetAttack.GetComponent<Unit>();
                print("do damage");
                print(attackConter);
                n.takeDamage(attackDamage);
                attackConter = attackSpeed;
            }
            else
            {
                print(attackConter);
                attackConter -= Time.deltaTime;
            }
        }
    }
}