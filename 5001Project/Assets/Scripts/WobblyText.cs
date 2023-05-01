using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class WobblyText : MonoBehaviour
{
    public TMP_Text textComponent;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        //makes sure meshes used by tmp are up to date
        textComponent.ForceMeshUpdate();

        var textInfo = textComponent.textInfo;
        //for each character in the string held inside textInfo
        for(int i = 0; i < textInfo.characterCount; i++)
        {
            //store the character info of the first character in the array of characters that make up the string
            var charInfo = textInfo.characterInfo[i];
            //skips over invisible characters
            if(!charInfo.isVisible)
            {
                continue;
            }
            //variable to contain the vertices of char
            var verts = textInfo.meshInfo[charInfo.materialReferenceIndex].vertices;

            for(int j = 0; j < 4; ++j)
            {
                var orig = verts[charInfo.vertexIndex + j];
                verts[charInfo.vertexIndex + j] = orig + new Vector3(0, Mathf.Sin(Time.time * -2f + orig.x * 0.01f) * 5f, 0);

            }

        }

        for (int i = 0; i < textInfo.meshInfo.Length; ++i)
        {
            var meshInfo = textInfo.meshInfo[i];
            meshInfo.mesh.vertices = meshInfo.vertices;
            textComponent.UpdateGeometry(meshInfo.mesh, i);
        }
    }
}
