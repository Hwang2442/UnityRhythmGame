using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace RhythmEngine
{
    public class RhythmTracker : MonoBehaviour
    {
        [SerializeField] AudioSource m_source;





        public float BPM { get; private set; }
    }
}
