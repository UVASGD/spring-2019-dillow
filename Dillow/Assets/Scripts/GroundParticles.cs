using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GroundParticles : MonoBehaviour
{
    public ParticleSystem particles;

    // Start is called before the first frame update
    void Start()
    {
        if (particles == null)
            particles = GetComponent<ParticleSystem>();
    }

    private void OnCollisionEnter(Collision other)
    {
        if (other.gameObject.CompareTag("Ground"))
        {
        }
        else if (other.gameObject.CompareTag("Ground Terrain"))
        {
            int terrainLayer = TerrainSurface.GetMainTexture(transform.position);
        }
        particles.Play();
    }

    private void OnCollisionExit(Collision collision)
    {
        particles.Stop();
    }
}
