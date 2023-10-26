using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace RhythmEngine
{
    [RequireComponent(typeof(AudioSource))]
    public class RhythmTracker : MonoBehaviour
    {
        private AudioSource m_source;

        [SerializeField] float threshold = 0.01f;
        [SerializeField] int frameSize = 1024;

        public List<int> mySets = new List<int>();

        private void Start()
        {
            m_source = GetComponent<AudioSource>();

            var clip = m_source.clip;
            var samples = new float[clip.samples * clip.channels];
            m_source.clip.GetData(samples, 0);

            var previousFrame = new float[frameSize];
            for (int i = 0; i < samples.Length - frameSize; i += frameSize)
            {
                float sumCurrentFrame = 0;
                float sumPreviousFrame = 0;
                for (int j = 0; j < frameSize; j++)
                {
                    sumCurrentFrame += Mathf.Abs(samples[i + j]);
                }
                for (int j = 0; j < frameSize; j++)
                {
                    sumPreviousFrame += Mathf.Abs(previousFrame[j]);
                }

                if ((sumCurrentFrame - sumPreviousFrame) > threshold)
                {
                    mySets.Add(i);
                }

                for (int j = 0; j < frameSize; j++)
                {
                    previousFrame[j] = samples[i + j];
                }
            }
        }
    }
}
