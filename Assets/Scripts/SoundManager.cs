using UnityEngine;

public class SoundManager : MonoBehaviour
{
    public static SoundManager Instance;

    [Header("BackGround Music")]
    [SerializeField] private AudioSource _audioSourceBG;
    [SerializeField] private AudioClip _audioClipBG;


    private void Awake()
    {
        if(Instance != null && Instance != this)
        {
            Destroy(gameObject);
            return;
        }
        Instance = this;
    }

    private void Start()
    {
        PlayBackGroundMusic();
    }
    public void PlayBackGroundMusic()
    {
        if(_audioClipBG == null)
        {
            return;
        }
        _audioSourceBG.clip = _audioClipBG;
        _audioSourceBG.loop = true; //play on loop
        _audioSourceBG.Play();
    }
}
