using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CardSunflower : CardTemplate
{
    private void Awake()
    {
        cardState = CardState.Loading;
        plantCost = plantPrefab.GetComponent<Plant>().cost;
        plantCD = 5f;
        cardColor = transform.Find("plant_color")?.gameObject;
        cardGray = transform.Find("plant_gray")?.gameObject;
        cardMask = transform.Find("plant_mask")?.gameObject;
    }
}
