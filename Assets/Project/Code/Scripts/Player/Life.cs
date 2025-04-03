using System.Collections;
using UnityEngine;

public class Life : MonoBehaviour
{
    public float maxLife = 50f;
    public float life = 50f;
    public float shield = 50f;
    public float maxShield = 50f;
    public float shieldRegenDelay = 3f;
    public float shieldRegenRate = 5f;
    private bool regeneratingShield = false;
    public GameObject gameoverPanel, HUD;



    private IEnumerator RegenerateShield()
    {
        // Esperar el tiempo de delay antes de regenerar
        yield return new WaitForSeconds(shieldRegenDelay);

        regeneratingShield = true;

        while (regeneratingShield && shield < maxShield)
        {
            shield += shieldRegenRate * Time.deltaTime; // Regenerar escudo gradualmente
            shield = Mathf.Min(shield, maxShield); // Asegurarse de que no supere el máximo
            yield return null; // Esperar al siguiente frame
        }

        regeneratingShield = false;
    }

    private void OnEnable()
    {
        EventManager.OnPlayerHit += TakeDamage;
    }
    private void OnDisable()
    {
        EventManager.OnPlayerHit -= TakeDamage;
    }


    private void TakeDamage(float damage)
    {
        shield -= damage;
        Debug.Log("Recibiendo daño");
        if (shield <= 0)
        {
            life -= damage;
            if (life <= 0)
            {
                Dead();
            }
        }
        StopCoroutine("RegenerateShield");
        regeneratingShield = false;
        StartCoroutine(RegenerateShield());

    }

    private void Dead()
    {
        if (life == 0)
        {
            Time.timeScale = 0f;
            HUD.SetActive(false);
            gameoverPanel.SetActive(true);
        }
    }


}


