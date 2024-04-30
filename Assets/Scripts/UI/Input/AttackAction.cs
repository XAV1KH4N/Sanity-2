using UnityEngine;
using UnityEngine.UI;

public class AttackAction : MonoBehaviour
{
    [SerializeField]
    private PlayerModule playerModule;

    [SerializeField]
    private Button button;

    public void onAttack()
    {
        playerModule.swingSword();
    }
}
