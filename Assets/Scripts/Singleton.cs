using UnityEngine;

public class Singleton<T> : MonoBehaviour where T : Component
{
    private static T instance;
    public static T Instance
    {
        get
        {
            if (instance == null)
            {
                GameObject go = new GameObject();
                go.name = typeof(T).Name;
                instance = go.AddComponent<T>();
            }
            return instance;
        }

    }
    private void OnDestroy()
    {
        if (instance == null)
        {
            instance = null;
        }
    }
}
