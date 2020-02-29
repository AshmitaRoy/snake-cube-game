using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SnakeHead : MonoBehaviour {

    public SnakeMove movement;
    public SpawnObjects spawnfood;
	void OnCollisionEnter(Collision col)
    {
        if(col.gameObject.tag == "Food")
        {
            movement.AddBodyPart();
            Destroy(col.gameObject);

            //spawning food
            spawnfood.SpawnFood();

        }

        else
        {
            // not colliding with second body part and also alive
            if(col.transform != movement.BodyParts[1] && movement.IsAlive)
            { 
                if (Time.time - movement.TimeFromLastTry > 5)
                    movement.GameOver();
            }
        }
    }
}
