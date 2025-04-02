using ArcherControl;
using Input;
using Misc;
using UnityEngine;

namespace LevelSetup
{
    public class Root : MonoBehaviour
    {
        [SerializeField] private ArcherPresenter _archer;
        [SerializeField] private ArrowPuller _arrowPuller;
        [SerializeField] private PlayerInputController _inputController;

        private void Awake()
        {
            _archer.Init(new Archer(_inputController, _arrowPuller, _archer.transform.position));
        }
    }
}
