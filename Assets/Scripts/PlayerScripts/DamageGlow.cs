using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using UnityEngine;

public class DamageGlow : MonoBehaviour
{

    bool canGlow = true;
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Attack"))
        {
            RedPlayer();
        }
    }

    private async Task RedPlayer()
    {
        canGlow = false;
        GetComponent<SpriteRenderer>().color = Color.white;
        Color color = new Color(0.3f, 0f, 0f);
        float t = 0f;

        while (t < 1f)
        {
            t += Time.deltaTime * 10f;
            GetComponent<SpriteRenderer>().color = Color.Lerp(Color.white, color, t);
            await Task.Yield();
        }
        t = 1f;
        while (t > 0f)
        {
            t -= Time.deltaTime * 10f;
            GetComponent<SpriteRenderer>().color = Color.Lerp(Color.white, color, t);
            await Task.Yield();
        }
        canGlow = true;

    }
}
