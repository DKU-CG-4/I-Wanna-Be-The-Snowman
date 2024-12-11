using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioManager : MonoBehaviour
{
    private static AudioManager _instance;

    public enum EBgm { BGM, EndingBGM };
    public enum ESfx { GetItem, EnterPortal };

    public AudioClip[] bgms;
    public AudioClip[] sfxs;

    public AudioSource audioBgm;
    public AudioSource audioSfx;

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

    public void PlayBGM(EBgm bgmIdx)
    {
        audioBgm.clip = bgms[(int)bgmIdx];
        audioBgm.Play();
    }

    public void StopBGM()
    {
        audioBgm.Stop();
    }

    public void PlaySfx(ESfx esfxIdx)
    {
        audioSfx.PlayOneShot(sfxs[(int)esfxIdx]);
    }
}
