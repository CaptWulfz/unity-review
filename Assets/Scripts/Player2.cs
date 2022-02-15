using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player2 : Entity
{
    // Start is called before the first frame update
    protected override void Start()
    {
        this.speed = 4f;
        base.Start();
    }

    // Update is called once per frame
    protected override void Update()
    {
        base.Update();
    }
}
