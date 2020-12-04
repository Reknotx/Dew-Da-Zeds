using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class BuyTurret : MonoBehaviour
{
    public GameObject focusOfBuy;

    public Button placeButton, cancelButton;

    [HideInInspector] public bool placing = false;

    private bool _canPlace = true;
    [HideInInspector] public bool CanPlace
    {
        get
        {
            return _canPlace;
        }

        set
        {
            if (value == true)
            {
                placeButton.interactable = true;
            }
            else
            {
                placeButton.interactable = false;
            }
            _canPlace = value;
        }
    }

    public List<Button> shopButtons = new List<Button>();

    public GameObject barrier;

    public static BuyTurret Instance;

    private void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(Instance.gameObject);
        }

        Instance = this;
    }

    private void Update()
    {
        UpdateButtons();

        if (placing == false) return;

        if (Input.touchCount > 0 && focusOfBuy != null)
        {
            if (!IsPointerOverUIObject())
            {
                Touch fingerPos = Input.GetTouch(0);
                Vector3 pos = Camera.main.ScreenToWorldPoint(fingerPos.position);
                focusOfBuy.transform.position = new Vector3(pos.x, pos.y, 0f);
            }
        }
        else if (Input.GetMouseButton(0) && focusOfBuy != null)
        {
            if (!IsPointerOverUIObject())
            {
                Vector3 mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
                mousePos.z = 0f;
                focusOfBuy.transform.position = new Vector3(mousePos.x, mousePos.y, 0f);
            }
        }
    }

    public void PlaceTurret()
    {
        placing = false;

        if (focusOfBuy.GetComponentInChildren<Turret>())
        {
            focusOfBuy.GetComponentInChildren<Turret>().enabled = true;
            PlayerStats.Instance.Gold -= focusOfBuy.GetComponentInChildren<Turret>().Cost;
            focusOfBuy.GetComponentInChildren<Turret>().openUpgradeMenuCanvas.SetActive(true);
        }
        else if (focusOfBuy.GetComponent<Barrier>())
        {
            focusOfBuy.GetComponent<BoxCollider2D>().enabled = true;
            PlayerStats.Instance.Gold -= focusOfBuy.GetComponent<Barrier>().Cost;
            focusOfBuy.GetComponent<Barrier>().enabled = true;
        }

        placeButton.gameObject.SetActive(false);
        cancelButton.gameObject.SetActive(false);

        focusOfBuy = null;
    }

    public void CancelBuy()
    {
        placing = false;

        //PlayerStats.Instance.Gold += focusOfBuy.GetComponentInChildren<Turret>().Cost;
        Destroy(focusOfBuy.gameObject);
        focusOfBuy = null;

        placeButton.gameObject.SetActive(false);
        cancelButton.gameObject.SetActive(false);
    }

    public void UpdateButtons()
    {
        //Buttons
        for (int index = 0; index < shopButtons.Count; index++)
        {
            Button shopButton = shopButtons[index];

            if (TurretCollection.Instance.GetTurretCost(index) > PlayerStats.Instance.Gold)
            {
                shopButton.interactable = false;
            }
        }
    }

    public void BuyBarrier()
    {
        if (focusOfBuy != null)
        {
            Destroy(focusOfBuy.gameObject);
        }

        focusOfBuy = Instantiate(TurretCollection.Instance.GetTurretToBuy(TurretCollection.TurretNum.Barrier),
                                 Vector3.zero,
                                 Quaternion.identity);

        focusOfBuy.GetComponent<Barrier>().enabled = false;

        placing = true;

        placeButton.gameObject.SetActive(true);
        placeButton.interactable = true;
        cancelButton.gameObject.SetActive(true);
        cancelButton.interactable = true;
    }

    private void BuyTurrets(GameObject turret)
    {
        if (focusOfBuy != null)
        {
            Destroy(focusOfBuy.gameObject);
        }

        focusOfBuy = Instantiate(turret, Vector3.zero, Quaternion.identity) as GameObject;
        focusOfBuy.GetComponentInChildren<Turret>().openUpgradeMenuCanvas.SetActive(false);
        focusOfBuy.GetComponentInChildren<Turret>().enabled = false;

        placing = true;

        placeButton.gameObject.SetActive(true);
        placeButton.interactable = true;
        cancelButton.gameObject.SetActive(true);
        cancelButton.interactable = true;
    }

    public void BuyTurretOne()
    {
        GameObject turret = TurretCollection.Instance.GetTurretToBuy(TurretCollection.TurretNum.TurretOne);

        if (turret != null) BuyTurrets(turret);
    }

    public void BuyTurretTwo()
    {
        GameObject turret = TurretCollection.Instance.GetTurretToBuy(TurretCollection.TurretNum.TurretTwo);

        if (turret != null) BuyTurrets(turret);
    }


    private bool IsPointerOverUIObject()
    {
        PointerEventData eventDataCurrentPosition = new PointerEventData(EventSystem.current);
        if (Input.touchCount > 0)
        {
            eventDataCurrentPosition.position = new Vector2(Input.GetTouch(0).position.x, Input.GetTouch(0).position.y);
        }
        else if (Input.GetMouseButton(0))
        {
            eventDataCurrentPosition.position = new Vector2(Input.mousePosition.x, Input.mousePosition.y);
        }
        List<RaycastResult> results = new List<RaycastResult>();
        EventSystem.current.RaycastAll(eventDataCurrentPosition, results);

        List<RaycastResult> filteredResults = new List<RaycastResult>();

        foreach (RaycastResult result in results)
        {
            if (result.gameObject.layer == 5)
            {
                filteredResults.Add(result);
                //print("Name: " + result.gameObject.name + "\nLayer: " + result.gameObject.layer);
            }
        }

        return filteredResults.Count > 0;
        //return results.Count > 0;
    }
}
