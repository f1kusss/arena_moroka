using UnityEngine;

public class GunSwitch : MonoBehaviour
{
    [Header("���� ������")]
    public GameObject[] weapons; // ������ � �������

    private int currentWeaponIndex = 0;

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
