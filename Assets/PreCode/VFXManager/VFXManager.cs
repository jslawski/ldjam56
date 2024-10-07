using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using RotaryHeart.Lib.SerializableDictionary;
using UnityEngine.Rendering;


public enum VFXType
{
    NONE,
    FOOD_SHINE,
    FOOD_GRAB
}

[Serializable]
public class VFXDictionary : SerializableDictionaryBase<VFXType,ParticleSystem> { };

public class VFXManager : MonoBehaviour
{
    public static VFXManager instance;

    public List<ParticleSystem> vfxCreated = new List<ParticleSystem>();
    
    [SerializeField]
    private VFXDictionary vfxDictionary;
    
    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
    }

    public void PlayVFXAtPosition(VFXType inVFXType, Vector3 inPosition,float deathDelay=0)
    {
        ParticleSystem ps = vfxDictionary[inVFXType];
        ps.gameObject.SetActive(true);
        ps.transform.position = inPosition;
        if (ps.TryGetComponent(out MeshWrapVFX mwVFX))
        {
            mwVFX.UpdateParticleWithMesh(ps.transform.GetComponentInChildren<MeshRenderer>());
            mwVFX.SetLifeTime(deathDelay);
        }
        ps.Play();
    }

    public void PlayVFXAttached(VFXType inVFXType, Transform parentObj,float deathDelay=0)
    {
        ParticleSystem ps = vfxDictionary[inVFXType];
        ps.gameObject.SetActive(true);
        ps.transform.SetParent(parentObj,false);
        ps.transform.localPosition = Vector3.zero;
        if (ps.TryGetComponent(out MeshWrapVFX mwVFX))
        {
            mwVFX.UpdateParticleWithMesh(ps.transform.parent.GetComponentInChildren<MeshRenderer>());
            mwVFX.SetLifeTime(deathDelay);
        }
        ps.Play();
    }
    
    public void CreateVFXAtPosition(VFXType inVFXType, Vector3 inPosition,float deathDelay=0)
    {
        ParticleSystem ps = vfxDictionary[inVFXType];
        ps.gameObject.SetActive(true);
        vfxCreated.Add(ps);
        ps.transform.position = inPosition;
        if (ps.TryGetComponent(out MeshWrapVFX mwVFX))
        {
            mwVFX.UpdateParticleWithMesh(ps.transform.GetComponentInChildren<MeshRenderer>());
            mwVFX.SetLifeTime(deathDelay);
        }
        ps.Play();
    }
    
    public void CreateVFXAttached(VFXType inVFXType, Transform parentObj,float deathDelay=0)
    {
        ParticleSystem ps = Instantiate(vfxDictionary[inVFXType]);
        ps.gameObject.SetActive(true);
        vfxCreated.Add(ps);
        ps.gameObject.SetActive(false);
        ps.transform.SetParent(parentObj);
        if (ps.TryGetComponent(out MeshWrapVFX mwVFX))
        {
            mwVFX.UpdateParticleWithMesh(ps.transform.GetComponentInChildren<MeshRenderer>());
            mwVFX.SetLifeTime(deathDelay);
        }
        ps.Play();
    }

    public void RemoveCreatedVFX(ParticleSystem inPS)
    {
        vfxCreated.Remove(inPS);
    }
}
