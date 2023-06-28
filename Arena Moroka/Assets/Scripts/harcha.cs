using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class harcha : MonoBehaviour
{

    private PlayerController player;
    private LizzardController liz;
    // Start is called before the first frame update
    private void Start()
    {
        liz = FindObjectOfType<LizzardController>();
    }
    private void OnCollisionEnter(Collision collision)
    {
        Debug.Log("Collision occurred");
        if (collision.gameObject.CompareTag("Player"))
        {
            player = collision.gameObject.GetComponent<PlayerController>();
            player.TakeDamage(liz.damage);
            Destroy(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }   
    }
}
