using System.Collections;
using UnityEngine;

public class Patrullaje_WP : MonoBehaviour
{
    [SerializeField] private Transform[] waypoints;
    private int currentWaypoint = 0;
    [SerializeField] private float speed;
    [SerializeField] private float waitTime;
    private bool isWaiting;

    void Update()
    {
        Vector2 currentPosition = transform.position;
        Vector2 targetPosition = waypoints[currentWaypoint].position;

        if (currentPosition != targetPosition)
        {
            transform.position = Vector2.MoveTowards(currentPosition, targetPosition, speed * Time.deltaTime);
            
        }
        else if (!isWaiting)
        {
            StartCoroutine(Wait());
        }
    }

    IEnumerator Wait()
    {
        isWaiting = true;
        yield return new WaitForSeconds(waitTime);
        currentWaypoint++;
        if (currentWaypoint == waypoints.Length)
        {
            currentWaypoint = 0;
        }
        isWaiting = false;

        Flip(); 
    }

    private void Flip()
    {
        if (transform.position.x > waypoints[currentWaypoint].position.x)
        {
            transform.rotation = Quaternion.Euler(0f, -180f, 0f);
        }
        else
        {
            transform.rotation = Quaternion.Euler(0f, 0f, 0f);
        }
    }
}
