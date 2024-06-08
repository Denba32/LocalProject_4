using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class JumpPad : MonoBehaviour
{
    public float jumpPower = 20f;
    protected void OnCollisionEnter(Collision collision)
    {
        if(collision != null)
        {
            Rigidbody rigid = collision.gameObject.GetComponent<Rigidbody>();
            if(rigid != null)
            {
                rigid.AddForce(Vector3.up * jumpPower, ForceMode.Impulse);
            }
        }
    }
}
