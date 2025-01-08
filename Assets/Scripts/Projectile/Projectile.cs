using UnityEngine;

namespace Unchord
{
    public class Projectile : MonoBehaviour
    {
        public bool usePenetration = false;
        public float moveSpeed;

        private Transform _targetTransform;
        private Vector2 _destination;
        private Vector2 _direction;

        private bool _shouldDestroy;

        private void Awake()
        {
            _shouldDestroy = false;
        }

        private void FixedUpdate()
        {
            if (_targetTransform != null)
                FixedUpdateTraceLogic();
            else
                FixedUpdateLinearMovementLogic();
        }

        private void Update()
        {
            if (_shouldDestroy)
            {
                Destroy(gameObject);
            }
        }

        public void Trace(Transform targetTransform)
        {
            Debug.Assert(targetTransform != null);

            _targetTransform = targetTransform;
            _destination = targetTransform.position;
            _direction = Vector2.zero;
        }

        public void SetDirection(Vector2 direction)
        {
            Debug.Assert(direction.sqrMagnitude > 0.0f, "Projectile's direction cannot be zero vector.");

            _targetTransform = null;
            _direction = direction.normalized;
        }

        private void FixedUpdateTraceLogic()
        {
            Vector2 current = transform.position;

            if (_targetTransform != null)
                _destination = _targetTransform.position;

            Vector2 diff = _destination - current;

            float stepDistance = moveSpeed * Time.fixedDeltaTime;

            if (ScreenBounds.EvalScreenZone(current) != ScreenZone.Dead && stepDistance < diff.magnitude)
                transform.position = (current + diff.normalized * stepDistance);
            else
                _shouldDestroy = true;
        }

        private void FixedUpdateLinearMovementLogic()
        {
            Vector2 current = transform.position;
            float stepDistance = moveSpeed * Time.fixedDeltaTime;

            if (ScreenBounds.EvalScreenZone(current) == ScreenZone.Dead)
                _shouldDestroy = true;
            else
                transform.position = (current + _direction * stepDistance);
        }
    }
}