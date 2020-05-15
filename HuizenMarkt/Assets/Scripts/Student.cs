using System;
using UnityEngine;

public class Student : MonoBehaviour
{
	#region Enums
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
    #endregion

    #region Variables
    public int currentMoney;
    public int income;
    public float happiness = 100;

    [SerializeField] public Animator animator;
    [SerializeField] public Room appartment;

    [SerializeField] public float StudentSpeed { get; set; }
    private WaypointHandler waypoints;

    float studentActivityTimer;
    #endregion

    #region Methodes
    private void Start()
    {
        StudentSpeed = 1f;
        foreach (Transform child in appartment.transform)
            if (child.name == "Waypoints")
                waypoints = child.GetComponent<WaypointHandler>();     
    }

    private void Update()
    {
        if(studentState == StudentState.DoingActivity)
            studentActivityTimer += Time.deltaTime;
        SwitchStudentActivity();
        Movement();
        ItSmellsLikeBrokeInHere();


    }

    public void ItSmellsLikeBrokeInHere()
    {
        if (appartment.rent < currentMoney)
            happiness = 100;
        if (appartment.rent > currentMoney/2)
            happiness = 50;
        if (appartment.rent > currentMoney)
            happiness = 0;
    }



    private void SwitchStudentActivity()
    {
        if(studentActivityTimer >= 10)
        {
            studentActivity = ChooseActivity();
            studentActivityTimer = 0;
        }        
    }

    private void Movement()
    {
        if (studentState == StudentState.Idle)
        {
            studentActivity = ChooseActivity();
        }
        if (studentState == StudentState.Idle || studentState == StudentState.DoingActivity)
            animator.SetBool("Moving", false);

        if (studentActivity != StudentActivity.Nothing && CalculateMovement(GetPosition()) != Vector2.zero)
        {
            MoveTo(GetPosition());
            studentState = StudentState.Walking;
            animator.SetBool("Moving", true);
        }
        if (studentActivity != StudentActivity.Nothing && CalculateMovement(GetPosition()) == Vector2.zero)
            studentState = StudentState.DoingActivity;
    }

    private StudentActivity ChooseActivity()
    {
        int rand = new System.Random().Next(0, 3);
        switch (rand)
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

    public void MoveTo(Vector2 _waypoint)
    {
        this.transform.Translate(CalculateMovement(_waypoint) * Time.deltaTime, Space.World);
        if (Math.Abs(GetDistance(_waypoint).x) < 0.01f && Math.Abs(GetDistance(_waypoint).y) < 0.01f)
            transform.position = _waypoint;
            
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

        animator.SetFloat("HorizontalSpeed", movement.x);

        return movement;
    }

    private Vector2 GetDistance(Vector2 _waypoint)
    {
        return _waypoint - new Vector2(this.transform.position.x, this.transform.position.y);
    }

    public bool PayRent()
    {
        if (currentMoney - appartment.rent >= 0)
        {
            currentMoney -= appartment.rent;
            return true;
        }
        else 
        {
            FileForBankruptcy();
            return false;
        }   
    }

    public void FileForBankruptcy()
    {
        happiness = 0;
    }


    #endregion
}
