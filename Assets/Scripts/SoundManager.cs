using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;

public enum SoundName
{
    Jump, Boost, WinGame, GameOver, Coin
}

public class SoundManager : MonoBehaviour
{
    AudioSource audioSource;
    public AudioClip jumpAudio;
    public AudioClip boostAudio;
    public AudioClip winGameAudio;
    public AudioClip GameOverAudio;
    public AudioClip CoinAudio;

    private void Start()
    {
        audioSource = GetComponent<AudioSource>();
    }

    public void PlaySound(SoundName soundName)
    {
        switch (soundName)
        {
            case SoundName.Jump: audioSource.clip = jumpAudio; break;
            case SoundName.Boost: audioSource.clip = boostAudio; break;
            case SoundName.WinGame: audioSource.clip = winGameAudio; break;
            case SoundName.GameOver: audioSource.clip = GameOverAudio; break;
            case SoundName.Coin: audioSource.clip = CoinAudio; break;
        }
        audioSource.Play();
    }

}
