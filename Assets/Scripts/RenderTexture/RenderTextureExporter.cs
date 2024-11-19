using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;
using static UnityEngine.UIElements.UxmlAttributeDescription;

public class RenderTextureExporter : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    private void Update()
    {
        if(Input.GetKey(KeyCode.E))
        {
            SaveToPNG();
        }
    }

    public RenderTexture renderTexture;
    [SerializeField] private string saveLocation;
    [SerializeField] private string name;

    void SaveToPNG()
    {
        // Ȱ��ȭ
        RenderTexture.active = renderTexture;

        // Texture2D ����
        Texture2D tex = new Texture2D(renderTexture.width, renderTexture.height, TextureFormat.RGBA32, false);
        tex.ReadPixels(new Rect(0, 0, renderTexture.width, renderTexture.height), 0, 0);
        tex.Apply();

        // RenderTexture ��Ȱ��ȭ
        RenderTexture.active = null;

        // PNG�� ����
        byte[] bytes = tex.EncodeToPNG();
        string fileName = name + ".png";
        string path = System.IO.Path.Combine(Application.dataPath, saveLocation, fileName);
        File.WriteAllBytes(path, bytes);
        Debug.Log("Saved to " + Application.dataPath + "/SavedImage.png");

        // �޸� ����
        Destroy(tex);
    }
}
