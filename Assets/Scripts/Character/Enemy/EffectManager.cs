using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EffectManager : MonoBehaviour
{
    private static EffectManager instance;
    private ObjectPool effectPools;
    private GameObject curUseEffect;

    public static EffectManager Instance 
    { 
        get 
        { 
            if(instance == null)
            {
                Init();
            }
            return instance; 
        } 
    }

    private void Awake()
    {
        RemoveDuplicates();
    }

    private static void Init()
    {
        if (instance == null)
        {
            GameObject gameObj = new GameObject();
            gameObj.name = typeof(EffectManager).Name;
            instance = gameObj.AddComponent<EffectManager>();
            DontDestroyOnLoad(gameObj);
        }
    }

    private void RemoveDuplicates()
    {
        if (instance == null)
        {
            instance = this;
            effectPools = GetComponent<ObjectPool>();
        }
        else
        {
            Destroy(gameObject);
        }
    }

    public void PlayEffect(string effectTag, Vector3 position, Quaternion rotation)
    {
        GameObject effect = effectPools.SpawnFromPool(effectTag);
        curUseEffect = effect;

        if (effect == null)
        {
            Debug.LogWarning($"No available effect in pool: {effectTag}");
            return;
        }

        effect.transform.position = position;
        effect.transform.rotation = rotation;

        StartCoroutine(DeactivateEffect(effect, 2.0f));
    }

    public void PlayEffect(string effectTag, float delay, Vector3 position, Quaternion rotation)
    {
        GameObject effect = effectPools.SpawnFromPool(effectTag);
        curUseEffect = effect;

        if (effect == null)
        {
            Debug.LogWarning($"No available effect in pool: {effectTag}");
            return;
        }

        effect.transform.position = position;
        effect.transform.rotation = rotation;

        StartCoroutine(DeactivateEffect(effect, delay));
    }

    public void PlayEffect(string effectTag, float delay, Vector3 position, Quaternion rotation, string sfxTag)
    {
        GameObject effect = effectPools.SpawnFromPool(effectTag);
        curUseEffect = effect;

        if (effect == null)
        {
            Debug.LogWarning($"No available effect in pool: {effectTag}");
            return;
        }

        effect.transform.position = position;
        effect.transform.rotation = rotation;

        SoundManager.Instance.Play(sfxTag, Sound.Sfx);
        StartCoroutine(DeactivateEffect(effect, delay));
    }

    public void SettingColor(float r, float g, float b)
    {
        if (curUseEffect == null) return;

        Color color = new Color(r, g, b);

        if (curUseEffect.TryGetComponent(out ParticleSystem particleSystem))
        {
            ParticleSystem.MainModule mainModule = particleSystem.main;
            color.a = mainModule.startColor.color.a;
            mainModule.startColor = color;
        }
    }

    public void SettingColor(float r, float g, float b, float a)
    {
        if (curUseEffect == null) return;

        Color color = new Color(r, g, b, a);

        if(curUseEffect.TryGetComponent(out ParticleSystem particleSystem))
        {
            ParticleSystem.MainModule mainModule = particleSystem.main;
            mainModule.startColor = color;
        }
    }

    private IEnumerator DeactivateEffect(GameObject effect, float delay)
    {
        yield return new WaitForSeconds(delay);
        effect.SetActive(false);
    }
}
