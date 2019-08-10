using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Building : MonoBehaviour
{
    [SerializeField] public GameObject spawnables;
    [SerializeField] public GameObject SpawnPoint;
    [SerializeField] float maxHealthPoints = 100f;
    float currentHealthPoints = 100f;
    public float healthAsPercentage
    { get { return currentHealthPoints / maxHealthPoints; } }

    public void Deselect()
    {
        GetComponentInChildren<Canvas>().enabled = false;
        //TODO turn off ui
    }

    public void Select()
    {

        //TODO turn on ui
        GetComponentInChildren<Canvas>().enabled = true;
    }
    public void spaw()
    {

    }

    private void Update()
    {
        if (Input.GetKeyUp(KeyCode.Y))
        {
            Instantiate(spawnables, SpawnPoint.transform);
        }
    }
}
