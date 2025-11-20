using UnityEngine;
using UnityEngine.SceneManagement;

namespace Audio
{
    public class AudioManager : MonoBehaviour
    {
        public static AudioManager Instance { get; private set; }
        public AudioSource buttonAudioSource;
        public AudioSource musicSource;
        public AudioClip menuMusic;
        public AudioClip levelMusic;
        
        private bool musicMuted = false;
        private bool soundMuted = false;
    
        private void Awake()
        {
            // check if there is no other instance of audio class currently
            if (Instance == null)
            {
                // if there isn't, current instance is assigned
                Instance = this;
            
                // keep audio alive between scenes
                DontDestroyOnLoad(gameObject);
            }
            else
            {
                // destroy current GameObject
                Destroy(gameObject);
            }
            
        }

        private void Start()
        {
            PlayMenuMusic();
        }
        
        public void PlayButtonSound(AudioClip clip)
        {
            buttonAudioSource.PlayOneShot(clip);
        }
        
        public void PlayMenuMusic()
        {
            if (musicSource.clip == menuMusic) return;
            musicSource.clip = menuMusic;
            musicSource.loop = true;
            musicSource.Play();
        }

        public void PlayLevelMusic()
        {
            if (musicSource.clip == levelMusic) return;
            musicSource.clip = levelMusic;
            musicSource.loop = true;
            musicSource.Play();
        }
        
        public void SetMusicVolume(float value)
        {
            musicSource.volume = value;
        }

        public void ToggleMusic()
        {
            if (musicMuted)
            {
                UnmuteMusic();
            }
            else
            {
                MuteMusic();
            }
        }

        private void MuteMusic()
        {
            musicMuted = true;
            musicSource.mute = true;
        }

        private void UnmuteMusic()
        {
            musicMuted = false;
            musicSource.mute = false;
        }

        public void ToggleSound()
        {
            if (soundMuted)
            {
                UnmuteSound();
            }
            else
            {
                MuteSound();
            }
        }

        private void MuteSound()
        {
            soundMuted = true;
            buttonAudioSource.mute = true;
        }

        private void UnmuteSound()
        {
            soundMuted = false;
            buttonAudioSource.mute = false;
        }
    
    }
}
