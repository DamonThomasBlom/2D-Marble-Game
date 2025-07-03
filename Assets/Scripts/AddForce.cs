using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AddForce : MonoBehaviour
{
    void OnCollisionEnter2D(Collision2D collision)
    {
        Rigidbody2D rb = GetComponent<Rigidbody2D>();
        Vector2 impactDirection = (transform.position - collision.transform.position).normalized;
        float forceAmount = 5f; // Adjust force as needed

        rb.AddForce(impactDirection * forceAmount, ForceMode2D.Impulse);
    }
}
