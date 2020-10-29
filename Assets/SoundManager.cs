using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundManager : MonoBehaviour
{

    public static AudioClip failTypeSound, wordCompleteSound, powerUpSound, loseSound, loseSound2, lostStreakSound, UISound;
    static AudioSource audioSource;

    

   

    // Start is called before the first frame update
    void Start()
    {    
        failTypeSound = Resources.Load<AudioClip>("hit_24");
        wordCompleteSound = Resources.Load<AudioClip>("hit_16");
        powerUpSound = Resources.Load<AudioClip>("power_up_03");
        lostStreakSound = Resources.Load<AudioClip>("game_over_13");
        loseSound = Resources.Load<AudioClip>("game_over_30");
        loseSound2 = Resources.Load<AudioClip>("power_up_19");
        UISound = Resources.Load<AudioClip>("hit_10");
        

        audioSource = GetComponent<AudioSource>();
    }


   

    public static void PlaySound(string clip)
    {
        switch(clip)
        {
            
            case "hit_24":
                audioSource.PlayOneShot(failTypeSound);
                break;
            case "hit_16":
                audioSource.PlayOneShot(wordCompleteSound);
                break;
            case "power_up_03":
                audioSource.PlayOneShot(powerUpSound);
                break;
            case "game_over_30":
                audioSource.PlayOneShot(loseSound);
                break;
            case "power_up_19":
                audioSource.PlayOneShot(loseSound2);
                break;
            case "hit_10":
                audioSource.PlayOneShot(UISound);
                break;
            case "game_over_13":
                audioSource.PlayOneShot(lostStreakSound);
                break;
        }
    }

    public void PlayMenuSound()
    {
        audioSource.PlayOneShot(UISound);
    }
}
