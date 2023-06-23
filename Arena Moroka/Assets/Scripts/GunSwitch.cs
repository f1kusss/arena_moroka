using UnityEngine;

public class GunSwitch : MonoBehaviour
{
    [Header("Gun type")]
    public GameObject[] weapons; // ������ � �������

    public static int currentWeaponIndex = 0;

    private void Start()
    {
        SwitchWeapon(currentWeaponIndex); // ������������� ��������� ������
    }

    private void Update()
    {
        int previousWeaponIndex = currentWeaponIndex;

        // ��������� ������� ������ "1" � "2"
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

        // ���� ��������� ������ ����������, ����������� ���
        if (currentWeaponIndex != previousWeaponIndex)
        {
            SwitchWeapon(currentWeaponIndex);
        }
    }

    private void SwitchWeapon(int weaponIndex)
    {
        // ������������ ��� ������
        for (int i = 0; i < weapons.Length; i++)
        {
            weapons[i].SetActive(false);
        }

        // ���������� ��������� ������
        weapons[weaponIndex].SetActive(true);
    }
}
