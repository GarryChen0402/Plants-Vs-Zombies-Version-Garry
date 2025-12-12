using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Cell : MonoBehaviour
{
    public int row;
    public int col;
    public GameObject currentPlant;

    //private void OnMouseDown()
    //{
    //    Debug.Log("Mouse down here: "+ gameObject.name.ToString() + " " + row.ToString() + " " + col.ToString());
    //}

    private void OnMouseUp() 
    {
        if (currentPlant != null)
        {
            HandManager.Instance.ReleasePlant();
            return;
        }
        Debug.Log("Mouse up here: "+ gameObject.name.ToString() + " " + row.ToString() + " " + col.ToString());
        HandManager.Instance.PlantPlant(gameObject);
    }
    
}
