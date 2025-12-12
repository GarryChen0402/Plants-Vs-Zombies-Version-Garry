using UnityEngine;
using UnityEngine.UI;

public class CardPeashooter : CardTemplate
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
