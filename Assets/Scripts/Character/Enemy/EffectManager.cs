using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EffectManager : MonoBehaviour
{
    private static EffectManager _instance;
    private ObjectPool _effectPool;

    public static EffectManager Instance 
    { 
        get 
        { 
            if(_instance == null)
            {
                Init();
            }
            return _instance; 
        } 
    }

    private void Awake()
    {
        RemoveDuplicates();
    }

    private static void Init()
    {
        if (_instance == null)
        {
            GameObject gameObj = new GameObject();
            gameObj.name = typeof(EffectManager).Name;
            _instance = gameObj.AddComponent<EffectManager>();
            DontDestroyOnLoad(gameObj);
        }
    }

    private void RemoveDuplicates()
    {
        if (_instance == null)
        {
            _instance = this;
            _effectPool = GetComponent<ObjectPool>();
        }
        else
        {
            Destroy(gameObject);
        }
    }

    public void ShotEffect(string tag, Vector3 pos)
    {
        GameObject effect = _effectPool.SpawnFromPool(tag);
        effect.transform.position = pos;
    }

    public GameObject FollowEffect(string tag, GameObject player)
    {
        GameObject effect = _effectPool.SpawnFromPool(tag);
        //effect.GetComponent<EffectFollowPlayer>().SetPlayer(player);

        return effect;
    }
}
