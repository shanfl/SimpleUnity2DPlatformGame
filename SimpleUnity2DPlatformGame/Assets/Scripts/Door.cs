using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Door : MonoBehaviour
{
    BoxCollider2D coll;
    Animator anim;
    // Start is called before the first frame update
    void Start()
    {
        anim = GetComponent<Animator>();
        coll = GetComponent<BoxCollider2D>();
        coll.enabled = false;
        //anim.Play("open");
    }

    
    public void OpenDoor()
    {
        Debug.Log("OpenDoor");
        //if (coll.enabled == true) return;
        
        anim.Play("open");
        coll.enabled = true;
    }

    private void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            // next level
            Debug.Log("Door-OnTriggerStay2D ");
            GameManager.instance.NextLevel();
        }
    }
}
