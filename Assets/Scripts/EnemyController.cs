/**
 * Summary 9.11.2021
 * 
 * EnemyController.cs is a enemy controller for all kind of enemies.
 * 
 *
 **/

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyController : MonoBehaviour
{
    public GameManager gamecontroller;

    public Transform target;//set target from inspector instead of looking in Update
    public float distance;
    public float agroRange;
    public float speed;

    private bool facingLeft = true;

    private Rigidbody2D rigidbody2D;

    void Awake()
    {
        rigidbody2D = GetComponent<Rigidbody2D>();
        gamecontroller = GameObject.Find("GameManager").GetComponent<GameManager>();
        target = GameObject.Find("Player").GetComponent<Transform>();
    }

    void Flip()
    {
        //===========FLIP THE MODEL===========
        facingLeft = !facingLeft;
        Vector3 Scaler = transform.localScale;
        Scaler.x = Scaler.x * -1;
        transform.localScale = Scaler;
    }

    // Update is called once per frame
    void Update()
    {
        if (Vector3.Distance(transform.position, target.position) < agroRange) //Agro range
        {  //rotate to look at the player
            transform.LookAt(target.position);
            transform.Rotate(new Vector3(0, -90, 0), Space.Self);//correcting the original rotation
        }

        if (Vector3.Distance(transform.position, target.position) < agroRange) //Agro range
        {   //move towards the player

            if (Vector3.Distance(transform.position, target.position) > distance)
            {//move if distance from target is greater than distance
                transform.Translate(new Vector3(speed * Time.deltaTime, 0, 0));
            }
        }
    }
}
