using UnityEngine;
using System.Collections;
using TMPro;

public class LectorGuion : MonoBehaviour
{
    public GameObject prefab_TextBoxDer;
    public GameObject prefab_TextBoxIzq;

    private GameObject inst_TextBoxDer;
    private GameObject inst_TextBoxIzq;
    private TMP_Text dialogueText;

    public IEnumerator LeerGuion(TextAsset csvFile, GameObject dialogueBox)
    {
        string[] lines = csvFile.text.Split(new char[] { '\n' });

        for (int i = 1; i < lines.Length - 1; i++)
        {
            yield return EncanrgarsePrevios();
            yield return CrearCajaDialogo(dialogueBox, i);
            yield return LeerLinea(lines[i]);
        }
    }

    private IEnumerator EncanrgarsePrevios()
    {
        /*if (inst_TextBoxDer != null)
        {
            Destroy(inst_TextBoxDer);
        }
        if (inst_TextBoxIzq != null)
        {
            Destroy(inst_TextBoxIzq);
        }*/
        yield return null;
    }

    private IEnumerator CrearCajaDialogo(GameObject dialogueBox, int index)
    {
        if (index % 2 == 0)
        {
            inst_TextBoxDer = Instantiate(prefab_TextBoxDer, dialogueBox.transform);
            dialogueText = inst_TextBoxDer.GetComponentInChildren<TMP_Text>();
        }
        else
        {
            inst_TextBoxIzq = Instantiate(prefab_TextBoxIzq, dialogueBox.transform);
            dialogueText = inst_TextBoxIzq.GetComponentInChildren<TMP_Text>();
        }



        // Aquí puedes agregar la lógica para crear o actualizar la caja de diálogo
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
        yield return new WaitForSecondsRealtime(2f);
        yield return null;
    }
}
