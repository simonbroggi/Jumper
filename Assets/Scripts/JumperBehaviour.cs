using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class JumperBehaviour : MonoBehaviour
{
    [Tooltip("The body of the character")]
    public Transform centerOfMass;

    [Range(0.1f, 100f)]
    public float mass = 20f;
    
    [Range(0.1f, 10f)]
    public float jumpAnimationSpeed = 1f;

    [Range(0f, 20f)]
    public float airDrag = 1f; // air resistance

    [Range(1f, 100f)]
    public float rotationSpeed = 50f;

    public Trampolin trampolin;

    Vector3 centerOfMassPosition;
    Vector3 velocity = Vector3.zero;
    
    float jumpAnimationTime = 0f;
    Animator characterAnimator;

    Vector3 gravitationalPull = new Vector3(0f, -9.8f, 0f); // m/s2

    // Start is called before the first frame update
    void Start()
    {
        characterAnimator = GetComponent<Animator>();

        // centerOfMass should be defined.
        // In case it is not defined, try to find it according to position
        if(centerOfMass == null)
        {
            foreach(Transform bone in transform.GetComponentsInChildren<Transform>())
            {
                if(centerOfMass == null || centerOfMass.position.y < bone.position.y)
                {
                    centerOfMass = bone;
                }
            }
        }


        centerOfMassPosition = centerOfMass?centerOfMass.position:transform.position;

        if(trampolin == null)
        {
            trampolin = FindObjectOfType<Trampolin>();
        }

        if(trampolin == null)
        {
            Debug.LogError("No trampolin found in the scene!");
        }
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        Vector3 dragForce = -(airDrag * velocity.magnitude * velocity.magnitude) * velocity.normalized;

        // Spring physics according to hooks law
        Vector3 springForce = Vector3.zero;
        if(transform.position.y < trampolin.transform.position.y)
        {
            float springCompression = trampolin.transform.position.y - transform.position.y;
            springForce = trampolin.springRate * springCompression * trampolin.transform.up;

            dragForce += -(trampolin.trampolinDrag * velocity.magnitude) * velocity.normalized;
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
        trampolin.UpdateAnimation(transform.position);
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

        if (Input.GetKey(KeyCode.A) || Input.GetKey(KeyCode.LeftArrow))
        {
            characterAnimator.transform.Rotate(Vector3.up * rotationSpeed * Time.deltaTime);
        }

        if (Input.GetKey(KeyCode.D) || Input.GetKey(KeyCode.RightArrow))
        {
            characterAnimator.transform.Rotate(-Vector3.up * rotationSpeed * Time.deltaTime);
        }

        characterAnimator.SetFloat("jumpAnimationTime", jumpAnimationTime);
    }
}
