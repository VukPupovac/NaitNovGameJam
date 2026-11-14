using UnityEngine;

public class AudioManager : MonoBehaviour
{

    [SerializeField] private AudioClip fireClip;
    [SerializeField] private AudioClip hitClip;
    [SerializeField] private float volume = 0.3f;


    public void PlayFire()
    {
        AudioSource.PlayClipAtPoint(fireClip, transform.position, volume);
    }

    public void PlayHit()
    {
        AudioSource.PlayClipAtPoint(hitClip, transform.position, volume);
    }
}
