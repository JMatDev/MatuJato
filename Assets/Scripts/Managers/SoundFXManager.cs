using UnityEngine;

public class SoundFXManager : MonoBehaviour
{
    public static SoundFXManager instance;
    public AudioSource audioSourcePrefab;

    private void Awake()
    {
        if (instance == null) instance = this;
    }

    public void ExampleMethod()
    {
        Debug.Log("SoundFXManager ExampleMethod called");
    }

    public void PlaySound(AudioClip audioClip, Transform spawnTransform, float volume = 1f)
    {
        //spawn in  gameObject
        AudioSource audioSource = Instantiate(audioSourcePrefab, spawnTransform.position, Quaternion.identity);
        //assign the audioClip
        audioSource.clip = audioClip;
        //assign volume
        audioSource.volume = volume;
        //play sound
        audioSource.Play();
        //get length of the clip
        float clipLength = audioClip.length;
        //destroy the audioSource after the clip has finished playing
        Destroy(audioSource.gameObject, clipLength);
    }

}
