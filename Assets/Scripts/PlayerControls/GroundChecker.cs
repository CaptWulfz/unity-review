using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GroundChecker : MonoBehaviour
{
    public bool isObjectOnGround = true;

    

    public bool IsOnGround
    {
        get { return this.isObjectOnGround; }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == GroundCheckTag.PLATFORM)
            isObjectOnGround = true;

        Debug.Log("The Player is on the ground");

    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.tag == GroundCheckTag.PLATFORM)
            isObjectOnGround = false;

        Debug.Log("The Player is jumping");

    }

    public class GroundCheckTag
    {
        public const string PLATFORM = "Platform";
    }

}
