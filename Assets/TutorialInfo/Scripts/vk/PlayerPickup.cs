using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class PlayerPickup : MonoBehaviour
{
    [SerializeField] private float pickupRange;
    [SerializeField] private LayerMask pickupLayer;

    private Camera camera;
    private Gun gun;

    private void Start()
    {
        
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.E))
        {
            Ray ray = camera.ScreenPointToRay(new Vector3(Screen.width / 2, Screen.height / 2));
            RaycastHit hit;

            if (Physics.Raycast(ray, out hit, pickupRange, pickupLayer))
            {
                Debug.Log("Hit: " + hit.transform.name);
                //if(hit.transform.GetComponent<ItemObject>().item as Gun)
               //{
                    //Gun newItem = hit.transform.GetComponent<ItemObject>().ite, as Gun;
                   //inventory.AddItem(newitem);
                //}

                Destroy(hit.transform.gameObject);
            }
        }
    }
    private void GetRefrences()
    {

    }
}
