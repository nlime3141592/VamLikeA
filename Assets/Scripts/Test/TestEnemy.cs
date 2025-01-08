using UnityEngine;

namespace Unchord
{
    public class TestEnemy : MonoBehaviour
    {
        public float moveSpeed = 2.0f;

        private void FixedUpdate()
        {
            Vector2 current = transform.position;
            Vector2 destination = Vector2.zero;

            Vector2 nextPosition = Vector2.Lerp(current, destination, Time.fixedDeltaTime * moveSpeed);
            transform.position = nextPosition;

            if (Vector2.Distance(nextPosition, destination) < 1e-2)
                Destroy(gameObject);
        }
    }
}