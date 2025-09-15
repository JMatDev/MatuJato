using UnityEngine;
using UnityEngine.UIElements;

public class FieldView : MonoBehaviour {
    public float visionAngle = 60f;

    void Start() {
        Mesh myMesh = new Mesh();
        GetComponent<MeshFilter>().mesh = myMesh;

        Vector3[] vertices = new Vector3[3];
        Vector2[] uv = new Vector2[3];
        int[] triangles = new int[3];

        vertices[0] = Vector3.zero;
        vertices[1] = new Vector3(50, 0);
        vertices[2] = new Vector3(0, -50);

        triangles[0] = 0;
        triangles[1] = 1;
        triangles[2] = 2;

        myMesh.vertices = vertices;
        myMesh.uv = uv; 
        myMesh.triangles = triangles;
    }

}
