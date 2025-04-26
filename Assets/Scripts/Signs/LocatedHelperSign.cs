using System.Collections;
using System.Collections.Generic;
using Unity.Mathematics;
using Unity.VisualScripting;
using UnityEngine;
using static UnityEngine.GraphicsBuffer;

public class LocatedHelperSign : MonoBehaviour
{
    Collider2D[] toys = new Collider2D[5];
    private void OnEnable()
    {

        Physics2D.OverlapCircleNonAlloc(new Vector2(transform.position.x, transform.position.y), 100f, toys);
        Collider2D closesetToy = FinClosestCollider(toys);
        if (closesetToy == null) return;
        Vector2 rot = (closesetToy.transform.position - transform.position).normalized;
        float rotation = Mathf.Acos(rot.x)*Mathf.Rad2Deg-90;
        transform.rotation = Quaternion.Euler(0, 0, rotation);

    }
    Collider2D FinClosestCollider(Collider2D[] toys)
    {
        if(toys == null)
        {
            return null;
        }
        float distance = Mathf.Infinity;
        Collider2D closestCollider = null;
        foreach (Collider2D col in toys)
        {
            float colDistance = Vector3.Distance(col.transform.position, transform.position);
            if (colDistance < distance)
            {
                distance = colDistance;
                closestCollider = col;
            }

        }
        return closestCollider;
    }
}
