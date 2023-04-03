using UnityEngine;

public class Bullet : MonoBehaviour

{
    public float speed;

    void Start()
    {

    }

    void FixedUpdate()
    {
        if (Input.GetAxis("Horizontal") > 0){
            transform.position = transform.position + new Vector3(speed * Time.deltaTime, 0, 0);
        } 
        else 
        {
            transform.position = transform.position - new Vector3(speed * Time.deltaTime, 0, 0);
        };
        
    }
}