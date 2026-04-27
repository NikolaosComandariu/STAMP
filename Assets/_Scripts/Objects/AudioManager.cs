using UnityEngine;

public class AudioManager : MonoBehaviour
{
<<<<<<< Updated upstream
<<<<<<< Updated upstream
    [Header ("------ Audio Source -----")]
=======
    [Header("------ Audio Source -----")]
>>>>>>> Stashed changes
    [SerializeField] private AudioSource musicSource;
    [SerializeField] private AudioSource SFXSource;

    [Header("------ Audio Clip -----")]
    public AudioClip correctChoiceSFX;
    public AudioClip incorrectChoiceSFX;
    public AudioClip roundChangeSFX;

    private void start()
    {

    }

=======
    [Header("-----Audio Source-----")]
    [SerializeField] AudioSource musicSource;
    [SerializeField] AudioSource SFXSource;

    [Header("-----Audio Clip-----")]
    public AudioClip ItemAccepted;
    public AudioClip ItemDeclined;

    // Example of how to play sound effect -
    //audioManager.PlaySFX(audioManager.(nameofsound));
>>>>>>> Stashed changes
    public void PlaySFX(AudioClip clip)
    {
        SFXSource.PlayOneShot(clip);
    }
<<<<<<< Updated upstream
<<<<<<< Updated upstream
}
=======
}
>>>>>>> Stashed changes
=======
}
>>>>>>> Stashed changes
