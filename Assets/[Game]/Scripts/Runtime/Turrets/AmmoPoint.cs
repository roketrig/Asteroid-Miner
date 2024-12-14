using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AmmoPoint : MonoBehaviour
{
    [SerializeField] private TurretController _myTurret;
    public Transform turret;
    public float moveDuration = 2f;

    private Queue<Ammo> ammoQueue = new Queue<Ammo>();

    public void DropAmmo(GameObject ammoObject)
    {
        Ammo ammo = ammoObject.GetComponent<Ammo>();
        _myTurret.ReceiveAmmo(ammo);

        ammoQueue.Enqueue(ammo);
       // StartCoroutine(MoveAmmoToTurret(ammo));
    }
    /*
    private IEnumerator MoveAmmoToTurret(Ammo ammo)
    {
        Vector3 originalPosition = ammo.transform.position;
        Vector3 targetPosition = turret.position;

        ammo.transform.DOLocalMove(targetPosition, moveDuration).OnComplete(() =>
        {
            TurretController turretController = turret.GetComponent<TurretController>();
            turretController.ReceiveAmmo(ammo);
            ammoQueue.Dequeue(); 
            Destroy(ammo.gameObject);  
        });

        yield return new WaitForSeconds(moveDuration);
    }
    */

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Valuable"))
        {
            //StartCoroutine(StackAmmoOnAmmoPoint(other.transform));
        }
    }
    /*
    private IEnumerator StackAmmoOnAmmoPoint(Transform ammo)
    {
        ammo.SetParent(transform);
        Vector3 targetPosition = new Vector3(0, transform.childCount * 0.25f, 0);
        ammo.DOLocalMove(targetPosition, 0.2f);
        yield return new WaitForSeconds(0.2f);
    }
    */
}
