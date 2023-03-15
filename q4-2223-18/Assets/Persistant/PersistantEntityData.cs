using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[CreateAssetMenu(fileName = "EntityData", menuName = "Persistant/PersistantEntityData", order = 1)]
public class PersistantEntityData : ScriptableObject
{
    [SerializeField] private List<int> entitysToDelete = new List<int>();
    private void OnEnable() => hideFlags = HideFlags.DontUnloadUnusedAsset;

    private void OnValidate()
    {
        entitysToDelete.Clear(); 
    }
    public void addEntity(int entity)
    {
        entitysToDelete.Add(entity); 
    }
    public void removeEntity(int entity)
    {
        entitysToDelete.Remove(entity);
        entitysToDelete.TrimExcess(); 
    }
    public bool hasEntity(int entity)
    {
        return entitysToDelete.Contains(entity); 
    }

    

}
