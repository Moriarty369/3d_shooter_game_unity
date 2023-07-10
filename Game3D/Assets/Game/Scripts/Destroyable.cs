using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Destroyable : MonoBehaviour
{
    [SerializeField]
    private GameObject _crateDestroyed;
    
    // OJO la función no puede llamarse Destroy ya que Unity ya tiene en su biblioteca una función con ese nombre
    public void DestroyCrate()
    {
        Instantiate(_crateDestroyed, transform.position, transform.rotation);
        Destroy(this.gameObject);
    }
}
