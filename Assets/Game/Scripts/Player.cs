using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    private CharacterController _controller;
    private UI_Manager _uiManager;
    
    [SerializeField] private float _speed = 3.5f;
    [SerializeField] private GameObject _muzzleFlash;
    [SerializeField] private GameObject _hitMarker;
    [SerializeField] private GameObject _weapon;

    private float _gravity = 9.81f;
    private float _horizontalInput;
    private float _verticalInput;
    private RaycastHit hitinfo;
    private Ray rayOrigin;

    [SerializeField] private int _currentAmmo;
    private int _maxAmmo = 50;
    private bool _isreloading = false;
    public bool _hasCoin = false;
    

    void Start()
    {
        _controller = GetComponent<CharacterController>();
        Cursor.visible = false;
        Cursor.lockState = CursorLockMode.Locked;
        _currentAmmo = _maxAmmo;
        _uiManager = GameObject.Find("Canvas").GetComponent<UI_Manager>();
    }
	
	
	void Update()
    {
        CalculateMovement();
        
        if (Input.GetMouseButton(0) && _currentAmmo > 0)
        {
            Shooting();           
        }
        else
        {
            _muzzleFlash.SetActive(false);
        }
        
        if (Input.GetKeyDown(KeyCode.R) && _isreloading == false)
        {
            _isreloading = true;
            StartCoroutine(Reloading());
        }

        if (Input.GetKeyUp(KeyCode.Escape))
        {
            Cursor.visible = true;
            Cursor.lockState = CursorLockMode.None;
        }
	}

    void CalculateMovement()
    {
        _horizontalInput = Input.GetAxis("Horizontal");
        _verticalInput = Input.GetAxis("Vertical");
        Vector3 direction = new Vector3(_horizontalInput, 0, _verticalInput);
        Vector3 velocity = direction * _speed;
        velocity.y -= _gravity;
        velocity = transform.TransformDirection(velocity);
        _controller.Move(velocity * Time.deltaTime);
    }

    void Shooting()
    {
        rayOrigin = Camera.main.ViewportPointToRay(new Vector3(0.5f, 0.5f, 0));
        _muzzleFlash.SetActive(true);
        _currentAmmo--;
        _uiManager.UpdateAmmo(_currentAmmo);

        if (Physics.Raycast(rayOrigin, out hitinfo))
        {
            Debug.Log("Hit " + hitinfo.transform.name);
            GameObject hit = Instantiate(_hitMarker, hitinfo.point, Quaternion.LookRotation(hitinfo.normal)) as GameObject;
            Destroy(hit, 1.6f);

            Destructable crate = hitinfo.transform.GetComponent<Destructable>();
            if (crate != null)
            {
                crate.DestroyCrate();
            }
        }
    }

    IEnumerator Reloading()
    {
        yield return new WaitForSeconds(1.5f);
        _currentAmmo = _maxAmmo;
        _uiManager.UpdateAmmo(_currentAmmo);
        _isreloading = false;
    }

    public void EnableWeapon()
    {
        _weapon.SetActive(true);
    }
}
