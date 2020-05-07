using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Student : MonoBehaviour
{
    public enum StudentActivity
    {
        Nothing,
        Chilling,
        Sleeping,
        Cooking,
    }

    public enum StudentState
    {
        DoingActivity,
        Idle,
        Walking,
    }

    public StudentActivity studentActivity = StudentActivity.Nothing;
    public StudentState studentState = StudentState.Idle;


    public float StudentSpeed { get; set; }
    [SerializeField] public Room appartment;
    private WaypointHandler waypoints;

    private void Start()
    {
        StudentSpeed = 1f;
        foreach (Transform child in appartment.transform)
            if (child.name == "Waypoints")
                waypoints = child.GetComponent<WaypointHandler>();     
    }

    private void Update()
    {
        if (studentState == StudentState.Idle)
            studentActivity = ChooseActivity();
        if (studentActivity != StudentActivity.Nothing && CalculateMovement(GetPosition()) != Vector2.zero)
        {
            MoveTo(GetPosition());
            studentState = StudentState.Walking;
        }
        if (studentActivity != StudentActivity.Nothing && CalculateMovement(GetPosition()) == Vector2.zero)
            studentState = StudentState.DoingActivity;
            
    }

    private Vector2 GetPosition()
    {
        switch(studentActivity)
        {
            case StudentActivity.Chilling:
                return waypoints.couchWaypoint.transform.position;
            case StudentActivity.Sleeping:
                return waypoints.bedWaypoint.transform.position;
            case StudentActivity.Cooking:
                return waypoints.kitchenWaypoint.transform.position;
            default:
                return Vector2.zero;
        }
    }

    private StudentActivity ChooseActivity()
    {
        int rand = new System.Random().Next(0,2);
        switch(rand)
        {
            case 0:
                return StudentActivity.Chilling;
            case 1:
                return StudentActivity.Cooking;
            case 2:
                return StudentActivity.Sleeping;
            default:
                return StudentActivity.Nothing;
        }
    }

    public void MoveTo(Vector2 _waypoint)
    {
        this.transform.Translate(CalculateMovement(_waypoint) * Time.deltaTime, Space.World);
    }

    private Vector2 CalculateMovement(Vector2 _waypoint)
    {
        Vector2 movement = Vector2.zero;
        Vector2 distance = GetDistance(_waypoint);
        float margin = 0.01f;

        if (Math.Abs(distance.x) < StudentSpeed * Time.deltaTime)
            movement.x = distance.x;
        if (Math.Abs(distance.y) < StudentSpeed * Time.deltaTime)
            movement.y = distance.y;

        if (_waypoint.x > this.transform.position.x)
            movement.x = StudentSpeed;
        if (_waypoint.y > this.transform.position.y)
            movement.y = StudentSpeed;

        if (_waypoint.x < this.transform.position.x)
            movement.x = -StudentSpeed;
        if (_waypoint.y < this.transform.position.y)
            movement.y = -StudentSpeed;

        if (Math.Abs(distance.x) < margin)
            movement.x = 0;
        if (Math.Abs(distance.y) < margin)
            movement.y = 0;
        
        return movement;
    }

    private Vector2 GetDistance(Vector2 _waypoint)
    {
        return _waypoint - new Vector2(this.transform.position.x, this.transform.position.y);
    }
}
