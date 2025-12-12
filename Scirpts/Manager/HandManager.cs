using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class HandManager : MonoBehaviour
{
    public static HandManager Instance;
    //public HandManager Instance => instance;

    public GameObject currentCard;
    public GameObject currentPlant;

    private void Awake()
    {
        if(Instance == null)
        {
            Instance = this;
        }
        else
        {
            Destroy(gameObject);
        }

            currentCard = null;
        currentPlant = null;
    }

    private void Update()
    {
        if (currentPlant != null) MovePlant();
        if(Input.GetMouseButtonUp(1))ReleasePlant();
    }

    private void MovePlant()
    {
        Vector3 mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        mousePosition.z = 0;
        currentPlant.transform.position = mousePosition;
    }

    public void HoldPlant(GameObject card, GameObject plant)
    {
        currentCard = card;
        currentPlant = plant;  
    }

    public void PlantPlant(GameObject cell)
    {
        if (currentPlant == null) return;
        currentPlant.transform.position = cell.transform.position;
        currentPlant.GetComponent<Animator>().enabled = true;
        currentCard.GetComponent<CardTemplate>().BePlanted();
        currentCard = null;
        currentPlant = null;

    }

    public void ReleasePlant()
    {
        currentCard = null;
        if(currentPlant != null) Destroy(currentPlant);
        currentPlant = null;
    }
}
