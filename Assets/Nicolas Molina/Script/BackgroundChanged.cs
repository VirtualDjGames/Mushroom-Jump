using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BackgroundChanged : MonoBehaviour
{
    public Camera camara;
    public Color _color0;
    public Color _color1;
    public Color _color2;
    public Color _color3;

    public Transform player;

    public float duracionTransicion = 4f;

    public Transform altura_Nivel_1;
    public Transform altura_Nivel_2;
    public Transform altura_Nivel_3;

    private float tiempoInicioTransicion;

    private void Start()
    {
        camara = GetComponent<Camera>();
        tiempoInicioTransicion = Time.time;
    }

    private void Update()
    {
        float playerPos = player.position.y;
        float pos1 = altura_Nivel_1.position.y;
        float pos2 = altura_Nivel_2.position.y;
        float pos3 = altura_Nivel_3.position.y;

        float tiempoTranscurrido = Time.time - tiempoInicioTransicion;
        float t = Mathf.Clamp01(tiempoTranscurrido / duracionTransicion);

        if (playerPos > pos1 && playerPos < pos2)
        {
            camara.backgroundColor = Color.Lerp(_color0, _color1, t);

        }
        else if (playerPos > pos2 && playerPos < pos3)
        {
            camara.backgroundColor = Color.Lerp(_color1, _color2, t);
        }
        else if (playerPos > pos3)
        {
            camara.backgroundColor = Color.Lerp(_color2, _color3, t);
        }
    }
}

