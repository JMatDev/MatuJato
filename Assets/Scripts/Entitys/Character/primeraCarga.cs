using UnityEngine;

public class primeraCarga : MonoBehaviour
{
    public void FrameLevantarse()
    {
        GetComponent<Animator>().SetTrigger("PrimeraCarga");
    }

    public void AnimacionLevantarse()
    {
        GetComponent<Animator>().SetTrigger("Levantarse");
    }
}
