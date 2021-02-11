using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HitPoint : MonoBehaviour
{
    public void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            Debug.Log("Player Get hurt!!!!!!!!!!!!!!!");
            other.GetComponent<IDamageable>().GetHit(1);
        }

        if (other.CompareTag("Bomb"))
        {
            Debug.Log("bomb get skill-action-hurt");
        }
    }
}
