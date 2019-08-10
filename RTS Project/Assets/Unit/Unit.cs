using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Unit : MonoBehaviour, ISelectable
{
    [SerializeField] public GameObject targetMove;
    [SerializeField] float maxHealthPoints = 100f;

    NavMeshAgent agent;
    //Animator animator;

    float currentHealthPoints = 100f;
    public float healthAsPercentage
    { get { return currentHealthPoints / maxHealthPoints; } }

    public void Deselect()
    {
        GetComponentInChildren<Canvas>().enabled = false;
    }

    public void Select()
    {
        
        PlayerUnits.selectedUnits.Add(gameObject);
        GetComponentInChildren<Canvas>().enabled = true;
    }

    public void Move(Vector3 point)
    {
        targetMove.transform.position = point;

        //use this if using A*
        //astarPath.updatePosition();

        //use this if using unity navigation
        agent.destination = targetMove.transform.position;
        //Debug.Log("Target Move position is at " + point);

    }

    void Start()
    {
        PlayerUnits.playerUnits.Add(gameObject);
        agent = GetComponent<NavMeshAgent>();
        print("int");
        //animator = this.transform.Find("toonRTS").GetComponent<Animator>();
    }
}
