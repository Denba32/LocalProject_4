using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimationEvents : MonoBehaviour
{
    public AudioSource aSource;

    private void Awake()
    {
        aSource = GetComponent<AudioSource>();  
    }

    public void PlaySound()
    {
        
    }
    
}
