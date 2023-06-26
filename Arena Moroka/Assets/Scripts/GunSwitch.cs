using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class GunSwitch : MonoBehaviour
{
    [Header("Gun type")]
    public GameObject[] weapons;

    public Image chooseWeapon;
    public Sprite chooseMolot;
    public Sprite choosePistol;
    public Sprite chooseRifle;
    public TextMeshProUGUI ammoText;
    public SingleGunController pistol;
    public AutoGunController rifle;

    public static int currentWeaponIndex = 0;

    private void Start()
    {
        SwitchWeapon(currentWeaponIndex);

    }

    private void Update()
    {
        int previousWeaponIndex = currentWeaponIndex;

        if (Input.GetKeyDown(KeyCode.Alpha1))
        {
            currentWeaponIndex = 0;
            chooseWeapon.sprite = chooseMolot;
            ammoText.text = "";
        }
        else if (Input.GetKeyDown(KeyCode.Alpha2))
        {
            currentWeaponIndex = 1;
            chooseWeapon.sprite = choosePistol;
            ammoText.text = pistol.currentAmmo.ToString();

        }
		else if (Input.GetKeyDown(KeyCode.Alpha3))
        {
            currentWeaponIndex = 2;
            chooseWeapon.sprite = chooseRifle;
            ammoText.text = rifle.currentAmmo.ToString();
        }

        // ���� ��������� ������ ����������, ����������� ���
        if (currentWeaponIndex != previousWeaponIndex)
        {
            // Обновление текста с количеством патронов после смены оружия
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
