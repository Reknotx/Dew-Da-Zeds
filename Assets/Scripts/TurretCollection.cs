using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TurretCollection : MonoBehaviour
{
    [SerializeField] private GameObject _barrier;
    [SerializeField] private GameObject _turretOne;
    [SerializeField] private GameObject _turretTwo;
    [SerializeField] private GameObject _turretThree;

    public static TurretCollection Instance;

    private void Start()
    {
        if (Instance != null && Instance !=  this)
        {
            Destroy(Instance.gameObject);
        }

        Instance = this;
    }

    public enum TurretNum
    {
        Barrier,
        TurretOne,
        TurretTwo,
        TurretThree
    }

    public GameObject GetTurretToBuy(TurretNum turret)
    {
        GameObject boughtTurret = null;

        switch (turret)
        {
            case TurretNum.Barrier:
                if (_barrier.GetComponent<Barrier>().Cost > PlayerStats.Instance.Gold)
                    return null;

                boughtTurret = _barrier;
                break;
            
            case TurretNum.TurretOne:
                if (_turretOne.GetComponentInChildren<Turret>().Cost > PlayerStats.Instance.Gold)
                    return null;

                boughtTurret = _turretOne;
                break;
            case TurretNum.TurretTwo:
                if (_turretTwo.GetComponentInChildren<Turret>().Cost > PlayerStats.Instance.Gold)
                    return null;

                boughtTurret = _turretTwo;
                break;

            case TurretNum.TurretThree:
                if (_turretThree.GetComponentInChildren<Turret>().Cost > PlayerStats.Instance.Gold)
                    return null;

                boughtTurret = _turretThree;
                break;
            default:
                break;
        }

        return boughtTurret;
    }

    public int GetTurretCost(int index)
    {
        int cost = 0;
        switch (index)
        {
            case 0:
                cost = _barrier.GetComponent<Barrier>().Cost;
                break;

            case 1:
                cost = _turretOne.GetComponentInChildren<Turret>().Cost;
                break;

            case 2:
                cost = _turretTwo.GetComponentInChildren<Turret>().Cost;
                break;
        }

        return cost;
    }
    
}
