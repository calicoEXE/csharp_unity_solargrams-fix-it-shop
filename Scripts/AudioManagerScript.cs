using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioManagerScript : MonoBehaviour
{
    public List<AudioClip> bgClips = new List<AudioClip>();
    public GameManager refToGM;
    AudioSource refToAudioSource;

    //
    public AudioClip shopBell;
    public AudioClip cashierSound;
    public AudioClip transitionSound;
    public AudioClip buttonNoise;
    public AudioClip pcBootSE;
    public AudioClip givingItemBackSE;

    //Machine Sounds
    public AudioClip itemPlaced;
    public AudioClip machineSolved;
    public AudioClip wrongitemPlaced;

    //Refurbish Sounds
    public AudioClip refurbishComplete;
    public AudioClip refurbishing;


    public AudioSource refToSoundEffect;
    public AudioSource refToButtonAudio;
    public AudioSource reftoMachineAudio;
    public AudioSource reftoRefurbishAudio;
    

    void Start()
    {
        refToAudioSource = GetComponent<AudioSource>();
        if(refToAudioSource.clip == null)
        {
            refToAudioSource.clip = bgClips[0];
        }
        refToAudioSource.Play();


    }

    public void SwitchBGAudio()
    {
        // Loop back to the beginning if reached the end of bgClips
        if (bgClips.Count > 0)
        {
            StartCoroutine(PlayTransitionSoundCoroutine());

            int currentIndex = bgClips.IndexOf(refToAudioSource.clip);
            int nextIndex = (currentIndex + 1) % bgClips.Count;

            refToAudioSource.clip = bgClips[nextIndex];
            refToAudioSource.Play(); // Play the new background audio
        }
    }

    private IEnumerator PlayTransitionSoundCoroutine()
    {
        // Play the transition sound effect
        refToSoundEffect.clip = transitionSound; // Replace with your transition sound effect
        refToSoundEffect.Play();

        // Wait for 3 seconds before stopping the transition sound effect
        yield return new WaitForSeconds(3f);

        refToSoundEffect.Stop();
    }
    public void npcArrivedBellSE()
    {
        refToSoundEffect.clip = shopBell;
        refToSoundEffect.PlayOneShot(shopBell);
        refToSoundEffect.clip = null;
    }
    public void itemReceivedDiaTriggerSE()
    {
        refToSoundEffect.clip = cashierSound;
        refToSoundEffect.Play();
    }
    public void onButtonDownAudio()
    {
        Debug.Log("Button audio has been audioed");
        refToSoundEffect.clip = buttonNoise;
        refToSoundEffect.Play();
        //refToButtonAudio.PlayOneShot(refToButtonAudio.clip);
    }
    public void onPCBootSE()
    {
        Debug.Log("PC");
        refToSoundEffect.clip = pcBootSE;
        refToSoundEffect.Play();
        refToSoundEffect.Stop();
        //refToButtonAudio.PlayOneShot(refToButtonAudio.clip);
    }
    public void onItemBackToNPCSE()
    {
        Debug.Log("Item");
        refToSoundEffect.clip = givingItemBackSE;
        refToSoundEffect.Play();
        refToSoundEffect.Stop();
        //refToButtonAudio.PlayOneShot(refToButtonAudio.clip);
    }
    public void ItemPlacedMachine()
    {

        reftoMachineAudio.clip = itemPlaced;
        reftoMachineAudio.Play();
    }
    public void MachineSolved()
    { 
       // reftoMachineAudio.clip = machineSolved;
        reftoMachineAudio.PlayOneShot(machineSolved);
    }

    public void WrongItemPlaced()
    {
        reftoMachineAudio.clip = wrongitemPlaced;
        reftoMachineAudio.PlayOneShot(wrongitemPlaced);
    }

    public void RefurbishComplete()
    {
        reftoRefurbishAudio.Stop();
        reftoRefurbishAudio.clip = refurbishComplete;
        reftoRefurbishAudio.Play();
    }

    public void Refurbishing()
    {
        reftoRefurbishAudio.clip = refurbishing;
        reftoRefurbishAudio.PlayOneShot(refurbishing);
    }
}


