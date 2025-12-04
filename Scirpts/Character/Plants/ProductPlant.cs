using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProductPlant : Plant
{
    // Start is called before the first frame update
    public GameObject productResourcePrefab;

    [SerializeField]
    protected float productCD;
    protected float productTimer;
    [SerializeField]
    protected bool canProduce;
    //[SerializeField]
    //protected float attackDamange;

    private void Update()
    {
        if (canProduce) ProductResourceUpdate();
    }

    protected virtual void ProductResourceUpdate()
    {
        Debug.Log("This is the parent class of ProductPlant , in  function Named ProductResourceUpdate, please override the virtual function in subclass");
    }
}
