using UnityEngine;

public class Item : MonoBehaviour
{
    private ObjectSpawner spawner;
    private ObjectPrototype_ prototype;

    void Start()
    {
        spawner = FindAnyObjectByType<ObjectSpawner>();
        prototype = GetComponent<ObjectPrototype_>();
    }

    public bool MatchesCondition(System.Func<ObjectPrototype_, bool> conditionCheck)
    {
        if (prototype == null)
            return false;

        return conditionCheck(prototype);
    }
}



