using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class JumperPhysics : MonoBehaviour
{
    Animator characterAnimator;
    public Transform centerOfMass;
    Vector3 centerOfMassPosition;
    float jumpAnimationTimeNormalized = 1f;
    public float jumpAnimationSpeed = 1f;

    // Start is called before the first frame update
    void Start()
    {
        characterAnimator = GetComponent<Animator>();
        centerOfMassPosition = centerOfMass?centerOfMass.position:transform.position;
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
            jumpAnimationTimeNormalized = Mathf.MoveTowards(jumpAnimationTimeNormalized, 1f, jumpAnimationSpeed * Time.deltaTime);
        }
        else
        {
            jumpAnimationTimeNormalized = Mathf.MoveTowards(jumpAnimationTimeNormalized, 0f, jumpAnimationSpeed * Time.deltaTime);
        }
        // control current animation
        AnimatorClipInfo[] animatorInfo = characterAnimator.GetCurrentAnimatorClipInfo(0);
        string currentAnimation = animatorInfo[0].clip.name;
        
        characterAnimator.Play("Jump", 0, jumpAnimationTimeNormalized);
    }
}
