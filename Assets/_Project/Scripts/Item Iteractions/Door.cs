using UnityEngine;
using System.Collections.Generic;

public class Door : MonoBehaviour
{
    public bool locked = true;
    public bool tpd = false;
    public Transform tpTarget;
    public Door tpDoor;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.GetComponent<CapsuleCollider2D>().tag == "Player")
        {
            PlayerController player = collision.gameObject.GetComponent<PlayerController>();
            if(locked)
            {
                if(player.hasKey())
                {
                    player.loseKey(1);
                    locked = false;
                }
                else{
                    Debug.Log("Player has no key");
                }
            }
            else if(tpd == false && locked == false)
            {
                collision.gameObject.transform.position = tpTarget.position;
                tpDoor.locked = false;
                tpDoor.tpd = true;
            }
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if(collision.GetComponent<CapsuleCollider2D>().tag == "Player")
        {
            tpd = false;
        }
    }
}
