using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class PlayerInventory : MonoBehaviour
{
    public Transform StackParent;
    [SerializeField] private float distanceBetweenObject = 0.25f;

    private bool _isCarrying;
    public bool IsCarrying { get { return _isCarrying; } }
    private List<Transform> collectedTrans = new List<Transform>();
    private bool _ammoCollected = false;
    private int _stackLimit = 10;
    private int _maxQueueLimit=10;
    private Stack<Ammo> ammoStack = new Stack<Ammo>();
    private bool _isEnterProductMachine;
    public Transform DropAmmoPoint;

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("AmmoPoint"))
        {
            StartCoroutine(StackAmmoOnAmmoPoint(other.transform));
        }

        if (other.TryGetComponent(out ICollectable collectable) && collectedTrans.Count < _stackLimit)
        {
            if (collectedTrans.Count == 0)
            {
                collectable.GetCollected();
                TestCollectItem(other.gameObject.transform);
                _ammoCollected = collectable.IsAmmo();
            }
            else if (collectable.IsAmmo() && _ammoCollected)
            {
                collectable.GetCollected();
                TestCollectItem(other.gameObject.transform);
                //ammoStack.Push(other.GetComponent<Ammo>());
            }
            else if (!collectable.IsAmmo() && !_ammoCollected)
            {
                collectable.GetCollected();
                TestCollectItem(other.gameObject.transform);
            }
        }
        ProductMachine productMachine = other.GetComponent<ProductMachine>();
        if (productMachine != null)
        {
            if (_isEnterProductMachine)
                return;
            _isEnterProductMachine = true;
            StartCoroutine(DropGemsToProductMachine(productMachine));
        }
    }

    private void OnTriggerExit(Collider other)
    {
        ProductMachine productMachine = other.GetComponent<ProductMachine>();
        if (productMachine != null)
        {
            _isEnterProductMachine = false;
        }
    }

    private void TestCollectItem(Transform collectedObject)
    {
        if (!collectedTrans.Contains(collectedObject))
        {
            _isCarrying = true;
            collectedObject.SetParent(StackParent);
            Vector3 desPos = collectedTrans.Count == 0 ? Vector3.zero : new Vector3(0, collectedTrans.Count * distanceBetweenObject, 0);
            collectedObject.localPosition = desPos;
            collectedObject.localRotation = Quaternion.identity;
            collectedObject.localRotation = Quaternion.Euler(0, -90, 0);
            collectedTrans.Add(collectedObject);

            if (collectedObject.TryGetComponent(out Gem gem))
            {
                gem.PrintAsteroidType(); 
            }
        }
    }


    private IEnumerator DropGemsToProductMachine(ProductMachine machine)
    {
        for (int i = collectedTrans.Count - 1; i >= 0; i--)
        {
            Transform itemTransform = collectedTrans[i];
            itemTransform.TryGetComponent(out Gem gem);

            if (machine.productList.ContainsKey(gem.AsteroidType))
            {
                itemTransform.SetParent(machine.transform);
                Vector3 desPos = new Vector3(0, (collectedTrans.Count - 1 - i) * distanceBetweenObject, 0);
                Tween jumpTween = itemTransform.DOLocalJump(desPos, 1f, 1, 0.1f)
                                                 .SetEase(Ease.OutQuad)
                                                 .OnComplete(() =>
                                                 {
                                                     if (itemTransform != null)
                                                     {
                                                         itemTransform.localRotation = Quaternion.identity;
                                                         machine.AddGem(gem);
                                                         collectedTrans.RemoveAt(i);
                                                         itemTransform.gameObject.SetActive(false);
                                                     }
                                                 });
                yield return jumpTween.WaitForCompletion();
            }
        }

        _isCarrying = collectedTrans.Count > 0;
    }


    private IEnumerator StackAmmoOnAmmoPoint(Transform ammoPoint)
    {
        if (ammoStack.Count >= _maxQueueLimit)
        {
            yield break;
        }

        var indexPos = ammoStack.Count - 1;
        List<Transform> itemsToRemove = new List<Transform>(collectedTrans);

        for (int i = itemsToRemove.Count - 1; i >= 0; i--)
        {
            if (ammoStack.Count >= _maxQueueLimit)
            {
                yield break;
            }

            var pose = new Vector3(0, indexPos - 0.3f, 0);
            Transform itemTransform = itemsToRemove[i];
            itemTransform.SetParent(DropAmmoPoint);

            Collider itemCollider = itemTransform.GetComponent<Collider>();
            if (itemCollider != null)
            {
                itemCollider.isTrigger = false; 
            }

            itemTransform.DOLocalMove(pose, 0.2f).OnComplete(() => {
                if (itemCollider != null)
                {
                    itemCollider.enabled = false; 
                }
            });

            itemTransform.DORotate(Vector3.zero, 0.2f);
            indexPos++;
            ammoPoint.GetComponent<AmmoPoint>().DropAmmo(itemTransform.gameObject);
            ammoStack.Push(itemTransform.GetComponent<Ammo>());
            collectedTrans.Remove(itemTransform);
        }

        _isCarrying = false;
        yield break;
    }





    public void CollectAmmo(Ammo ammo)
    {
        _ammoCollected = true;
        TestCollectItem(ammo.transform);
        //ammoStack.Push(ammo);
    }
}
