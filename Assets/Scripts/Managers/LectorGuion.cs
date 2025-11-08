using TMPro;
using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using UnityEngine.InputSystem;
using System.Collections.Generic;



public class LectorGuion : MonoBehaviour
{
    public GameObject prefab_TextBox;
    public float tiempoTransicionDialogos;
    public AnimationCurve curvaTransicionDialogos;
    public AnimationCurve curvaAparicionTexto;
    public InputActionReference interact;
    public float velocidadEscritura;


    private TMP_Text dialogueText;
    private GameObject inst_TextBox;
    private List<GameObject> listaCajas;

    

    //singleton pattern
    public static LectorGuion instance;
    private void Awake()
    {
        if (instance == null) instance = this;
    }

    public IEnumerator LeerGuion(TextAsset csvFile, GameObject dialoguesBox)
    {
        string[] lines = csvFile.text.Split(new char[] { '\n' });
        for (int i = 1; i < lines.Length - 1; i++)
        {
            //por cada linea del guion
            yield return moverYcrearNuevo(dialoguesBox, i);
            //yield return EncanrgarsePrevios(i);
            //yield return CrearCajaDialogo(dialoguesBox, i);
            yield return LeerLinea(lines[i]);
        }
        listaCajas = null;
        yield return null;
    }


    private IEnumerator moverYcrearNuevo(GameObject dialoguesBox, int index)
    {
        //si es el primero, inicializar lista y salir
        if (index == 1)
        {
            //crear nueva caja de dialogo
            inst_TextBox = Instantiate(prefab_TextBox, dialoguesBox.transform);
            //posicionar caja
            inst_TextBox.transform.localPosition = new Vector3(0, -90, 0);
            //obtener componentes
            dialogueText = inst_TextBox.GetComponentInChildren<TMP_Text>();
            Image pedazoPapel = inst_TextBox.transform.GetChild(0).GetComponent<Image>();
            Image characterImage = inst_TextBox.transform.GetChild(1).GetComponent<Image>();


            //inicializar lista de cajas
            listaCajas = new List<GameObject> ();
            //animacion primero
            Color finalColorPapel = pedazoPapel.color;
            Color finalColorCharacter = characterImage.color;
            finalColorPapel.a = 1;
            finalColorCharacter.a = 1;
            pedazoPapel.color = finalColorPapel;
            characterImage.color = finalColorCharacter;
            yield break;
        }
        else
        {
            //añadir cajas a la lista
            listaCajas.Add(inst_TextBox);


            //crear nueva caja de dialogo
            inst_TextBox = Instantiate(prefab_TextBox, dialoguesBox.transform);
            //posicionar caja
            inst_TextBox.transform.localPosition = new Vector3(0, -90, 0);
            //obtener componentes
            dialogueText = inst_TextBox.GetComponentInChildren<TMP_Text>();
            Image pedazoPapel = inst_TextBox.transform.GetChild(0).GetComponent<Image>();
            Image characterImage = inst_TextBox.transform.GetChild(1).GetComponent<Image>();
   
   
            //esperar accion del jugador para continuar
            yield return new WaitUntil(() => !interact.action.triggered);
            bool interrumpido = false;
            //posiciones iniciales y finales
            Vector3[] posicionesIniciales = new Vector3[listaCajas.Count];
            Vector3[] posicionesFinales = new Vector3[listaCajas.Count];
            //asignar posiciones
            for (int i = 0; i < listaCajas.Count; i++)
            {
                posicionesIniciales[i] = listaCajas[i].transform.localPosition;
                posicionesFinales[i] = new Vector3(listaCajas[i].transform.localPosition.x, listaCajas[i].transform.localPosition.y + 270, listaCajas[i].transform.localPosition.z);
            }

            float tiempo = 0f;

            while (tiempo < tiempoTransicionDialogos)
            {
                tiempo += Time.unscaledDeltaTime;
                float t = Mathf.Clamp01(tiempo / tiempoTransicionDialogos);
                float curvaTD = curvaTransicionDialogos.Evaluate(t);
                float curvaA = curvaAparicionTexto.Evaluate(t);

                for (int i = 0; i < listaCajas.Count; i++)
                {
                    listaCajas[i].transform.localPosition = Vector3.LerpUnclamped(posicionesIniciales[i], posicionesFinales[i], curvaTD);
                    if (interact.action.triggered)
                    {
                        interrumpido = true;
                        break;
                    }
                }

                Color colorPapel = pedazoPapel.color;
                Color colorCharacter = characterImage.color;

                colorPapel.a = Mathf.LerpUnclamped(0, 1, curvaA);
                colorCharacter.a = Mathf.LerpUnclamped(0, 1, curvaA);

                pedazoPapel.color = colorPapel;
                characterImage.color = colorCharacter;

                if (interrumpido) break;
                yield return null;
            }

            for (int i = 0; i < listaCajas.Count; i++)
            {
                listaCajas[i].transform.localPosition = posicionesFinales[i];
            }
            Color finalColorPapel = pedazoPapel.color;
            Color finalColorCharacter = characterImage.color;
            finalColorPapel.a = 1;
            finalColorCharacter.a = 1;
            pedazoPapel.color = finalColorPapel;
            characterImage.color = finalColorCharacter;
        }
        
    }

