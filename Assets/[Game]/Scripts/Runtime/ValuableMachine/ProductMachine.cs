using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class ProductMachine : MonoBehaviour
{
    public Dictionary<AsteroidType, int> productList = new Dictionary<AsteroidType, int>();
    [SerializeField] private List<ProductItemSO> _requirementDatas;
    [SerializeField] private Transform _ammoSpawnPoint;

    [SerializeField] private Transform _part1;
    [SerializeField] private Transform _part2;

    private int _createdAmmoCount;
    private int _maxCapacity = 5;
    private Tween _productionTween1;
    private Tween _productionTween2;

    private Vector3 _currentSpawnPosition;
    private float _stackOffset = 0.5f;

    private GameObject _currentAmmoPrefab; 
    private bool _isAnimating = false; 

    private void Start()
    {
        InitDatas();
        _currentSpawnPosition = _ammoSpawnPoint.position;
    }

    private void InitDatas()
    {
        for (int i = 0; i < _requirementDatas.Count; i++)
        {
            productList.Add(_requirementDatas[i].Key, 0);
        }
    }

    public void AddGem(Gem gem)
    {
        if (!productList.ContainsKey(gem.AsteroidType))
            return;

        int requirementIndex = _requirementDatas.FindIndex(x => x.Key == gem.AsteroidType);
        if (productList[gem.AsteroidType] < _requirementDatas[requirementIndex].TargetAmount)
        {
            productList[gem.AsteroidType] += 1;

            _currentAmmoPrefab = gem.AmmoPrefab;

            CheckProductState();
        }
    }

    private void CheckProductState()
    {
        int counter = 0;

        for (int i = 0; i < _requirementDatas.Count; i++)
        {
            if (productList[_requirementDatas[i].Key] == _requirementDatas[i].TargetAmount)
            {
                counter++;
            }
        }

        if (counter >= _requirementDatas.Count && !_isAnimating)
        {
            StartProductionAnimation();

            for (int i = 0; i < _requirementDatas.Count; i++)
            {
                productList[_requirementDatas[i].Key] = 0;
            }
        }
    }

    private void StartProductionAnimation()
    {
        _isAnimating = true; 
        _productionTween1 = _part1.DOScaleY(1.1f, 0.5f).SetLoops(-1, LoopType.Yoyo);
        _productionTween2 = _part2.DOScaleY(1.1f, 0.5f).SetLoops(-1, LoopType.Yoyo);

        StartCoroutine(ProductionCoroutine());
    }

    private IEnumerator ProductionCoroutine()
    {
        yield return new WaitForSeconds(5f);

        CreateProduct(_currentAmmoPrefab);
        StopProductionAnimation();
    }

    private void StopProductionAnimation()
    {
        _isAnimating = false; 

        if (_productionTween1 != null)
        {
            _productionTween1.Kill();
            _part1.localScale = Vector3.one;
        }

        if (_productionTween2 != null)
        {
            _productionTween2.Kill();
            _part2.localScale = Vector3.one;
        }
        CheckProductState();
    }

    private void CreateProduct(GameObject ammoPrefab)
    {
        if (_createdAmmoCount >= _maxCapacity)
        {
            _createdAmmoCount = 0;
            _currentSpawnPosition = _ammoSpawnPoint.position;
            return;
        }

        GameObject newAmmo = Instantiate(ammoPrefab, _currentSpawnPosition, Quaternion.identity);
        MoveProductToTarget(newAmmo);

        _currentSpawnPosition += new Vector3(0, _stackOffset, 0);

        _createdAmmoCount++;

        ResetProductionState(); 
    }

    private void MoveProductToTarget(GameObject product)
    {
        Transform productTransform = product.transform;
        productTransform.DOLocalJump(_currentSpawnPosition, 1f, 1, 1f).SetEase(Ease.InOutSine);
    }
    private void ResetProductionState()
    {
        _createdAmmoCount = 0; 
        _currentSpawnPosition = _ammoSpawnPoint.position; 
    }
}
