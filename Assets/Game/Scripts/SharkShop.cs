using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SharkShop : MonoBehaviour
{
    private void OnTriggerStay(Collider other)
    {
        if (other.tag == "Player")
        {            
            if (Input.GetKeyDown(KeyCode.E))
            {
                Player player = other.GetComponent<Player>();
                if (other!= null)
                {
                    if (player._hasCoin == true)
                    {
                        player._hasCoin = false;
                        UI_Manager uiManager = GameObject.Find("Canvas").GetComponent<UI_Manager>();
                        if (uiManager!=null)
                        {
                            uiManager.RemoveCoin();
                        }
                        AudioSource audio = GetComponent<AudioSource>();
                        audio.Play();
                        player.EnableWeapon();
                    }
                    else
                    {
                        Debug.Log("Get out of here!");
                    }
                }                
            }
        }
    }

}
