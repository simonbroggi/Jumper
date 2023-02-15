using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class JumperBehaviour : MonoBehaviour
{
    [Header("Jumper")]
    public Transform centerOfMass;

    [Range(0.1f, 100f)]
    public float mass = 20f;
    
    [Range(0.1f, 10f)]
    public float jumpAnimationSpeed = 1f;

    [Range(0f, 20f)]
    public float airDrag = 1f; // air resistance

    [Header("Trampolin")]

    public Transform trampolin;

    public Transform trampolinCloth;

    public float springRate = 2000f; // Federkonstante (kg/s2)

    [Range(0f, 30f)]
    public float trampolinDrag = 15f; // trampolin damping


    Vector3 centerOfMassPosition;
    Vector3 velocity = Vector3.zero;
    
    float jumpAnimationTime = 0f;
    Animator characterAnimator;

    Vector3 trampolinClothRestPosition;
    Vector3 gravitationalPull = new Vector3(0f, -9.8f, 0f); // m/s2

    // Start is called before the first frame update
    void Start()
    {
        characterAnimator = GetComponent<Animator>();
        centerOfMassPosition = centerOfMass?centerOfMass.position:transform.position;

        trampolinClothRestPosition = trampolinCloth.position;
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        Vector3 dragForce = -(airDrag * velocity.magnitude * velocity.magnitude) * velocity.normalized;

        // Spring physics according to hooks law
        Vector3 springForce = Vector3.zero;
        if(transform.position.y < trampolin.position.y)
        {
            float springCompression = trampolin.position.y - transform.position.y;
            springForce = springRate * springCompression * trampolin.up;

            dragForce += -(trampolinDrag * velocity.magnitude) * velocity.normalized;
        }

        Vector3 acceleration = (springForce + dragForce) / mass;
        acceleration += gravitationalPull; // add graviational acceleration

        velocity += acceleration * Time.deltaTime; // apply acceleration to velocity
        centerOfMassPosition += velocity * Time.deltaTime; // apply velocity to position

    }

    void OnAnimatorMove()
    {
        if (centerOfMass != null)
        {
            // move such that center of mass stays at the same position
            Vector3 deltaPos = centerOfMass.position - centerOfMassPosition;
            transform.position -= deltaPos;
        }

        // move trampolin cloth bone for trampolin animation.
        // this all doesn't really work with multiple jumpers...
        if(transform.position.y <= trampolin.position.y)
        {
            trampolinCloth.position = new Vector3(trampolinClothRestPosition.x, transform.position.y, trampolinClothRestPosition.z);
        }
    }

    private void Update()
    {
        if (Input.GetKey(KeyCode.Space) || Input.GetMouseButton(0))
        {
            jumpAnimationTime = Mathf.MoveTowards(jumpAnimationTime, 1f, jumpAnimationSpeed * Time.deltaTime);
        }
        else
        {
            jumpAnimationTime = Mathf.MoveTowards(jumpAnimationTime, 0f, jumpAnimationSpeed * Time.deltaTime);
        }

        if (Input.GetKey(KeyCode.LeftShift) || Input.GetMouseButton(1))
        {
            characterAnimator.SetBool("special", true);
        }
        else
        {
            characterAnimator.SetBool("special", false);
        }


        characterAnimator.SetFloat("jumpAnimationTime", jumpAnimationTime);
    }
}
