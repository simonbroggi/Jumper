using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Jumper : MonoBehaviour
{
    public Transform feet;
    public Transform trampolin;
    public Transform trampolinCloth;

    [Range(0.1f, 200f)] public float mass = 20f; //kg
    public float drag = 1f;

    public float legsSpeed = 0.1f; // how fast can legsExpanded change
    public float legsExpandedLocalY = -1; // how far can the legs expand
    public float springRate = 30f; // Federkonstante (kg/s2)

    Vector3 gravitationalPull = new Vector3(0f, -9.8f, 0f); // m/s2

    Vector3 velocity = Vector3.zero;
    Vector3 trampolinClothRestPos;
    float legsExpanded = 0f; // between 0 and 1
    float legsContractedLocalY; // initial local y positon of legs

    private void Start()
    {
        trampolinClothRestPos = trampolinCloth.position;
        legsContractedLocalY = feet.localPosition.y;
    }

    // FixedUpdate is called at fixed intervals, possibly several times per frame (check Fixed Timestep in Project Settings / Time).
    // Physics calculations should be done here.
    void FixedUpdate()
    {
        // change feet position according to keyboard input
        if(Input.GetKey(KeyCode.Space) || Input.GetMouseButton(0))
        {
            legsExpanded = Mathf.MoveTowards(legsExpanded, 1f, legsSpeed * Time.deltaTime);
        }
        else
        {
            legsExpanded = Mathf.MoveTowards(legsExpanded, 0f, legsSpeed * Time.deltaTime);
        }
        feet.localPosition = new Vector3(0f, Mathf.Lerp(legsContractedLocalY, legsExpandedLocalY, legsExpanded), 0f);


        // Spring physics according to hooks law
        float feetY = feet.position.y;
        float trampolinY = trampolin.position.y;
        Vector3 springForce = Vector3.zero;
        if (feetY < trampolinY)
        {
            // https://de.wikipedia.org/wiki/Hookesches_Gesetz
            float springCompression = trampolinY - feetY;
            springForce = springRate * springCompression * trampolin.up;
        }

        Vector3 dragForce = -(drag * velocity.magnitude * velocity.magnitude) * velocity.normalized;

        Vector3 acceleration = (springForce + dragForce) / mass; // acceleration due to spring force and drag force (m/s2)
        acceleration += gravitationalPull; // add graviational acceleration

        velocity += acceleration * Time.deltaTime; // apply acceleration to velocity
        transform.position += velocity * Time.deltaTime; // apply velocity to position
    }

    // Update is called once per frame
    private void Update()
    {
        // move trampolin cloth bone position if feet are touching
        float feetY = feet.position.y;
        float trampolinY = trampolin.position.y;
        if (feetY < trampolinY)
        {
            trampolinCloth.position = new Vector3(trampolinClothRestPos.x, feetY, trampolinClothRestPos.z);
        }
        else
        {
            trampolinCloth.position = trampolinClothRestPos;
        }
    }
}
