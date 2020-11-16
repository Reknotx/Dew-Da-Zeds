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

    private bool canPlace = false;

    public List<Button> shopButtons = new List<Button>();

    public GameObject barrier;

    void NewBuy()
    {
        UpdateButtons();

        if (placing && Input.touchCount > 0 && focusOfBuy != null)
        {
            Touch fingerPos = Input.GetTouch(0);
            Vector3 pos = Camera.main.ScreenToWorldPoint(fingerPos.position);
            focusOfBuy.transform.position = new Vector3(pos.x, pos.y, 0f);
        }
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
    }

    public void PlaceTurret()
    {
        placing = false;

        if (focusOfBuy.GetComponentInChildren<Turret>())
        {
            focusOfBuy.GetComponentInChildren<Turret>().enabled = true;
            PlayerStats.Instance.Gold -= focusOfBuy.GetComponentInChildren<Turret>().Cost;
        }
        else if (focusOfBuy.GetComponent<Barrier>())
        {
            focusOfBuy.GetComponent<BoxCollider2D>().enabled = true;
            PlayerStats.Instance.Gold -= focusOfBuy.GetComponent<Barrier>().Cost;
        }

        focusOfBuy = null;
        placeButton.gameObject.SetActive(false);
        cancelButton.gameObject.SetActive(false);
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

        focusOfBuy = Instantiate(barrier, Vector3.zero, Quaternion.identity) as GameObject;
        focusOfBuy.GetComponent<BoxCollider2D>().enabled = false;
    }

    private void BuyTurrets(GameObject turret)
    {
        if (focusOfBuy != null)
        {
            Destroy(focusOfBuy.gameObject);
        }

        focusOfBuy = Instantiate(turret, Vector3.zero, Quaternion.identity) as GameObject;
        focusOfBuy.GetComponentInChildren<Turret>().enabled = false;

        placing = true;

        placeButton.gameObject.SetActive(true);
        cancelButton.gameObject.SetActive(true);
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
        eventDataCurrentPosition.position = new Vector2(Input.GetTouch(0).position.x, Input.GetTouch(0).position.y);
        List<RaycastResult> results = new List<RaycastResult>();
        EventSystem.current.RaycastAll(eventDataCurrentPosition, results);
        return results.Count > 0;
    }
}
