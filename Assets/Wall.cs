using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Wall : MonoBehaviour
{
    public GameObject Block;

        private void OnTriggerStay2D(Collider2D other)
        {
            if(other.CompareTag("Block"))
            {
                Instantiate(Block, transform.GetChild(0).position, Quaternion.identity);
                Instantiate(Block, transform.GetChild(1).position, Quaternion.identity);
                Destroy(gameObject);
            }
        }
}
