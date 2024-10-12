using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Brick : MonoBehaviour
{
    public MeshRenderer meshRenderer;

    public ColorSO colorData;

    public ColorType colorType;/// <summary>
    ///  dùng để nhận biết màu của gạch và dùng cho so sánh
    /// </summary>
    public void OnInit()
    {
        /// sinh ra màu cho gạch
        colorType = (ColorType)Random.Range(0, 4);

        meshRenderer.material = colorData.GetMaterial(colorType);
        ///
    }
}
