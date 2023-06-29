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

    public float abilityDuration = 10f; // ������������ �����������
    public float abilityCooldown = 30f; // ����� ����������� �����������

    private bool isAbilityActive = false; // ����, ������������, ������� �� �����������
    private float abilityTimer = 0f; // ������ ��� ������������ ������������ �����������
    private float cooldownTimer = 0f; // ������ ��� ������������ ����������� �����������

    private void Update()
    {
        // ���� ����������� �������, ��������� ������ ������������ �����������
        if (isAbilityActive)
        {
            abilityTimer -= Time.deltaTime;
            if (abilityTimer <= 0f)
            {
                // ����� ����������� �������, ������������ �����������
                isAbilityActive = false;
                // ��������, ����� ����� ��������� �������������� �������� ��� ���������� �����������
                rifle.damage = 1;
                pistol.damage = 15;
                molot.damage = 25;
            }
        }
        else
        {
            // ���� ����������� ���������, ����������� ������ ����������� �����������
            cooldownTimer += Time.deltaTime;
            abilityIMG.color = Color.gray;
        }

        // ���������, ����� �� ������������ �����������
        if (cooldownTimer >= abilityCooldown)
        {
            abilityIMG.color = Color.white;
            // ����� ����� �������� ��� ��� ��������� �����������, ��������, ������� ������� ��� ������� �������
            if (Input.GetKeyDown(KeyCode.Q))
            {
                ActivateAbility();
            }
        }
    }

    private void ActivateAbility()
    {
        // ���������� ����������� � ���������� �������
        isAbilityActive = true;
        abilityTimer = abilityDuration;
        cooldownTimer = 0f;

        rifle.damage = 5;
        pistol.damage = 30;
        molot.damage = 50;
        abilityIMG.color = Color.yellow;
    }
}
