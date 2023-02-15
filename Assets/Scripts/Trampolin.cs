using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Trampolin : MonoBehaviour
{
    public Transform cloth;

    public float springRate = 2000f; // Federkonstante (kg/s2)

    [Range(0f, 30f)]
    public float trampolinDrag = 15f; // trampolin damping

    Vector3 trampolinClothRestPosition;

    int frameLastUpdatedAnimation = 0;

    public void UpdateAnimation(Vector3 feetPosition)
    {
        if(feetPosition.y <= transform.position.y)
        {
            if (Time.frameCount == frameLastUpdatedAnimation)
            {
                // if the cloth animation has already been updated this frame (by another jumper)
                // then take the minnimum position to allow multiple jumpers to jump on the same trampolin
                cloth.position = new Vector3(trampolinClothRestPosition.x, Mathf.Min(feetPosition.y, cloth.position.y), trampolinClothRestPosition.z);
            }
            else
            {
                // otherwise simply set the position to the y of the feet.
                cloth.position = new Vector3(trampolinClothRestPosition.x, feetPosition.y, trampolinClothRestPosition.z);
            }
        }
        frameLastUpdatedAnimation = Time.frameCount;
    }

    // Start is called before the first frame update
    void Start()
    {
        trampolinClothRestPosition = cloth.position;
    }
}
