using UnityEngine;
using UnityEngine.UI;

[CreateAssetMenu(fileName = "New Color Variable", menuName = "Variable/Color")]
public class ColorData : ScriptableObject
{
    public Color value = Color.white;
    
    public void ChangeImageColor(Image imgObj)
    {
        imgObj.color = value;
    }

    public void ChangeMeshRendererColor(MeshRenderer meshObj)
    {
        meshObj.material.color = value;
    }
    
    public void ChangeSpriteRendererColor(SpriteRenderer spriteObj)
    {
        spriteObj.color = value;
    }
    
    public void ChangeCameraBackgroundColor(Camera cameraObj)
    {
        cameraObj.backgroundColor = value;
    }
    
    public void ChangeParticleColor(ParticleSystem particleSystemObj)
    {
        var mainModule = particleSystemObj.main;
        mainModule.startColor = value;
    }
}
