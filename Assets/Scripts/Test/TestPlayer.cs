using UnityEngine;

namespace Unchord
{
    public class TestPlayer : MonoBehaviour
    {
        public float moveSpeed = 2.0f;
        public float shootCooltime = 1.5f;
        public float projectileMoveSpeed = 3.5f;

        private float ix;
        private float iy;

        private float leftShootCooltime;

        private void Awake()
        {
            GameManager.Instance.Subscribe(this);
        }

        private void FixedUpdate()
        {
            Move();
        }

        private void Move()
        {
            Vector3 delta = Vector3.zero;
            delta.x = moveSpeed * ix * Time.fixedDeltaTime;
            delta.y = moveSpeed * iy * Time.fixedDeltaTime;

            transform.position += delta;
        }

        private void Update()
        {
            UpdateInput();
            UpdateShooting();
        }

        private void UpdateInput()
        {
            ix = Input.GetAxisRaw("Horizontal");
            iy = Input.GetAxisRaw("Vertical");
        }

        private void UpdateShooting()
        {
            if (leftShootCooltime > 0.0f)
            {
                leftShootCooltime -= Time.deltaTime;
                return;
            }

            leftShootCooltime += shootCooltime;

            GameObject resource = Resources.Load("TestProjectile") as GameObject;
            GameObject instance = GameObject.Instantiate(resource);

            float radian = UnityEngine.Random.value * Mathf.PI * 2.0f;
            float cos = Mathf.Cos(radian);
            float sin = Mathf.Sin(radian);
            Vector2 direction = new Vector2(cos, sin);

            Projectile projectile = instance.GetComponent<Projectile>();
            projectile.Trace(GameManager.Instance.transform);
            // projectile.SetDirection(direction);
            projectile.transform.position = this.transform.position;
            projectile.moveSpeed = projectileMoveSpeed;
        }
    }
}