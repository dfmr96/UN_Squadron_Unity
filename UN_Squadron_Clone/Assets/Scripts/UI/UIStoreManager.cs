using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class UIStoreManager : MonoBehaviour
{
    public GameObject firstAvalaible;
    [SerializeField] Number_Fonts _numberFonts;
    [SerializeField] Image[] _moneyImages;
    public int moneyCheat = 0;

    private void Awake()
    {
        GameManager.instance.SetUIStoreManager(this);
    }
    void Start()
    {
        UpdateMoneySprites(GameManager.instance.Money);
        EventSystem.current.SetSelectedGameObject(firstAvalaible);
    }

    public void UpdateMoneySprites(int money)
    {
        int[] moneyDigits = GetIntArray(money);
        foreach (Image image in _moneyImages)
        {
            image.gameObject.SetActive(false);
        }
        _moneyImages[0].gameObject.SetActive(true);
        for (int i = 0; i < moneyDigits.Length; i++)
        {
            Debug.Log(moneyDigits[i]);
            if (!_moneyImages[i].gameObject.activeSelf) _moneyImages[i].gameObject.SetActive(true);
            _moneyImages[i].sprite = _numberFonts.sprite[moneyDigits[i]];
        }
    }

    int[] GetIntArray(int num)
    {
        List<int> list = new List<int>();
        while (num > 0)
        {
            list.Add(num % 10);
            num /= 10;
        }
        //list.Reverse();
        return list.ToArray();
    }
}
