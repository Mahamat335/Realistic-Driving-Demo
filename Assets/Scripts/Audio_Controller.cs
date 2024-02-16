using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class Audio_Controller : MonoBehaviour
{
    public AudioClip startClip;
    public AudioClip idleClip;
    public AudioClip accelerateClip;
    public AudioSource audioSource;

    private Car_Controller carController;
    private float rpm;
    private bool start;

    void Start()
    {
        carController = GetComponent<Car_Controller>();
        StartCoroutine(AudioCoroutine());
    }

    IEnumerator AudioCoroutine()
    {
        audioSource.clip = startClip;
        audioSource.Play();
        yield return new WaitForSeconds(audioSource.clip.length * 0.75f);

        audioSource.loop = true;
        audioSource.clip = idleClip;
        audioSource.Play();
        while (!carController.GetStart())
        {
            yield return new WaitForFixedUpdate();
        }
        audioSource.clip = accelerateClip;
        audioSource.Play();
        while (true)
        {
            rpm = carController.GetRpm();
            if (rpm == 0 && audioSource.clip == accelerateClip)
            {
                audioSource.pitch = 1f;
                audioSource.clip = idleClip;
                audioSource.Play();
            }
            if (rpm > 0)
            {
                audioSource.pitch = 0.5f + rpm / 8000f;
                if (audioSource.clip == idleClip)
                {
                    audioSource.clip = accelerateClip;
                    audioSource.Play();
                }
            }

            yield return new WaitForFixedUpdate();
        }
    }
}
