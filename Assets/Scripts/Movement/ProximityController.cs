using UnityEngine;

namespace Movement
{
    public class ProximityController : MonoBehaviour
    {
        [Tooltip("Maximum distance over which to cast the ray.")]
        public float maxObjectDistance = 1f;

        // TODO: add lastHit
        public AnimationController animationController;
        public InputController inputController;

        private void FixedUpdate()
        {
            UpdateRaycast();
        }

        public void UpdateRaycast()
        {
            UpdateRaycast(inputController.GetInputInfo(), transform.position,
                animationController.GetCurrentFaceDirectionVector());
        }

        public void UpdateRaycast(InputInfo input, Vector2 currentPosition, Vector2 currentFaceDirection)
        {
            if (currentFaceDirection == Vector2.zero)
            {
                return;
            }

            RaycastHit2D hit = Physics2D.Raycast(currentPosition, currentFaceDirection, maxObjectDistance);

            // Debug.DrawRay(transform.position, dirVector, Color.green);
            OnRaycastHit(hit, input);
        }

        private void OnRaycastHit(RaycastHit2D hit, InputInfo input)
        {
            if (!hit.collider)
            {
                return;
            }

            var interactable = hit.collider.gameObject.GetComponentSafe<Interactable>();
            if (!interactable)
            {
                return;
            }

            Debug.LogFormat("<b>Proximity Controller:</b> Raycast Hit Detected: " + hit.collider.gameObject.name);

            // Debug.Log("[raycast hit] @interactable " + hit.collider.gameObject.name);
            interactable.Interact(input);
        }
    }
}