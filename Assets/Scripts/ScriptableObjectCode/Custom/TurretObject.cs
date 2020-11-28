using UnityEngine;


// Goal would be to put things here that is the same for each version of the turret.
// For example things such as base stats are always the same but things such as their stats scaled to level would be different.

[CreateAssetMenu(fileName = "TurretObject", menuName = "Turret")]
public class TurretObject : ScriptableObject
{
    [SerializeField] private float baseDamage;
    [SerializeField] private float baseRange;
    [SerializeField] private GameObject selectedPrefab = null;
    [SerializeField] private GameObject actualPrefab = null;

    public GameObject GetActualPrefab()
    {
        return actualPrefab;
    }
    
    public GameObject GetSelectedPrefab()
    {
        return selectedPrefab;
    }
}