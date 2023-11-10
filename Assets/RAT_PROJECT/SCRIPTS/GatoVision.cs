using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GatoVision : MonoBehaviour
{
    public int raysToCast;
    public int sightRange;
    public float angleOfVision; // in radians

    public Vector3[] vertices;
    private Vector2[] uvs;
    public int[] triangles;

    public Mesh visionConeMesh;
    public MeshFilter meshFilter;

    private float castAngle;
    private float sinX;
    private float cosX;
    private Vector3 dir;
    private Vector3 temp;
    private RaycastHit hit;

    public LayerMask playerLayer;
    public LayerMask obstaculo;
    public Transform Player;
    public float MoveSpeed = 4;



    // Use this for initialization
    void Start()
    {
        vertices = new Vector3[raysToCast + 1];
        uvs = new Vector2[vertices.Length];
        triangles = new int[(vertices.Length * 3) - 9];

        // Set up procedural mesh
        visionConeMesh = new Mesh();
        visionConeMesh.name = "VisionCone";
        meshFilter = GetComponent<MeshFilter>();
        meshFilter.mesh = visionConeMesh;

    }

    // Update is called once per frame
    void Update()
    {
        RaySweep();
    }

    void RaySweep()
    {
        // angle relative to players'/parents' forward vector
        castAngle = -angleOfVision + Mathf.Deg2Rad * transform.eulerAngles.y;

        /// Sweep rays over the cone of vision ///

        // cast rays to map out the space in a cone-shaped sweep
        for (int i = 0; i < raysToCast; i++)
        {
            sinX = sightRange * Mathf.Sin(castAngle);
            cosX = sightRange * Mathf.Cos(castAngle);

            // Increment in proportion to the size of the cone and the number of rays used to map it
            castAngle += 2 * angleOfVision / raysToCast;

            dir = new Vector3(sinX, 0, cosX);

            Debug.DrawRay(transform.position, dir, Color.green); // to aid visualization

            if (Physics.Raycast(transform.position, dir, out hit, sightRange, playerLayer))
            {
                temp = transform.InverseTransformPoint(hit.point);
                //temp = hit.point;
                vertices[i] = new Vector3(temp.x, 0.1f, temp.z);
                Debug.DrawLine(transform.position, hit.point, Color.red); // agregar color
                Debug.Log("¡Jugador detectado!");
                Vector3 posPlayer = new Vector3(Player.position.x, transform.position.y, Player.position.z);
                transform.position = Vector3.MoveTowards(transform.position, posPlayer, MoveSpeed * Time.deltaTime);
            }
            if (Physics.Raycast(transform.position, dir, out hit, sightRange, obstaculo))
            {
                temp = transform.InverseTransformPoint(hit.point);
                //temp = hit.point;
                vertices[i] = new Vector3(temp.x, 0.1f, temp.z);
                Debug.DrawLine(transform.position, hit.point, Color.cyan); // agregar color
                Debug.Log("Muro");
            }

            else
            {
                temp = transform.InverseTransformPoint(transform.position + dir);
                //temp = transform.position + dir;
                vertices[i] = new Vector3(temp.x, 0.1f, temp.z);
            }

        } // end raycast loop

        /// Building/Updating the vision cone mesh ///

        // assign the vertices BEFORE dealing with the uvs and triangles
        visionConeMesh.vertices = vertices;

        // created uvs for mesh
        for (int i = 0; i < vertices.Length; i++)
        {
            uvs[i] = new Vector2(vertices[i].x, vertices[i].z);
        } // end uvs loop

        // create triangles for mesh, with each tri ending at the player's location (like pizza slices)
        int x = -1;
        for (int i = 0; i < triangles.Length; i += 3)
        {
            x++;
            triangles[i] = x + 1;
            triangles[i + 1] = x + 2;
            triangles[i + 2] = vertices.Length - 1; // all triangles end at the centre
        }

        visionConeMesh.triangles = triangles;
        visionConeMesh.uv = uvs;

        //visionConeMesh.RecalculateNormals (); // not sure if this is necessary anymore

    } // end RaySweep

    
    


}

