using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Explode : MonoBehaviour
{
    public ParticleSystem prefeb;
    public float radius;
    public float damage;
    public float power;
    List<Target> tg = new List<Target>();
    List<Rigidbody> rbL = new List<Rigidbody>();
    public void Explosion()
    {
        Collider[] collider = Physics.OverlapSphere(transform.position, radius);

        foreach (Collider c in collider)
        {
            Target target = c.GetComponent<Target>();
            Rigidbody rb = c.GetComponentInParent<Rigidbody>();
            Target self = this.GetComponent<Target>();

            if (target != null)
            {
                if (!tg.Contains(target))
                {
                    tg.Add(target);
                }
            }
            if (rb != null)
            {
                if (!rbL.Contains(rb))
                {
                    rbL.Add(rb);
                }
               
            }
        }
        StartCoroutine(Boom(0.5f, tg,rbL));
    }
    IEnumerator Boom(float time, List<Target> targets, List<Rigidbody> rigidBodys)
    {
        yield return new WaitForSeconds(time);

        if(this != null)
        {
            foreach (Target targ in targets)
            {
                if(targ != null)
                {
                    targ.TakeDamage(damage);
                    Debug.Log(targ.name);
                }
            }

            foreach (Rigidbody rb in rigidBodys)
            {
                if (rb != null)
                {
                    rb.AddExplosionForce(8f, this.transform.position, radius, 15f, ForceMode.Impulse);
                }
            }
            targets.Clear();
            rigidBodys.Clear();
            ClearAll();
            Instantiate(prefeb, this.transform.position, Quaternion.identity);
            Destroy(gameObject);
        }
    }
    void ClearAll()
    {
        tg.Clear();
        rbL.Clear();
    }
}
