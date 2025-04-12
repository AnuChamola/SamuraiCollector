using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioManager : MonoBehaviour
{
    public static AudioManager instance;
    public AudioSource musicSource, effectSource;

    public List<AudioData> audioList;
    // Start is called before the first frame update
    void Start()
    {
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(this.gameObject);
        }
        else
            Destroy(this.gameObject);
    }

    public void PlaySound(string name, bool forcePlay = false)
    {
        AudioClip clip = FindSound(name);
        if (clip != null)
        {
            if(!forcePlay)
            {
                if(!effectSource.isPlaying)
                {
                    effectSource.PlayOneShot(clip);

                }
            }
            else
            {
                effectSource.PlayOneShot(clip);
            }

        }
    }
 
    public AudioClip FindSound(string name)
    {
        return audioList.Find(a => a.name == name).clip;
    }
}
[System.Serializable]
public class AudioData
{
    public string name;
    public AudioClip clip;
}