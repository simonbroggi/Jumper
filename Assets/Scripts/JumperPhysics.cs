using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class JumperPhysics : MonoBehaviour
{
    Animator characterAnimator;
    Renderer renderer;
    public Transform centerOfMass;
    Vector3 centerOfMassPosition;
    float legsExpanded = 1;
    public float legsSpeed = 0.1f;
    // Start is called before the first frame update
    void Start()
    {
        characterAnimator = GetComponent<Animator>();
        renderer = characterAnimator.GetComponentInChildren<Renderer>();
        centerOfMassPosition = centerOfMass?centerOfMass.position:renderer.bounds.center;
        
    }

    // Update is called once per frame
    void FixedUpdate()
    {

    }

    void OnAnimatorMove()
    {
        if (centerOfMass != null)
        {
            // move such that center of mass stays at the same position
            Vector3 deltaPos = centerOfMass.position - centerOfMassPosition;
            transform.position -= deltaPos;
        }
    }

    private void Update()
    {
        if (Input.GetKey(KeyCode.Space) || Input.GetMouseButton(0))
        {
            legsExpanded = Mathf.MoveTowards(legsExpanded, 1f, legsSpeed * Time.deltaTime);
        }
        else
        {
            legsExpanded = Mathf.MoveTowards(legsExpanded, 0f, legsSpeed * Time.deltaTime);
        }
        // control current animation
        AnimatorClipInfo[] animatorInfo = characterAnimator.GetCurrentAnimatorClipInfo(0);
        string currentAnimation = animatorInfo[0].clip.name;
        
        characterAnimator.Play("Jump", 0, legsExpanded);
    }
}
