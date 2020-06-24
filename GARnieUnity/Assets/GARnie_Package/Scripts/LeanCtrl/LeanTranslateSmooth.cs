using UnityEngine;
namespace Lean.Touch
{

    /// <summary>
    /// Allow to modify transform of the current GameObject with smoothing 
    /// </summary>
    public class LeanTranslateSmooth : LeanTranslate
    {
        [Tooltip("How smoothly this object moves to its target position")]
        public float Dampening = 10.0f;
        // The position we still need to add
        [HideInInspector]
        public Vector3 RemainingDelta;

        /// <summary>
        /// don't need to select model to translate
        /// </summary>
        protected override void Start()
        {
            RequiredSelectable = null;
        }


        protected virtual void LateUpdate()
        {
            // Get t value
            var factor = LeanTouch.GetDampenFactor(Dampening, Time.deltaTime);
            // Dampen remainingDelta
            var newDelta = Vector3.Lerp(RemainingDelta, Vector3.zero, factor);
            // Shift this transform by the change in delta(Atul`s change)
            Vector3 moveMe = (RemainingDelta - newDelta);
            moveMe.z = -moveMe.y; //-: selse bad inverse translate z 
            moveMe.y = 0f;

            transform.position += moveMe;
            // Update remainingDelta with the dampened value
            RemainingDelta = newDelta;
        }
        protected override void Translate(Vector2 screenDelta)
        {

            // Make sure the camera exists
            var camera = LeanTouch.GetCamera(Camera, gameObject);
            if (camera != null)
            {
                // Store old position
                var oldPosition = transform.position;
                // Screen position of the transform
                var screenPosition = camera.WorldToScreenPoint(oldPosition);
                // Add the deltaPosition
                screenPosition += (Vector3)screenDelta;
                // Convert back to world space
                var newPosition = camera.ScreenToWorldPoint(screenPosition);
                // Add to delta
                RemainingDelta += newPosition - oldPosition;
            }
        }
    }
}