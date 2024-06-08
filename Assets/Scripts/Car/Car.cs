using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Car : MonoBehaviour
{
    public Rigidbody rigid;
    public AudioSource audio;
    public float hitPower = 30f;

    private float moveSpeed = 5f;
    private float zVelocity = 0;

    private bool isCollision = false;

    private Coroutine co_Crash;

    private void OnEnable()
    {
        zVelocity = 0;
        moveSpeed = Random.Range(20f, 30f);
        audio.Play();
    }

    private void OnDisable()
    {
        audio.Stop();
    }

    // rigidbody Update·Î
    private void FixedUpdate()
    {
        if (!isCollision)
        {
            transform.Translate(transform.right *  moveSpeed * Time.fixedDeltaTime);
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        if(collision != null)
        {
            if(collision.gameObject.CompareTag("PoolDestroyer"))
            {
                PoolManager.Instance.Push<Car>(this);
            }
            else if(collision.gameObject.CompareTag("Player"))
            {
                Vector3 dir = (collision.transform.position - transform.position).normalized;
                dir.y = 1f;
                Rigidbody rigid = collision.gameObject.GetComponent<Rigidbody>();
                rigid.AddForce(dir * hitPower * 5f, ForceMode.Impulse);

                if (co_Crash != null)
                    StopCoroutine(Crash());
                co_Crash = StartCoroutine(Crash());
            }
        }
    }

    private IEnumerator Crash()
    {
        isCollision = true;
        transform.Translate(Vector3.zero);
        yield return CoroutineHelper.WaitForSeconds(2f);
        isCollision = false;
        yield break;
    }
}
