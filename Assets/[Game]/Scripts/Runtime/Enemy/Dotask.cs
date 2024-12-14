using UnityEngine;

public class Dotask : MonoBehaviour
{
    public GameObject customerPrefab;
    public Transform[] destinations;
    private GameObject instantiatedCustomer; 

    public void InstantiateCustomerPrefab()
    {
        if (instantiatedCustomer == null)
        {
            Vector3 spawnPosition = transform.position;

            instantiatedCustomer = Instantiate(customerPrefab, spawnPosition, Quaternion.identity);

            Enemy customerNavMesh = instantiatedCustomer.GetComponent<Enemy>();
            if (customerNavMesh != null)
            {
                //customerNavMesh.SetDestinations(destinations);
            }
        }
    }
}
