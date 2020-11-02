using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BuyTurret : MonoBehaviour
{
    public GameObject turretOne;

    public GameObject focusOfBuy;


    [HideInInspector] public bool placing = false;

    public List<Button> shopButtons = new List<Button>();

    private void Update()
    {
        if (PlayerStats.Instance.Gold < turretOne.GetComponentInChildren<Turret>().Cost)
        {
            shopButtons[0].interactable = false;
            //return;
        }
        else
        {
            shopButtons[0].interactable = true;
        }

        if (placing && (Input.GetMouseButton(0) || Input.touchCount > 0))
        { 
            if (Input.touchCount > 0)
            {
                Touch fingerPos = Input.GetTouch(0);
                Vector3 pos = Camera.main.ScreenToWorldPoint(fingerPos.position);
                focusOfBuy.transform.position = new Vector3(pos.x, pos.y, 0f);
            }

            //Vector3 pos = Camera.main.ScreenToWorldPoint(Input.mousePosition);

            //focusOfBuy.transform.position = new Vector3(pos.x, pos.y, 0f);

            if (Input.GetTouch(0).phase == TouchPhase.Ended)
            {

                placing = false;
                focusOfBuy.GetComponentInChildren<Firing>().enabled = true;
                focusOfBuy = null;
            }
            //if (Input.GetMouseButtonDown(0))
            //{
            //    placing = false;
            //    focusOfBuy.GetComponentInChildren<Firing>().enabled = true;
            //    focusOfBuy = null;
            //}
        }
        else
        {
            return;
        }


    }

    public void UpdateButtons()
    {
       //Buttons
    }

    private void BuyTurrets(GameObject turret)
    {
        focusOfBuy = Instantiate(turret, Vector3.zero, Quaternion.identity) as GameObject;
        focusOfBuy.GetComponentInChildren<Firing>().enabled = false;
        PlayerStats.Instance.Gold -= focusOfBuy.GetComponentInChildren<Turret>().Cost;

        placing = true;
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
}
