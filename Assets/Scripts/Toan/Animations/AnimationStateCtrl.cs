using UnityEngine;

namespace Manager
{
    
    public class AnimationStateCtrl : MonoBehaviour
    {
        public Animation Animations;

        public void Play(string name)
        {
            Animations.CrossFade(name, 0.25f, PlayMode.StopSameLayer);
        }
    }
}