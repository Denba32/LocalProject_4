using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundManager : Singleton<SoundManager>
{
    public AudioSource bgm;

    public List<AudioSource> effects;

    public void Play(AudioClip clip, float volume, float pitch = 1, Transform parent = null, Define.SoundType type = Define.SoundType.Bgm)
    {
        if (type == Define.SoundType.Bgm)
        {
            if (bgm != null)
            {
                if (bgm.isPlaying)
                {
                    bgm.Stop();
                }

                bgm.volume = volume;
                bgm.pitch = pitch;
                bgm.clip = clip;

                bgm.Play();
            }
        }

        else if (type == Define.SoundType.Effect)
        {
            if (effects != null)
            {
                for (int i = 0; i < effects.Count; i++)
                {
                    if (effects[i].isPlaying)
                        continue;
                    if (i == effects.Count)
                    {
                        // TODO : 새로 오디오 소스를 생성해야 함.
                        AudioSource aSource = CreateEffectAudio(i);
                        aSource.transform.SetParent(parent);
                        aSource.PlayOneShot(clip, volume);
                        effects.Add(aSource);
                        return;
                    }
                    effects[i].transform.SetParent(parent);
                    effects[i].PlayOneShot(clip, volume);
                    break;
                }
            }
        }
        else
        {
            return;
        }
    }
    public void Play(AudioClip clip, float volume, float pitch = 1, Define.SoundType type = Define.SoundType.Bgm)
    {
        if (type == Define.SoundType.Bgm)
        {
            if(bgm != null)
            {
                if(bgm.isPlaying)
                {
                    bgm.Stop();
                }

                bgm.volume = volume;
                bgm.pitch = pitch;
                bgm.clip = clip;

                bgm.Play();
            }
        }

        else if (type == Define.SoundType.Effect)
        {
            if(effects != null)
            {
                for (int i = 0; i < effects.Count; i++)
                {                    
                    if (effects[i].isPlaying)
                        continue;
                    if (i == effects.Count)
                    {
                        // TODO : 새로 오디오 소스를 생성해야 함.
                        AudioSource aSource = CreateEffectAudio(i);
                        aSource.PlayOneShot(clip, volume);
                        effects.Add(aSource);
                        return;

                    }
                    effects[i].PlayOneShot(clip, volume);
                    break;
                }
            }
        }
        else
        {
            return;
        }
    }

    private AudioSource CreateEffectAudio(int index)
    {
        GameObject go = new GameObject($"EffectAudio {index}");
        AudioSource effect = go.AddComponent<AudioSource>();
        return effect;
    }
}