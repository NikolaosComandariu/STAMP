using UnityEngine;

public class AudioManager : MonoBehaviour
{
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

    public void PlaySFX(AudioClip clip)
    {
        SFXSource.PlayOneShot(clip);
    }
<<<<<<< Updated upstream
}
=======
}
>>>>>>> Stashed changes
