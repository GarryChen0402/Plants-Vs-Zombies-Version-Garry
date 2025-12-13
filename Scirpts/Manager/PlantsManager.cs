using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlantsManager : MonoBehaviour
{
    public static PlantsManager Instance { get; private set; }




    public CellList cellMatrix;


    private void Awake()
    {
        if (Instance == null) Instance = this;
        else Destroy(gameObject);
    }

    //private void Update()
    //{
    //    //DisableAttack();
    //}
    public void DisableAttack()
    {
        if(cellMatrix == null)
        {
            Debug.Log("Cell matrix is null..");
            return;
        }
        for (int i = 0; i < cellMatrix.rows; i++) DisableAttack(i);

    }

    public void DisableAttack(int idx)
    {
        if(idx < 0 || idx >= cellMatrix.rows)
        {
            Debug.Log("out of index...");
            return;
        }

        for (int i = 0; i < cellMatrix.cellRows[idx].cells.Count; i++)
        {
            if (cellMatrix.cellRows[idx].cells[i].currentPlant != null)
            {
                Plant curPlant = cellMatrix.cellRows[idx].cells[i].currentPlant.GetComponent<Plant>() ;
                if (curPlant.GetType().IsSubclassOf(typeof(AttackPlant)))
                {
                    AttackPlant atkPlant = (AttackPlant) curPlant;
                    atkPlant.canAttack = false;
                }
            }
        }
    }

    public void EnableAttack(int idx)
    {
        if (idx < 0 || idx >= cellMatrix.rows)
        {
            Debug.Log("out of index...");
            return;
        }

        for (int i = 0; i < cellMatrix.cellRows[idx].cells.Count; i++)
        {
            if (cellMatrix.cellRows[idx].cells[i].currentPlant != null)
            {
                Plant curPlant = cellMatrix.cellRows[idx].cells[i].currentPlant.GetComponent<Plant>();
                if (curPlant.GetType().IsSubclassOf(typeof(AttackPlant)))
                {
                    AttackPlant atkPlant = (AttackPlant)curPlant;
                    atkPlant.canAttack = true;
                }
            }
        }
    }

    public void EnableAttack()
    {
        if (cellMatrix == null)
        {
            Debug.Log("Cell matrix is null..");
            return;
        }
        for (int i = 0; i < cellMatrix.rows; i++) EnableAttack(i);
    }

    public void DisableGenerate()
    {
        if (cellMatrix == null)
        {
            Debug.Log("Cell matrix is null..");
            return;
        }
        for (int i = 0; i < cellMatrix.rows; i++) DisableGenerate(i);
    }

    public void DisableGenerate(int idx)
    {
        if (idx < 0 || idx >= cellMatrix.rows)
        {
            Debug.Log("out of index...");
            return;
        }

        for (int i = 0; i < cellMatrix.cellRows[idx].cells.Count; i++)
        {
            if (cellMatrix.cellRows[idx].cells[i].currentPlant != null)
            {
                Plant curPlant = cellMatrix.cellRows[idx].cells[i].currentPlant.GetComponent<Plant>();
                if (curPlant.GetType().IsSubclassOf(typeof(ProductPlant)))
                {
                    ProductPlant productPlant= (ProductPlant)curPlant;
                    productPlant.CanProduce = false;
                }
            }
        }
    }

    public void EnableGenerate()
    {
        if (cellMatrix == null)
        {
            Debug.Log("Cell matrix is null..");
            return;
        }
        for (int i = 0; i < cellMatrix.rows; i++) EnableGenerate(i);
    }

    public void EnableGenerate(int idx)
    {
        if (idx < 0 || idx >= cellMatrix.rows)
        {
            Debug.Log("out of index...");
            return;
        }

        for (int i = 0; i < cellMatrix.cellRows[idx].cells.Count; i++)
        {
            if (cellMatrix.cellRows[idx].cells[i].currentPlant != null)
            {
                Plant curPlant = cellMatrix.cellRows[idx].cells[i].currentPlant.GetComponent<Plant>();
                if (curPlant.GetType().IsSubclassOf(typeof(ProductPlant)))
                {
                    ProductPlant productPlant = (ProductPlant)curPlant;
                    productPlant.CanProduce = true;
                }
            }
        }
    }

    public void DisableAllPlants()
    {
        DisableAttack();
        DisableGenerate();
    }

    public void EnableAllPlants()
    {
        EnableAttack();
        EnableGenerate();
    }
}