    private IEnumerator LeerLinea(string line)
    {
        string[] partes = line.Split(';');
        int numero = int.Parse(partes[0]);
        string texto = partes[1];
        string emocion = partes[2];

        yield return MostrarTextoLinea(texto);
        yield return new WaitUntil(() => !interact.action.triggered);
        //esperar accion del jugador para continuar
        while (!interact.action.triggered)
        yield return null;
    }

    private IEnumerator MostrarTextoLinea(string texto)
    {
        int totalCaracteres = texto.Length;
        int caracteresMostrados = 0;
        float delay = 1f / velocidadEscritura;

        while (caracteresMostrados < totalCaracteres)
        {
            if (interact.action.triggered)
            {
                dialogueText.text = texto; // Asegura que todo el texto esté visible al final
                yield break;
            }
            // Incrementa según velocidad (caracteres por segundo)

            dialogueText.text = texto.Substring(0, caracteresMostrados);
            caracteresMostrados++;

            float timer = 0f;
            while (timer < delay)
            {
                if (interact.action.triggered)
                {
                    dialogueText.text = texto;
                    yield break;
                }
                timer += Time.unscaledDeltaTime;
                yield return null;
            }
            yield return null;
        }
        dialogueText.text = texto; // Asegura que todo el texto esté visible al final
    }
    
    public IEnumerator desaparecerContenedor(GameObject dialogueBox)
    {
        /*
        Image pedazoPapel = dialogueBox.transform.GetChild(0).GetComponent<Image>();
        Image characterImage = dialogueBox.transform.GetChild(1).GetComponent<Image>();

        float tiempo = 0f;
        float duracionDesaparicion = 0.5f;

        Color colorInicialPapel = pedazoPapel.color;
        Color colorInicialCharacter = characterImage.color;

        while (tiempo < duracionDesaparicion)
        {
            tiempo += Time.unscaledDeltaTime;
            float t = Mathf.Clamp01(tiempo / duracionDesaparicion);
            float curvaT = curvaAparicionTexto.Evaluate(t);

            Color colorPapel = pedazoPapel.color;
            Color colorCharacter = characterImage.color;

            colorPapel.a = Mathf.LerpUnclamped(colorInicialPapel.a, 0, curvaT);
            colorCharacter.a = Mathf.LerpUnclamped(colorInicialCharacter.a, 0, curvaT);

            pedazoPapel.color = colorPapel;
            characterImage.color = colorCharacter;

            yield return null;
        }

        Color finalColorPapel = pedazoPapel.color;
        Color finalColorCharacter = characterImage.color;
        finalColorPapel.a = 0;
        finalColorCharacter.a = 0;
        pedazoPapel.color = finalColorPapel;
        characterImage.color = finalColorCharacter;
        */
        yield return null;
        
    }
}
