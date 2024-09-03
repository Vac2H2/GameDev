using UnityEngine;

public class Projectile : MonoBehaviour
{
    public GameObject hitEffect;
    public float effectScale = 0.5f;
    
    private Vector3 firingPoint;
    [SerializeField] private float projectileSpeed;
    [SerializeField] private float maxProjectileDistance;
    [SerializeField] private int damage = 1;

    void Start()
    {
        firingPoint = transform.position;
    }

    void Update()
    {
        MoveProjectile();
    }

    void MoveProjectile()
    {
        if (Vector3.Distance(firingPoint, transform.position) > maxProjectileDistance)
        {
            Destroy(this.gameObject);
        }
        else
        {
            transform.Translate(Vector3.forward * projectileSpeed * Time.deltaTime);
        }
    }

    void OnTriggerEnter(Collider other)
    {
        EnemyHealth enemy = other.GetComponent<EnemyHealth>();
        if (enemy != null)
        {
            enemy.TakeDamage(damage);
            if (hitEffect != null) 
            {
                GameObject effect = Instantiate(hitEffect, transform.position, Quaternion.identity);
                effect.transform.localScale *= effectScale;

                Destroy(effect, 2);
            }
        }
        Destroy(this.gameObject);
    }
}