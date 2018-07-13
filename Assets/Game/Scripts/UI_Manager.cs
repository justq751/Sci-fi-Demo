﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UI_Manager : MonoBehaviour
{
    [SerializeField] private Text _ammoText;
    [SerializeField] private GameObject _coin;

    public void UpdateAmmo (int count)
    {
        _ammoText.text = "Ammo " + count;
    }

    public void GotCoin ()
    {
        _coin.SetActive(true);
    }
}
