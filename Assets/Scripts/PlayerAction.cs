using UnityEngine;

public class PlayerAction : MonoBehaviour
{
    [SerializeField]
    private Guns Guns;

    public void OnShoot()
    {
        Guns.Shoot();
    }
    
}
