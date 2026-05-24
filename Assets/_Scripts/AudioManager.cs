using UnityEngine;
using UnityEngine.Audio;

public class AudioManager : MonoBehaviour
{
    [Header("-----Audio Source-----")]
    [SerializeField] AudioSource musicSource;
    [SerializeField] AudioSource SFXSource;
    [SerializeField] AudioSource EnvironmentalSource;

    [Header("------ Audio Clip -----")]
    public AudioClip correctChoiceSFX;
    public AudioClip incorrectChoiceSFX;
    public AudioClip gameStartSFX;
    public AudioClip backgroundButtonClickUISFX;
    public AudioClip defaultMusic;
    public AudioClip gameChangerMusic;
    public AudioClip hitboxAcceptSFX;
    public AudioClip printSFX;

    private void OnEnable()
    {
        GameChangerManager.onGameChangerGenerated += PlayGameChangerMusic;
        GameManager.onNextRound += PlayOriginalMusic;
    }

    private void OnDisable()
    {
        GameChangerManager.onGameChangerGenerated -= PlayGameChangerMusic;
        GameManager.onNextRound -= PlayOriginalMusic;
    }

    private void Start()
    {
        //musicSource.clip = background;
        //musicSource.Play();
    }

    // Example of how to play sound effect -
    //audioManager.PlaySFX(audioManager.(nameofsound));
    public void PlaySFX(AudioClip clip)
    {
        SFXSource.PlayOneShot(clip);
    }

    private void PlayOriginalMusic()
    {
        Debug.Log("Playing og music");
        musicSource.clip = defaultMusic;
        musicSource.Play();
    }

    private void PlayGameChangerMusic(int nothing)
    {
        Debug.Log("Playing game changer music");
        musicSource.clip = gameChangerMusic;
        musicSource.Play();
    }
}