using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Freeze : Turret
{
    // Start is called before the first frame update
    protected override void Start()
    {
        base.Start();
        timer = new Timer(fireRate);
    }

    private void Freezey()
    {

    }
    
}
