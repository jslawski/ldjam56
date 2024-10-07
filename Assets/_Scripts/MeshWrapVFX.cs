using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum VFXDeathBehaviour
{
    NONE,
    DESTROY,
    DETACH_FROM_PARENT,
    DETACH_AND_HIDE
}

public class MeshWrapVFX : MonoBehaviour
{
    [SerializeField]
    private bool seekForMeshStartingFromParent = true;

    [SerializeField]
    private VFXDeathBehaviour deathBehaviour;
    
    private ParticleSystem ps;

    private float lifeTime = 0f;

    public void SetLifeTime(float inLifeTime)
    {
        lifeTime = inLifeTime;
    }

    private void OnEnable()
    {
        ps = GetComponent<ParticleSystem>();
        if (seekForMeshStartingFromParent)
        {
            MeshRenderer mr = transform.parent.GetComponentInChildren<MeshRenderer>();
            UpdateParticleWithMesh(mr);
        }
    }
    
    public void UpdateParticleWithMesh(MeshRenderer inMr)
    {
        ParticleSystem.ShapeModule shapeModule = ps.shape;
        shapeModule.meshRenderer = inMr;
    }

    // Update is called once per frame
    void Update()
    {
        if (lifeTime > 0)
        {
            lifeTime -= Time.deltaTime;
            if (lifeTime <= 0)
            {
                switch (deathBehaviour)
                {
                    case VFXDeathBehaviour.DESTROY:
                        Destroy(gameObject);
                        break;
                    case VFXDeathBehaviour.DETACH_FROM_PARENT:
                        transform.SetParent(transform.root);
                        break;
                    case VFXDeathBehaviour.DETACH_AND_HIDE:
                        transform.SetParent(transform.root);
                        transform.gameObject.SetActive(false);
                        break;
                }
            }
        }
    }
}
