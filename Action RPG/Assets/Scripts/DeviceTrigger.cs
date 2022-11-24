using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DeviceTrigger : MonoBehaviour
{
    [SerializeField] private GameObject[] targets;
    public bool requireKey;
    // Start is called before the first frame update

    void OnTriggerEnter(Collider other)
    {
      if(requireKey && Managers.Inventory.equippedItem != "key")
      {
        return;
      }
      foreach (GameObject target in targets)
      {
        target.SendMessage("Activate");
      }
    }

    void OnTriggerExit(Collider other)
    {
      if(requireKey && Managers.Inventory.equippedItem != "key")
      {
        return;
      }
      foreach (GameObject target in targets)
      {
        target.SendMessage("Deactivate");
      }
    }
}
