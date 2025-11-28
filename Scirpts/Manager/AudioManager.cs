using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class AudioManager : MonoBehaviour
{
    public static AudioManager Instance { get; private set; }

    public SoundEffect[] effects;
    private Dictionary<string, SoundEffect> _dictionaryEffects;

    public AudioSource currentBgm = null;
    //private List<AudioSource> fxSource;

    private AudioListener _listener;

    private void Awake()
    {
        if (Instance != null)
        {
            Destroy(gameObject);
            return;
        }
        Instance = this;
        DontDestroyOnLoad(Instance.gameObject);

        _dictionaryEffects = new Dictionary<string, SoundEffect>();
        foreach (SoundEffect effect in effects)
        {
            _dictionaryEffects[effect.name] = effect;
        }
        currentBgm = GetComponent<AudioSource>();
        //fxSource = new List<AudioSource>();

    }

    public void PlayBGM(string name)
    {
        if(_listener == null)_listener = FindFirstObjectByType<AudioListener>();

        PlayBGM(name, _listener.transform.position);
    }

    public void PlayBGM(string name,  Vector3 position)
    {
        if (!_dictionaryEffects.ContainsKey(name))
        {
            return;
        }

        SoundEffect effect = _dictionaryEffects[name];

        if(currentBgm != null)
        {
            StopBGM();
            currentBgm.loop = true;
            currentBgm.clip = effect.GetRandomClip();
            currentBgm.Play();
        }
    }

    public void StopBGM()
    {
        if (currentBgm == null) return;
        currentBgm.Stop();
    }

    public void PlayFx(string name, Vector3 position)
    {
        if (!_dictionaryEffects.ContainsKey(name))
        {
            return;
        }

        SoundEffect effect = _dictionaryEffects[name];
        AudioSource.PlayClipAtPoint(effect.GetRandomClip(), position);
    }

    public void PlayFx(string name)
    {
        if (_listener == null) _listener = FindFirstObjectByType<AudioListener>();

        PlayFx(name, _listener.transform.position);
    }




}
