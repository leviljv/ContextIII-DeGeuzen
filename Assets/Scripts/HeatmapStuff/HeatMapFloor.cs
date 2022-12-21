using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HeatMapFloor : MonoBehaviour
{
    public int hitcounter;
    Material mMaterial;
    MeshRenderer mMeshRenderer;
    ComputeBuffer buffer;

    float[] mPoints;
    int mHitCount;

    float mDelay;

    // Start is called before the first frame update
    void Start()
    {
        mDelay = 3;

        mMeshRenderer = GetComponent<MeshRenderer>();
        mMaterial = mMeshRenderer.material;

        mPoints = new float[3200 * 3]; //32 points

    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnCollisionStay(Collision other)
    {

        if(other.gameObject.tag == "Player")
        {
            hitcounter++;

            if(hitcounter >= 100)
            {
                foreach(ContactPoint cp in other.contacts)
                {
                    //Debug.Log("Had contact with " + cp.otherCollider.gameObject.name);

                    Vector3 StartOfRay = cp.point - cp.normal;
                    Vector3 RayDir = cp.normal;

                    Ray ray = new Ray(StartOfRay, RayDir);
                    RaycastHit hit;
                    Debug.DrawRay(StartOfRay, RayDir, Color.red);
                    bool hitit = Physics.Raycast(ray, out hit, 10f, LayerMask.GetMask("Ground"));

                    if (hitit)
                    {
                        //Debug.Log("Hit object " + hit.collider.gameObject.name);
                        //Debug.Log("Hit texture coordinates = " + hit.textureCoord.x + "," + hit.textureCoord.y);
                        addHitPoint(hit.textureCoord.x*4-2, hit.textureCoord.y*4-2);

                    }


                }

                hitcounter = 0;
            }
        }
    }

    public void addHitPoint(float xp, float yp)
    {
        mPoints[mHitCount * 3] = xp;
        mPoints[mHitCount * 3 + 1] = yp;
        mPoints[mHitCount * 3 + 2] = Random.Range(1f, 3f);

        mHitCount++;
        mHitCount %= 3200;
        //buffer.SetData(mPoints);
        mMaterial.SetFloatArray("_Hits", mPoints);
        //mMaterial.SetBuffer("outHits", buffer);
        mMaterial.SetInt("_HitCount", mHitCount);
        Debug.Log(buffer);
        //if(mHitCount >= 32)
        //{
        //    mHitCount = 0;
        //}
    }

    
}
