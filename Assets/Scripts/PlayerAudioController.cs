using UnityEngine;

public class PlayerAudioController : MonoBehaviour
{
    private AudioSource playerAudio;

    public AudioClip hit;
    public AudioClip koHit;
    public AudioClip jump;

    private void Start()
    {
        playerAudio = GetComponent<AudioSource>();
    }
    public void PlayJumpSound()
    {
        playerAudio.PlayOneShot(jump);
    }    
}
