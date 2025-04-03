using UnityEngine;
using UnityEngine.UI;

public class LifeHUD : MonoBehaviour
{
    public Slider life;
    public Slider shield;
    public Life lifeAndShield;

    private void Update()
    {
        life.maxValue = lifeAndShield.maxLife;
        shield.maxValue = lifeAndShield.maxShield;

        life.value = lifeAndShield.life;
        shield.value = lifeAndShield.shield;
    }

}
