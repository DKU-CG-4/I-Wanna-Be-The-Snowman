using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioManager : MonoBehaviour
{
    public enum ClipType { GetItem, EnterPortal };
    private static AudioManager _instance;
    public AudioClip[] arrAudio;
    public static AudioManager Instance
    {
        get
        {
            if (!_instance)
            {
                _instance = FindObjectOfType(typeof(AudioManager)) as AudioManager;

                if (_instance == null)
                    Debug.LogError("AudioManager ½Ì±ÛÅæ °´Ã¼°¡ ¾ø½À´Ï´Ù.");
            }
            return _instance;
        }
    }

    void Awake()
    {
        if (_instance == null)
        {
            _instance = this;
        }
        else if (_instance != this)
        {
            Destroy(gameObject);
        }
        DontDestroyOnLoad(gameObject);
    }
    public void PlaySfx(ClipType type)
    {
        AudioClip audio = arrAudio[(int)type];
        GetComponent<AudioSource>().PlayOneShot(audio, 0.8f);
    }
}
