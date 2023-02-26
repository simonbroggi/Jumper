using UnityEngine;

public class AudioController : MonoBehaviour
{
    public AudioSource jumpSound;
    
    public void PlayJumpSound()
    {
        jumpSound.Play();       
    }
}
