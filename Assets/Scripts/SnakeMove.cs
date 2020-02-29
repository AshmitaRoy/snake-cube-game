
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SnakeMove : MonoBehaviour
{
    //to store the bodyparts of the snake
    public List<Transform> BodyParts = new List<Transform>();

    //to ensure that the parts of the snake don't intersect
    public float mindistance = 0.25f;

    //to allow players to restart fresh
    public float TimeFromLastTry;

    public int beginsize;

    public float speed = 1;
    public float rotationspeed = 50;

    //snake's bodyparts
    public GameObject bodyprefab;

    public bool IsAlive;
    public Text CurrentScore;
    public Text GameOverScore;
    public GameObject GameOverScreen;
    private float dis;
    private Transform curBodyPart;
    private Transform PrevBodyPart;

    Rigidbody rb;


    // Use this for initialization
    void Start()
    {
        rb = GetComponent<Rigidbody>();
        StartLevel();
    }

    public void StartLevel()
    {

        //to allow game to begin afresh
        TimeFromLastTry = Time.time;
        GameOverScreen.SetActive(false);

        //delete every body part from list except head
        for(int i= BodyParts.Count - 1; i>1; i--)
        {
            Destroy(BodyParts[i].gameObject);
            BodyParts.Remove(BodyParts[i]);
        }

        //spawn snake
        BodyParts[0].position = new Vector3(0, 0.5f, 0);
        BodyParts[0].rotation = Quaternion.identity;

        //Resetting score and starting game
        CurrentScore.gameObject.SetActive(true);
        CurrentScore.text = "SCORE : 0";
        IsAlive = true;

        //Head exists, add remaining bodyparts
        for (int i=0; i < beginsize - 1; i++)
        {
            AddBodyPart();
        }

        //move to give it snake appearance
        BodyParts[0].position = new Vector3(2, 0.5f, 0);
    }

    // Update is called once per frame
    void Update()
    {
        if(IsAlive)
            Move();

        if (Input.GetKeyDown(KeyCode.Space))
            AddBodyPart();

        /***var vertical = Input.GetAxis("Vertical");
        var horizontal = Input.GetAxis("Horizontal");

        Vector3 velocity = Vector3.zero;

        velocity += (transform.forward * vertical); //Move forward
        velocity += (transform.right * horizontal); //Strafe

        //Adjust speed and frame rate
        velocity *= Speed * Time.fixedDeltaTime;


        velocity.y = rb.velocity.y;
        rb.velocity = velocity;
        ***/
    }

    public void Move()
    {
        float curspeed = speed;

        //if (Input.GetKey(KeyCode.UpArrow))
          //  curspeed *= 2;

        // BodyParts[0].Translate(BodyParts[0].forward * curspeed * Time.smoothDeltaTime, Space.World);
        //smoothDeltaTime to allow lower speeds if needed
        BodyParts[0].Translate(BodyParts[0].forward * curspeed * Time.deltaTime, Space.World);

        //to move the head of the snake
        if (Input.GetAxis("Horizontal") != 0)
            BodyParts[0].Rotate(Vector3.up * rotationspeed * Time.deltaTime * Input.GetAxis("Horizontal"));

        for (int i = 1; i < BodyParts.Count; i++)
        {
            curBodyPart = BodyParts[i];
            PrevBodyPart = BodyParts[i - 1];

            //checking if the bodyparts are too close
            dis = Vector3.Distance(PrevBodyPart.position, curBodyPart.position);

            Vector3 newpos = PrevBodyPart.position;

            //Locking the Y axis to the head's Y position
            newpos.y = BodyParts[0].position.y;

            float T = Time.deltaTime * dis  / mindistance * curspeed;

            //to lesen laggy movement
            if (T > 0.5f)
                T = 0.5f;

            //Bodyparts of the snake move together
            curBodyPart.position = Vector3.Slerp(curBodyPart.position, newpos, T);
            curBodyPart.rotation = Quaternion.Slerp(curBodyPart.rotation, PrevBodyPart.rotation, T);
        }
    }

    //to elongate the snake's body 
    public void AddBodyPart()
    {
        //Instantiate the snake on the last part thus use BodyParts.Count - 1 for position and rotation
        Transform newpart = (Instantiate(bodyprefab, BodyParts[BodyParts.Count - 1].position, BodyParts[BodyParts.Count - 1].rotation) as GameObject).transform;

        newpart.SetParent(transform);

        BodyParts.Add(newpart);

        CurrentScore.text = "SCORE : " + (BodyParts.Count - beginsize).ToString();
    }

    //Player dies, End Score is shown
    public void GameOver()
    {
        IsAlive = false;
        GameOverScore.text = "Your score was : " + (BodyParts.Count - beginsize).ToString();
        CurrentScore.gameObject.SetActive(false);
        GameOverScreen.SetActive(true);

    }
}