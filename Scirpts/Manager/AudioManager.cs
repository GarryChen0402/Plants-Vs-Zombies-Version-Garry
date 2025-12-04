using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UIElements;

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

    public void PlayFx(string name, Vector3 position, float volume)
    {
        if (!_dictionaryEffects.ContainsKey(name))
        {
            return;
        }

        SoundEffect effect = _dictionaryEffects[name];
        AudioSource.PlayClipAtPoint(effect.GetRandomClip(), position, volume);
    }

    public void PlayFx(string name, float volume = 0.5f)
    {
        if (_listener == null) _listener = FindFirstObjectByType<AudioListener>();

        PlayFx(name, _listener.transform.position, volume);
    }

    public void PlayFxAtTime(string name, float time, float volume = 0.5f)
    {
        if (_listener == null) _listener = FindFirstObjectByType<AudioListener>();
        if (!_dictionaryEffects.ContainsKey(name))
        {
            return;
        }

        SoundEffect effect = _dictionaryEffects[name];
        //AudioSource.PlayClipAtPoint(effect.GetRandomClip(), _listener.transform.position, volume);

        GameObject gameObject = new GameObject("One shot audio");
        gameObject.transform.position = _listener.transform.position;
        AudioSource audioSource = (AudioSource)gameObject.AddComponent(typeof(AudioSource));
        AudioClip clip = effect.GetRandomClip();
        audioSource.clip = clip;
        audioSource.spatialBlend = 1f;
        audioSource.time = time;
        audioSource.volume = volume;
        audioSource.Play();
        Object.Destroy(gameObject, clip.length * ((Time.timeScale < 0.01f) ? 0.01f : Time.timeScale));

    }




}
