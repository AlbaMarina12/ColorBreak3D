using UnityEngine;
using UnityEngine.SceneManagement;

public class MenuNavegacion : MonoBehaviour
{
    public void Jugar()
    {
        SceneManager.LoadScene("Nivel 1");
    }

    public void IrNivel2()
    {
        SceneManager.LoadScene("Nivel 2");
    }

    public void IrMenu()
    {
        SceneManager.LoadScene("Menú principal");
    }
}