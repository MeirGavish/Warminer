using UnityEngine;
using UnityEngine.SceneManagement;

namespace HOG.GameLogic
{
    public class HOGPlayerActionsComponent : MonoBehaviour
    {
        [SerializeField] private HOGHealthComponent healthComponent;

        private void Awake()
        {
            healthComponent.OnDeathAction = OnDeathAction;
        }
        private void OnDeathAction(GameObject thisGameObject)
        {
            Destroy(gameObject);
            SceneManager.LoadScene("IdleMiner");
        }
    }

}
