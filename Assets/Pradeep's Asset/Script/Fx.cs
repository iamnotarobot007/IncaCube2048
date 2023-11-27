using UnityEngine;
using System.Threading.Tasks;

public class Fx : MonoBehaviour
{
    [SerializeField] private ParticleSystem cubeExplosionFX;
    [SerializeField] private ParticleSystem cubeExplosionBombFX;
    [SerializeField] private ParticleSystem cubeExplosionRainbowFX;

    [SerializeField] GameObject bombParticles;

    ParticleSystem.MainModule cubeExplosionFXMainModule;
   // ParticleSystem.MainModule cubeExplosionFXBombMainModule;
    ParticleSystem.MainModule cubeExplosionFXRainbowMainModule;

    public static Fx Instance;

    private void Awake()
    {
        Instance = this;
    }

    private void Start()
    {
        cubeExplosionFXMainModule = cubeExplosionFX.main;
        cubeExplosionFXRainbowMainModule = cubeExplosionRainbowFX.main;
    }

    public void PlayCubeExplosionFX(Vector3 position, Color color)
    {
        cubeExplosionFXMainModule.startColor = new ParticleSystem.MinMaxGradient(new Color(color.r,color.g,color.b,1));
        cubeExplosionFX.transform.position = position;
        cubeExplosionFX.Play();
    }

    public async void PlayCubeExplosionFXBomb(Vector3 position)
    {
        GameObject g = Instantiate(bombParticles, position, Quaternion.identity);
        await Task.Delay(1000);
        Destroy(g);
     
    }

    public void PlayerCubeExplosionFXRainbow(Vector3 position,Color color)
    {
        cubeExplosionFXRainbowMainModule.startColor = new ParticleSystem.MinMaxGradient(new Color(color.r, color.g, color.b, 1));
        cubeExplosionRainbowFX.transform.position = position;
        cubeExplosionRainbowFX.Play();
    }


}