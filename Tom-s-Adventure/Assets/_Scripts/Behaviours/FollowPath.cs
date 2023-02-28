using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FollowPath : MyMonoBehaviour
{
    [SerializeField] private List<GameObject> waypoints;
    private int currentWaypointIndex = 0;

    [SerializeField] private float speed = 2f;
    protected override void LoadComponents()
    {
        base.LoadComponents();
        this.LoadWaypoints();
    }
    protected virtual void LoadWaypoints()
    {
        this.waypoints = new List<GameObject>();
        Transform wps = transform.parent.Find("Waypoints");
        for (int i = 0; i < wps.childCount; i++)
        {
            this.waypoints.Add(wps.GetChild(i).gameObject);
        }
    }

    private void FixedUpdate()
    {
        if (Vector2.Distance(waypoints[currentWaypointIndex].transform.position, transform.position) < .1f)
        {
            currentWaypointIndex++;
            if (currentWaypointIndex >= waypoints.Count)
            {
                currentWaypointIndex = 0;
            }
        }
        transform.position = Vector2.MoveTowards(transform.position, waypoints[currentWaypointIndex].transform.position, Time.fixedDeltaTime * speed);
    }
}
