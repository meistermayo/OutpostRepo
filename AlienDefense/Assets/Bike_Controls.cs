using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bike_Controls : FPS_Controls {
    protected float dirAngle;
    [Header ("Bike Specific Things")]
    [SerializeField] protected float turnSpeed;
    [SerializeField] protected float driftTurnSpeed;
    [SerializeField] protected float maxSpeed;
    [SerializeField] GameObject bike;
    bool lastSprint;
    protected override void Start()
    {
        base.Start();
        dirAngle = transform.rotation.y+90f;
    }
    protected override void Jump()
    {
        // Do nothing
    }

    protected override void Walk()
    {
        float _turnSpeed = sprintHeld ? driftTurnSpeed : turnSpeed;
        dirAngle -= h * _turnSpeed;


        if (sprintHeld)
        {
            Camera.main.transform.localRotation = Quaternion.Lerp(Camera.main.transform.localRotation, Quaternion.Euler(new Vector3(Camera.main.transform.localRotation.x, Camera.main.transform.localRotation.y, -35f * h)), 0.1f);
        }
        else
        {
            Vector3 bikeForwardVec = new Vector3(Mathf.Cos(Mathf.Deg2Rad * dirAngle), 0f, Mathf.Sin(Mathf.Deg2Rad * dirAngle));
            if (lastSprint)
            {
                body.velocity = maxSpeed * 1f * bikeForwardVec;
                Camera.main.transform.localRotation = /*Quaternion.Lerp(Camera.main.transform.localRotation, */Quaternion.Euler(new Vector3(Camera.main.transform.localRotation.x, Camera.main.transform.localRotation.y, 0f));//, 0.1f);
            }
            // if (CheckGrounded()) // why doesnt this work
            {
                body.velocity += accSpeed * v * bikeForwardVec;
                float y = body.velocity.y;
                body.velocity = Quaternion.Inverse(Quaternion.Euler(Vector3.up * -h * _turnSpeed)) * body.velocity;
                body.velocity -= Vector3.up * y;
                body.velocity = Vector3.ClampMagnitude(body.velocity, maxSpeed);
                body.velocity += Vector3.up * y;
            }
        }

        bike.transform.rotation *= Quaternion.Euler(Vector3.up * (h * _turnSpeed));

        lastSprint = sprintHeld;
    }

    protected override void Update()
    {
        base.Update();
        bike.transform.rotation = Quaternion.Euler(Vector3.up * -(dirAngle-90f) + Vector3.right*90f);
    }
}
