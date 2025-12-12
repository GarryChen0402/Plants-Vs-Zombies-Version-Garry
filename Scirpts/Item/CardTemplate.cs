using UnityEngine;
using UnityEngine.UI;

public enum CardState
{
    Loading,
    NoSunPoint,
    Ready
}

public class CardTemplate : MonoBehaviour
{
    public GameObject plantPrefab;

    public float plantCD;
    public float plantTimer;

    public int plantCost;


    protected CardState cardState;

    public GameObject cardColor;
    public GameObject cardGray;
    public GameObject cardMask;
    //On

    private void Awake()
    {
        cardState = CardState.Loading;
        plantCost = plantPrefab.GetComponent<Plant>().cost;

        cardColor = transform.Find("plant_color")?.gameObject;
        cardGray = transform.Find("plant_gray")?.gameObject;
        cardMask = transform.Find("plant_mask")?.gameObject;
    }

    private void Start()
    {
        SwitchToLoading();
    }

    private void Update()
    {
        switch (cardState)
        {
            case CardState.Loading:
                LoadingUpdate();
                break;
            case CardState.NoSunPoint:
                NoSunPointUpdate();
                break;
            case CardState.Ready:
                ReadyUpdate();
                break;
        }
    }

    private void LoadingUpdate()
    {
        plantTimer += Time.deltaTime;
        cardMask.GetComponent<Image>().fillAmount = (plantCD - plantTimer) / plantCD;
        if(plantTimer >= plantCD)
        {
            plantTimer = 0f;
            SwitchToNoSunPoint();
        }
    }

    private void NoSunPointUpdate()
    {
        if (SunManager.Instance.SunPoint >= plantCost) SwitchToReady();
    }

    private void ReadyUpdate()
    {
        if (SunManager.Instance.SunPoint < plantCost) SwitchToNoSunPoint();
    }


    private void SwitchToLoading()
    {
        cardState = CardState.Loading;
        cardColor.GetComponent<Image>().enabled = false;
        cardGray.GetComponent<Image>().enabled = true;
        cardMask.GetComponent <Image>().enabled = true;
        plantTimer = 0;
        //Debug.Log("Loading card");
    }

    private void SwitchToNoSunPoint()
    {
        cardState = CardState.NoSunPoint;
        cardColor.GetComponent<Image>().enabled = false;
        cardGray.GetComponent<Image>().enabled = true;
        cardMask.GetComponent<Image>().enabled = false;
        //Debug.Log("NoSunPoint card");
    }

    private void SwitchToReady()
    {
        cardState = CardState.Ready;
        cardColor.GetComponent<Image>().enabled = true;
        cardGray.GetComponent<Image>().enabled = false;
        cardMask.GetComponent <Image>().enabled = false;
        //Debug.Log("Ready card");
    }


    public void OnClicked()
    {
        //Debug.Log("You have clicked the card of " + plantPrefab.name.ToString() + plantCost.ToString());
        if (cardState == CardState.Ready)
        {
            GameObject go = GameObject.Instantiate(plantPrefab);
            go.GetComponent<Animator>().enabled = false;
            AudioManager.Instance?.PlayFx("ButtonClicked");
            HandManager.Instance.HoldPlant(gameObject, go);
        }
        //Only for test
        //if (cardState == CardState.Ready)
        //{
        //    SwitchToLoading();
        //    SunManager.Instance.SunPoint -= plantCost;
        //}
    }

    public void BePlanted()
    {
        AudioManager.Instance?.PlayFx("BePlanted");
        SwitchToLoading();
        SunManager.Instance.SunPoint -= plantCost;
    }
}
