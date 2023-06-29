using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class AbilityGun : MonoBehaviour
{
    public AutoGunController rifle;
    public SingleGunController pistol;
    public MeleeWeaponController molot;
    public Image abilityIMG;

    public float abilityDuration = 10f; // Длительность способности
    public float abilityCooldown = 30f; // Время перезарядки способности

    private bool isAbilityActive = false; // Флаг, определяющий, активна ли способность
    private float abilityTimer = 0f; // Таймер для отслеживания длительности способности
    private float cooldownTimer = 0f; // Таймер для отслеживания перезарядки способности

    private void Update()
    {
        // Если способность активна, уменьшаем таймер длительности способности
        if (isAbilityActive)
        {
            abilityTimer -= Time.deltaTime;
            if (abilityTimer <= 0f)
            {
                // Время способности истекло, деактивируем способность
                isAbilityActive = false;
                // Возможно, здесь нужно выполнить дополнительные действия при завершении способности
                rifle.damage = 1;
                pistol.damage = 15;
                molot.damage = 25;
            }
        }
        else
        {
            // Если способность неактивна, увеличиваем таймер перезарядки способности
            cooldownTimer += Time.deltaTime;
            abilityIMG.color = Color.gray;
        }

        // Проверяем, можно ли активировать способность
        if (cooldownTimer >= abilityCooldown)
        {
            abilityIMG.color = Color.white;
            // Здесь можно вставить код для активации способности, например, нажатие клавиши или триггер события
            if (Input.GetKeyDown(KeyCode.Q))
            {
                ActivateAbility();
            }
        }
    }

    private void ActivateAbility()
    {
        // Активируем способность и сбрасываем таймеры
        isAbilityActive = true;
        abilityTimer = abilityDuration;
        cooldownTimer = 0f;

        rifle.damage = 5;
        pistol.damage = 30;
        molot.damage = 50;
        abilityIMG.color = Color.yellow;
    }
}
