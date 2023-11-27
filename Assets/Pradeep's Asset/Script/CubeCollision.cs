using System.Collections.Generic;
using UnityEngine;

public class CubeCollision : MonoBehaviour
{
   public Cube cube;
    Color newCubeColor;
    Collider[] surroundedCubes;
    public bool IsCollisionReady = false;

    //private void Awake()
    //{
    //    cube = GetComponent<Cube>();
    //}

    private void OnCollisionEnter(Collision collision)
    {

        Cube otherCube = collision.gameObject.GetComponent<Cube>();
        if (IsCollisionReady)
        {
            cube.CubeRb.constraints = RigidbodyConstraints.None;
            switch (cube.cubeType)
            {
                case CubeType.normal:
                    if (otherCube != null && cube.CubeID > otherCube.CubeID)
                    {

                        if (cube.CubeNumber == otherCube.CubeNumber)
                        {
                            Vector3 contactPoint = collision.contacts[0].point;
                            Score.inst.ScoreAdd(cube.CubeNumber);
                            SoundManager.inst.PlaySound(SoundName.CubeCollision);


                            if (otherCube.CubeNumber < CubeSpawner.Instance.maxCubeNumber)
                            {


                                Cube newCube = CubeSpawner.Instance.Spawn(cube.CubeNumber * 2, contactPoint + Vector3.up * 1.6f);
                                newCube.CubeRb.constraints = RigidbodyConstraints.None;
                                newCube.GetComponent<CubeCollision>().IsCollisionReady = true;
                                newCubeColor = newCube.CubeColor;
                                float pushForce = 2.5f;
                                newCube.CubeRb.AddForce(new Vector3(0, .3f, 1f) * pushForce, ForceMode.Impulse);


                                float randomValue = Random.Range(-20f, 20f);
                                Vector3 randomDirection = Vector3.one * randomValue;
                                newCube.CubeRb.AddTorque(randomDirection);
                            }

                            surroundedCubes = Physics.OverlapSphere(contactPoint, 2f);
                            float explosionForce = 400f;
                            float explosionRadius = 1.5f;

                            foreach (Collider coll in surroundedCubes)
                            {
                                if (coll.attachedRigidbody != null)
                                    coll.attachedRigidbody.AddExplosionForce(explosionForce, contactPoint, explosionRadius);
                            }

                            Fx.Instance.PlayCubeExplosionFX(contactPoint, newCubeColor);

                            CubeSpawner.Instance.DestroyCube(cube);
                            CubeSpawner.Instance.DestroyCube(otherCube);
                        }
                    }
                    break;

                case CubeType.bomb:
                    surroundedCubes = Physics.OverlapSphere(transform.position, 3f);

                    foreach (Collider coll in surroundedCubes)
                    {
                        if (coll.attachedRigidbody != null)
                        {
                            //coll.GetComponent<Cube>().SetCube(CubeType.normal);
                            SoundManager.inst.PlaySound(SoundName.Bomb);
                            Fx.Instance.PlayCubeExplosionFXBomb(coll.gameObject.transform.position);
                            CubeSpawner.Instance.DestroyCube(coll.GetComponent<Cube>());
                            //coll.attachedRigidbody.AddExplosionForce(explosionForce, contactPoint, explosionRadius);
                        }
                    }
                    cube.SetCube(CubeType.normal);
                    break;

                case CubeType.Rainbow:
                    if (otherCube == null)
                    {
                        Fx.Instance.PlayerCubeExplosionFXRainbow(transform.position, cube.CubeColor);
                        cube.SetCube(CubeType.normal);
                        CubeSpawner.Instance.DestroyCube(cube);
                    }
                    else
                    {
                        Vector3 contactPoint = collision.contacts[0].point;
                        Score.inst.ScoreAdd(otherCube.CubeNumber);
                        SoundManager.inst.PlaySound(SoundName.CubeCollision);

                        if (otherCube.CubeNumber < CubeSpawner.Instance.maxCubeNumber)
                        {
                            SoundManager.inst.PlaySound(SoundName.RainBow);
                            Cube newCube = CubeSpawner.Instance.Spawn(otherCube.CubeNumber * 2, contactPoint + Vector3.up * 1.6f);
                            newCubeColor = newCube.CubeColor;
                            float pushForce = 2.5f;
                            newCube.CubeRb.AddForce(new Vector3(0, .3f, 1f) * pushForce, ForceMode.Impulse);


                            float randomValue = Random.Range(-20f, 20f);
                            Vector3 randomDirection = Vector3.one * randomValue;
                            newCube.CubeRb.AddTorque(randomDirection);
                        }

                        // the explosion should affect surrounded cubes too:
                        surroundedCubes = Physics.OverlapSphere(contactPoint, 2f);
                        float explosionForce = 400f;
                        float explosionRadius = 1.5f;

                        foreach (Collider coll in surroundedCubes)
                        {
                            if (coll.attachedRigidbody != null)
                                coll.attachedRigidbody.AddExplosionForce(explosionForce, contactPoint, explosionRadius);
                        }

                        Fx.Instance.PlayCubeExplosionFX(contactPoint, newCubeColor);
                        Fx.Instance.PlayerCubeExplosionFXRainbow(contactPoint, newCubeColor);
                        cube.SetCube(CubeType.normal);
                        CubeSpawner.Instance.DestroyCube(cube);
                        CubeSpawner.Instance.DestroyCube(otherCube);
                    }

                    break;
            }

        }
        IsCollisionReady = true;
        //  //If cube type is normal, then it should do the normal collision
        //  if (cube.cubeType == CubeType.normal)
        //  {
        //      if (otherCube != null && cube.CubeID > otherCube.CubeID)
        //      {

        //          if (cube.CubeNumber == otherCube.CubeNumber)
        //          {
        //              Vector3 contactPoint = collision.contacts[0].point;
        //              Score.inst.ScoreAdd(cube.CubeNumber);
        //              SoundManager.inst.PlaySound(SoundName.CubeCollision);


        //              if (otherCube.CubeNumber < CubeSpawner.Instance.maxCubeNumber)
        //              {


        //                  Cube newCube = CubeSpawner.Instance.Spawn(cube.CubeNumber * 2, contactPoint + Vector3.up * 1.6f);
        //                  newCubeColor = newCube.CubeColor;
        //                  float pushForce = 2.5f;
        //                  newCube.CubeRb.AddForce(new Vector3(0, .3f, 1f) * pushForce, ForceMode.Impulse);


        //                  float randomValue = Random.Range(-20f, 20f);
        //                  Vector3 randomDirection = Vector3.one * randomValue;
        //                  newCube.CubeRb.AddTorque(randomDirection);
        //              }

        //              Collider[] surroundedCubes = Physics.OverlapSphere(contactPoint, 2f);
        //              float explosionForce = 400f;
        //              float explosionRadius = 1.5f;

        //              foreach (Collider coll in surroundedCubes)
        //              {
        //                  if (coll.attachedRigidbody != null)
        //                      coll.attachedRigidbody.AddExplosionForce(explosionForce, contactPoint, explosionRadius);
        //              }

        //              Fx.Instance.PlayCubeExplosionFX(contactPoint, newCubeColor);

        //              CubeSpawner.Instance.DestroyCube(cube);
        //              CubeSpawner.Instance.DestroyCube(otherCube);
        //          }
        //      }

        //  }
        ////  If cube type is bomb, then upon collision it should destroy all cube within range
        //  if (cube.cubeType == CubeType.bomb /*&& otherCube != null*/)
        //  {
        //      Debug.Log("I collide");
        //      Collider[] surroundedCubes = Physics.OverlapSphere(transform.position, 3f);

        //      foreach (Collider coll in surroundedCubes)
        //      {
        //          if (coll.attachedRigidbody != null)
        //          {
        //              //coll.GetComponent<Cube>().SetCube(CubeType.normal);
        //              Fx.Instance.PlayCubeExplosionFXBomb(coll.gameObject.transform.position);
        //              CubeSpawner.Instance.DestroyCube(coll.GetComponent<Cube>());
        //              //coll.attachedRigidbody.AddExplosionForce(explosionForce, contactPoint, explosionRadius);
        //          }
        //      }
        //      cube.SetCube(CubeType.normal);
        //     // cube.ResetCube();
        //   //   CubeSpawner.Instance.DestroyCube(cube);


        //  }

        //  if (cube.cubeType == CubeType.Rainbow && otherCube != null)
        //  {
        //      Vector3 contactPoint = collision.contacts[0].point;
        //      Score.inst.ScoreAdd(otherCube.CubeNumber);
        //      SoundManager.inst.PlaySound(SoundName.CubeCollision);

        //      if (otherCube.CubeNumber < CubeSpawner.Instance.maxCubeNumber)
        //      {
        //          Cube newCube = CubeSpawner.Instance.Spawn(otherCube.CubeNumber * 2, contactPoint + Vector3.up * 1.6f);
        //          newCubeColor = newCube.CubeColor;
        //          float pushForce = 2.5f;
        //          newCube.CubeRb.AddForce(new Vector3(0, .3f, 1f) * pushForce, ForceMode.Impulse);


        //          float randomValue = Random.Range(-20f, 20f);
        //          Vector3 randomDirection = Vector3.one * randomValue;
        //          newCube.CubeRb.AddTorque(randomDirection);
        //      }

        //      // the explosion should affect surrounded cubes too:
        //      Collider[] surroundedCubes = Physics.OverlapSphere(contactPoint, 2f);
        //      float explosionForce = 400f;
        //      float explosionRadius = 1.5f;

        //      foreach (Collider coll in surroundedCubes)
        //      {
        //          if (coll.attachedRigidbody != null)
        //              coll.attachedRigidbody.AddExplosionForce(explosionForce, contactPoint, explosionRadius);
        //      }

        //      Fx.Instance.PlayCubeExplosionFX(contactPoint, newCubeColor);
        //      Fx.Instance.PlayerCubeExplosionFXRainbow(contactPoint, newCubeColor);
        //      cube.SetCube(CubeType.normal);
        //      CubeSpawner.Instance.DestroyCube(cube);
        //      CubeSpawner.Instance.DestroyCube(otherCube);
        //  }    
        //  if (cube.cubeType == CubeType.Rainbow && otherCube == null)
        //  {
        //      Fx.Instance.PlayerCubeExplosionFXRainbow(transform.position, cube.CubeColor);
        //      cube.SetCube(CubeType.normal);
        //      CubeSpawner.Instance.DestroyCube(cube);
        //  }
    }

}


