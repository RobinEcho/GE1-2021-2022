using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class AITank : MonoBehaviour
{

    public float radius = 10;
    public int numWaypoints = 5;
    public int current = 0;
    public List<Vector3> waypoints = new List<Vector3>();
    public float speed = 10;
    public Transform player;


    Vector3 targetDir;
    public void OnDrawGizmos()
    {
        if (!Application.isPlaying)
        {
            // Task 1
            // Put code here to draw the gizmos
            // Use sin and cos to calculate the positions of the waypoints 
            // You can draw gizmos using
            // Gizmos.color = Color.green;
            // Gizmos.DrawWireSphere(pos, 1);
            float theta = (Mathf.PI * 2.0f) / numWaypoints;
            for (int i = 0; i < numWaypoints; i++)
            {
                float angle = theta * i;
                Vector3 pos = new Vector3(Mathf.Sin(angle) * radius, 0, Mathf.Cos(angle) * radius);
                pos = transform.TransformPoint(pos);
                Gizmos.color = Color.red;
                Gizmos.DrawWireSphere(pos, 1);
            }
        }
    }

    // Use this for initialization
    void Awake()
    {

        // Task2
        // Write code here to populate waypoints List
        // You can use for loop, sin, cod and transform.TransformPoint
        float theta = (Mathf.PI * 2.0f) / numWaypoints;
        for (int i = 0; i < numWaypoints; i++)
        {
            float angle = theta * i;
            Vector3 pos = new Vector3(Mathf.Sin(angle) * radius, 0, Mathf.Cos(angle) * radius);
            pos = transform.TransformPoint(pos);
            waypoints.Add(pos);
        }
    }

    // Update is called once per frame
    void Update()
    {
        // Task 3
        // Put code here to move the tank towards the next waypoint
        // When the tank reaches a waypoint you should advance to the next one


        // Use three different way to rotate tank: Rotate(), LookAt(), forward
        //Rotate();
        transform.position = Vector3.MoveTowards(transform.position, waypoints[current], speed * Time.deltaTime);

        if (Vector3.Distance(transform.position, waypoints[current]) <= 0.1f)
        {
            current++;
            if (current >= waypoints.Count)
            {
                //this.enabled = false;
                current = 0;
                return;
            }
            targetDir = waypoints[current] - transform.position;
        }

        transform.forward = Vector3.Lerp(transform.forward, targetDir, speed * Time.deltaTime);
        // transform.LooAt(waypoints[current]);

             
        // Task 4
        // Put code here to check if the player is in front of or behine the tank

        Vector3 toTarget = player.position - transform.position;
        toTarget = Vector3.Normalize(toTarget);

        float dot = Vector3.Dot(transform.forward, toTarget);
        if (dot > 0)
        {
            GameManager.Log("Player Infront");
        }
        else
        {
            GameManager.Log("Player at back");
        }





        // Task 5
        // Put code here to calculate if the player is inside the field of view and in range
        // You can print stuff to the screen using:
        float theta = Mathf.Acos(dot) * Mathf.Rad2Deg;
        if (theta < 45)
        {
            GameManager.Log("Hello from th AI tank");
        }
        else
        {
            GameManager.Log("Seeking Player");
        }
        
    }

    /*
    void Rotate()
    {
        Vector3 dir = waypoint[current] - transform.position;
        float angle = Vector3.Angle(transform.forward,dir);
        
        angle = Mathf.Min(angle, speed);
        
        transform.Rotate(Vector3.Cross(transform.forward,dir) * angle)
    }     
    */
}
