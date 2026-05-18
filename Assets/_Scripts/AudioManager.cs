using Unity.VisualScripting;
using UnityEngine;

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
    public AudioClip RhythmGameChangerMusic;



    //AudioManager audioManager;

    private void OnEnable()
    {
        GameChangerManager.onOppositeDayActivated += OppositeDayMusic;
        GameChangerManager.onRhythmActivated += RhythmMusic;
        GameChangerManager.onRefundActivated += RefundMusic;
        GameManager.onNextRound += DefaultMusic;
}

    private void OnDisable()
    {
        GameChangerManager.onOppositeDayActivated -= OppositeDayMusic;
        GameChangerManager.onRhythmActivated -= RhythmMusic;
        GameChangerManager.onRefundActivated -= RefundMusic;
        GameManager.onNextRound -= DefaultMusic;
    }

    private void Start()
    {
        //musicSource.clip = background;
        //musicSource.Play();
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            //PlaySFX(audioManager.correctChoiceSFX);
        }
    }

    // Example of how to play sound effect -
    //audioManager.PlaySFX(audioManager.(nameofsound));
    public void PlaySFX(AudioClip clip)
    {
        SFXSource.PlayOneShot(clip);
    }

    private void OppositeDayMusic()
    {
        musicSource.clip =
    }

    private void RefundMusic()
    {
        musicSource.clip =
    }

    private void RhythmMusic()
    {
        musicSource.clip =
    }

    private void DefaultMusic()
    {
        musicSource.clip = 
    }
}