using UnityEngine;

public class GunSwitch : MonoBehaviour
{
    [Header("Gun type")]
    public GameObject[] weapons; // Массив с оружием

    private int currentWeaponIndex = 0;

    private void Start()
    {
        SwitchWeapon(currentWeaponIndex); // Устанавливаем начальное оружие
    }

    private void Update()
    {
        int previousWeaponIndex = currentWeaponIndex;

        // Проверяем нажатие клавиш "1" и "2"
        if (Input.GetKeyDown(KeyCode.Alpha1))
        {
            currentWeaponIndex = 0;
        }
        else if (Input.GetKeyDown(KeyCode.Alpha2))
        {
            currentWeaponIndex = 1;
        }
        else if (Input.GetKeyDown(KeyCode.Alpha3))
        {
            currentWeaponIndex = 2;
        }

        // Если выбранное оружие изменилось, переключаем его
        if (currentWeaponIndex != previousWeaponIndex)
        {
            SwitchWeapon(currentWeaponIndex);
        }
    }

    private void SwitchWeapon(int weaponIndex)
    {
        // Деактивируем все оружие
        for (int i = 0; i < weapons.Length; i++)
        {
            weapons[i].SetActive(false);
        }

        // Активируем выбранное оружие
        weapons[weaponIndex].SetActive(true);
    }
}
