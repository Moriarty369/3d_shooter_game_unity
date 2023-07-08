using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    private CharacterController _controller;
    [SerializeField]
    private float _playerSpeed = 3.5f;
    [SerializeField]
    private float _gravity = 9.81f;
    [SerializeField]
    private GameObject _muzzleFlash; 
    [SerializeField]
    private GameObject _hitMarkerPrefab; 
    [SerializeField]
    private AudioSource _weaponAudio;
    [SerializeField]
    private int _currentAmmo;
    
    public int _maxAmmo = 350;
    private bool _isReloading = false;
    private UIManager _uiManager;
    public bool hasCoin = false;

    void Start()
    {
        _controller = GetComponent<CharacterController>();
        Cursor.visible = false;
        Cursor.lockState = CursorLockMode.Locked;
        _currentAmmo = _maxAmmo;
        _uiManager = GameObject.Find("Canvas").GetComponent<UIManager>();
    }


    void Update()
    {   
        if (!_isReloading && Input.GetMouseButton(0) && _currentAmmo > 0)
        {
           Shoot();
        }
        else 
        {
            _muzzleFlash.SetActive(false);
            _weaponAudio.Stop();
        }
         if (Input.GetKeyDown(KeyCode.Escape))
        {
            Cursor.visible = true;
            Cursor.lockState = CursorLockMode.None;
        }

        if (Input.GetKeyDown(KeyCode.R) && _isReloading == false)
        {
            _isReloading = true;
           StartCoroutine(Reload());
        }
        playerMovement();
    }


    void playerMovement()
    {
        float horizontalInput = Input.GetAxis("Horizontal");
        float verticalInput = Input.GetAxis("Vertical");
        Vector3 direction = new Vector3(horizontalInput, 0, verticalInput);
        _controller.Move(direction * Time.deltaTime);
        Vector3 velocity = direction * _playerSpeed;
        velocity.y -= _gravity;
        // Override velocity to allocate the global rotation to our player
        velocity = transform.transform.TransformDirection(velocity); 
        _controller.Move (velocity * Time.deltaTime);
    }

    
    void Shoot()
    {
         _muzzleFlash.SetActive(true);
            _currentAmmo --;
            _uiManager.UpdateAmmo(_currentAmmo);
            if (_weaponAudio.isPlaying == false)
            {
                _weaponAudio.Play();
            }
            // Ray casting y efecto del impacto laser con un vector perpendicular al punto que golpeamos (lookrotation)
            Ray gunRayCasting = Camera.main.ViewportPointToRay(new Vector3(0.5f, 0.5f, 0));
            RaycastHit hitInfo;
            if(Physics.Raycast(gunRayCasting, out hitInfo))
            {
              // hacemos typecasting de la instancia como una buena práctica  
              GameObject hitMarker = (GameObject)Instantiate(_hitMarkerPrefab, hitInfo.point, Quaternion.LookRotation(hitInfo.normal));
              Destroy(hitMarker, 1f);
            }
    }
    // Creamos la funcion como una instancia de ienumerator para luego ser llamada como co-rutina y programar una pausa de recarga
    IEnumerator Reload()
    {
        yield return new WaitForSeconds(1f);
        _currentAmmo = _maxAmmo;
        _uiManager.UpdateAmmo(_currentAmmo);
        _isReloading = false;
    }
}
