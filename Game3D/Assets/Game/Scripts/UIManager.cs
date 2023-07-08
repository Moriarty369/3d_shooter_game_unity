using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{
   [SerializeField]
   private Text _ammoText;
   [SerializeField]
   private GameObject _coin;

   public void UpdateAmmo(int count)
   {
     _ammoText.text = "Munición: " + count;
   }

   public void CollectedCoin()
   {
      _coin.SetActive(true);
   }
}
