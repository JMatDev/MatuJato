using UnityEngine;
using System.Collections;
using TMPro;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEditor;

public class LectorGuion : MonoBehaviour
{
    public GameObject prefab_TextBox;
    public float tiempoTransicionDialogos;
    public AnimationCurve curvaMovimientoDialogos;
    public float tiempoAparicionTexto;
    public AnimationCurve curvaAparicionTexto;

    private TMP_Text dialogueText;
    private GameObject inst_TextBox;
    private List<GameObject> instancias;

    public IEnumerator LeerGuion(TextAsset csvFile, GameObject dialoguesBox)
    {
        string[] lines = csvFile.text.Split(new char[] { '\n' });
        for (int i = 1; i < lines.Length - 1; i++)
        {
            
            //por cada linea del guion
            yield return EncanrgarsePrevios(i);
            yield return CrearCajaDialogo(dialoguesBox, i);
            yield return LeerLinea(lines[i]);
        }
        instancias = null;
        yield return null;
    }

    private IEnumerator EncanrgarsePrevios(int index)
    {
        if (index == 1)
        {
            instancias = new List<GameObject>();
            yield break;
        }
        //añadir instancias a la lista
        instancias.Add(inst_TextBox);
        yield return moverCajasDialogo();
    }
    
    private IEnumerator moverCajasDialogo()
    {
        Vector3[] posicionesIniciales = new Vector3[instancias.Count];
        Vector3[] posicionesFinales = new Vector3[instancias.Count];

        for (int i = 0; i < instancias.Count; i++)
        {
            posicionesIniciales[i] = instancias[i].transform.localPosition;
            posicionesFinales[i] = new Vector3(instancias[i].transform.localPosition.x, instancias[i].transform.localPosition.y + 270, instancias[i].transform.localPosition.z);
        }

        float tiempo = 0f;

        while (tiempo < tiempoTransicionDialogos)
        {
            tiempo += Time.unscaledDeltaTime;
            float t = Mathf.Clamp01(tiempo / tiempoTransicionDialogos);
            float curvaT = curvaMovimientoDialogos.Evaluate(t);

            for (int i = 0; i < instancias.Count; i++)
            {
                instancias[i].transform.localPosition = Vector3.LerpUnclamped(posicionesIniciales[i], posicionesFinales[i], curvaT);
            }
            yield return null;
        }

        for (int i = 0; i < instancias.Count; i++)
        {
            instancias[i].transform.localPosition = posicionesFinales[i];
        }
    }

    private IEnumerator CrearCajaDialogo(GameObject dialoguesBox, int index)
    {
        inst_TextBox = Instantiate(prefab_TextBox, dialoguesBox.transform);
        inst_TextBox.transform.localPosition = new Vector3(0, -90, 0);
        dialogueText = inst_TextBox.GetComponentInChildren<TMP_Text>();

        Image pedazoPapel = inst_TextBox.transform.GetChild(0).GetComponent<Image>();
        Image characterImage = inst_TextBox.transform.GetChild(1).GetComponent<Image>();

        if (index == 1)
        {
            //animacion primero
            Color finalColorPapel = pedazoPapel.color;
            Color finalColorCharacter = characterImage.color;
            finalColorPapel.a = 1;
            finalColorCharacter.a = 1;
            pedazoPapel.color = finalColorPapel;
            characterImage.color = finalColorCharacter;
        }
        else
        {
            //animacion aparicion texto (todos menos el primero)
            float tiempo = 0f;
            while (tiempo < tiempoAparicionTexto)
            {
                tiempo += Time.unscaledDeltaTime;
                float t = Mathf.Clamp01(tiempo / tiempoAparicionTexto);
                float curvaT = curvaAparicionTexto.Evaluate(t);

                Color colorPapel = pedazoPapel.color;
                Color colorCharacter = characterImage.color;

                colorPapel.a = Mathf.LerpUnclamped(0, 1, curvaT);
                colorCharacter.a = Mathf.LerpUnclamped(0, 1, curvaT);

                pedazoPapel.color = colorPapel;
                characterImage.color = colorCharacter;

                yield return null;
            }

            Color finalColorPapel = pedazoPapel.color;
            Color finalColorCharacter = characterImage.color;
            finalColorPapel.a = 1;
            finalColorCharacter.a = 1;
            pedazoPapel.color = finalColorPapel;
            characterImage.color = finalColorCharacter;
        }
        yield return null;
    }

    private IEnumerator LeerLinea(string line)
    {
        string[] partes = line.Split(';');
        int numero = int.Parse(partes[0]);
        string texto = partes[1];
        string emocion = partes[2];
        yield return MostrarTextoLinea(texto);
        yield return null;
    }

    private IEnumerator MostrarTextoLinea(string texto)
    {
        dialogueText.text = texto;
        yield return null;
    }
}
