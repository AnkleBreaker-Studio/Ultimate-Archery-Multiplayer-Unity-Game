using UnityEngine;

namespace UI_Animations_Scripts
{
    public class TweenRotationCustomAnimations : MonoBehaviour
    {

        public void PlayTweenReverse()
        {
            TweenRotation tweenRotation = this.GetComponent<TweenRotation>();

            tweenRotation.to = -tweenRotation.to;
            tweenRotation.AddOnFinished(() =>
            {
                tweenRotation.SetStartToCurrentValue();
                tweenRotation.to = -tweenRotation.to;
                tweenRotation.PlayForward();
            });
            tweenRotation.PlayForward();
        }
    
    }
}
