using UnityEngine;

namespace Audio
{
    public class AudioManager : MonoBehaviour
    {
        public static AudioManager Instance { get; private set; }
        public AudioSource buttonAudioSource;
    
    
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

        public void PlayButtonSound(AudioClip clip)
        {
            buttonAudioSource.PlayOneShot(clip);
        }
    
    
    }
}
