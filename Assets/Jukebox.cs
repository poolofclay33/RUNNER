using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(AudioSource))]
public class Jukebox : MonoBehaviour {

    public AudioClip _music;

    public IEnumerator Music()
    {
        AudioSource audio = GetComponent<AudioSource>();
        audio.Play();
        yield return new WaitForSeconds(0f);
        audio.clip = _music;
        audio.Play();
    }
}
