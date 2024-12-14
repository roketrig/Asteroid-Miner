using UnityEngine;

public class Gem : MonoBehaviour, ICollectable
{
    [SerializeField] private AsteroidType _asteroidType;
    public AsteroidType AsteroidType { get { return _asteroidType; } }

    [SerializeField] private GameObject _ammoPrefab; // Gem'e karşılık gelen mermi prefab'ı
    public GameObject AmmoPrefab { get { return _ammoPrefab; } }

    public void GetCollected()
    {
        // Gem toplama işlemi
    }

    public bool IsAmmo()
    {
        return false;
    }

    // Ek olarak, türü debug etmek için bir fonksiyon ekleyebilirsiniz
    public void PrintAsteroidType()
    {
        Debug.Log("Asteroid Type: " + _asteroidType);
    }
}
