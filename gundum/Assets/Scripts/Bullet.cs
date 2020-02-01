using UnityEngine;

public class Bullet : MonoBehaviour
{
    [SerializeField] float bulletSpeed = 5;

    Rigidbody2D rb;

    void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    public void SetBulletSpeed(float speed)
    {
        bulletSpeed = speed;
    }

    public void Fire(Vector3 direction, float speed = int.MinValue)
    {
        if(speed == int.MinValue)
            rb.velocity = direction * bulletSpeed;
        else
            rb.velocity = direction * speed;
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.collider.tag == "Enemy") //For player bullets
        {
            collision.gameObject.GetComponent<PlayerEnemyHealth>().TakeDamage();
            Destroy(gameObject);
        }
        else if (collision.collider.tag != "Enemy") //For bullets that collide with world objects
        {
            var pd = gameObject.GetComponent<ProjectileDestroy>();
            if (pd != null)
                pd.DestroyBullet();
        }
    }
}
