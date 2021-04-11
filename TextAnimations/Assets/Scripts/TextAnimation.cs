using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

[RequireComponent(typeof(TextMeshProUGUI))]
public class TextAnimation : MonoBehaviour
{
    [SerializeField]
    private float colorChangeTime = 1f;

    private TextMeshProUGUI text;

    private void Start()
    {
        this.text = GetComponent<TextMeshProUGUI>();
        ChangeColors();
    }

    private void ChangeColors()
    {
        // Force the text object to update right away so we can have geometry to modify right from the start.
        this.text.ForceMeshUpdate();

        TMP_TextInfo textInfo = this.text.textInfo;

        for (int i = 0; i < textInfo.characterCount; i++)
        {
            // Get the index of the material used by the current character.
            int materialIndex = textInfo.characterInfo[i].materialReferenceIndex;

            // Get the vertex colors of the mesh used by this text element (character or sprite).
            Color32[] newVertexColors = textInfo.meshInfo[materialIndex].colors32;

            // Get the index of the first vertex used by this text element.
            int vertexIndex = textInfo.characterInfo[i].vertexIndex;

            Color32 color = new Color32((byte)Random.Range(0, 255), (byte)Random.Range(0, 255), (byte)Random.Range(0, 255), 255);

            newVertexColors[vertexIndex + 0] = color;
            newVertexColors[vertexIndex + 1] = color;
            newVertexColors[vertexIndex + 2] = color;
            newVertexColors[vertexIndex + 3] = color;

            // New function which pushes (all) updated vertex data to the appropriate meshes when using either the Mesh Renderer or CanvasRenderer.
            this.text.UpdateVertexData(TMP_VertexDataUpdateFlags.Colors32);
        }

    }
}
