using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TurretCollection : MonoBehaviour
{
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
        TurretOne,
        TurretTwo
    }

    public GameObject GetTurretToBuy(TurretNum turret)
    {
        GameObject boughtTurret = null;

        switch (turret)
        {
            case TurretNum.TurretOne:
                if (_turretOne.GetComponentInChildren<Turret>().Cost > PlayerStats.Instance.Gold)
                    return null;

                boughtTurret = _turretOne;
                break;
            case TurretNum.TurretTwo:
                boughtTurret = _turretTwo;
                break;
            default:
                break;
        }

        return boughtTurret;
    }
    
}
