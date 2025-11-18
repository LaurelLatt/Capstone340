using UnityEngine;

namespace Audio
{
    public class ButtonAudio : MonoBehaviour
    {
        public AudioClip[] buttonSounds;
        // Start is called once before the first execution of Update after the MonoBehaviour is created
        void Start()
        {
        
        }

        // Update is called once per frame
        void Update()
        {
        
        }

        public void PlayButtonSound()
        {
            int index = Random.Range(0, buttonSounds.Length);
            AudioManager.Instance.PlayButtonSound(buttonSounds[index]);
        }
    }
}
