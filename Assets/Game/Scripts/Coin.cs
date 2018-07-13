using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Coin : MonoBehaviour
{
    [SerializeField] private AudioClip _pickup;

    private void OnTriggerStay(Collider other)
    {
        if (other.tag == "Player")
        {
            if (Input.GetKeyDown(KeyCode.E))
            {
                Player player = other.GetComponent<Player>();
                if (player != null)
                {
                    player._hasCoin = true;
                    AudioSource.PlayClipAtPoint(_pickup, transform.position, 0.5f);
                    UI_Manager uiManager = GameObject.Find("Canvas").GetComponent<UI_Manager>();
                    if (uiManager != null)
                    {
                        uiManager.GotCoin();
                    }
                    Destroy(this.gameObject);
                }
            }
        }
    }
}
