using ArcherControl;
using Input;
using ArrowControl;
using UnityEngine;

namespace LevelSetup
{
    public class Root : MonoBehaviour
    {
        [SerializeField] private PlayerInputController _inputController;
        [SerializeField] private ArcherPresenter _archer;
        [SerializeField] private ArrowPuller _arrowPuller;
        [SerializeField] private Arrow _arrow;

        private void Awake()
        {
            _archer.Init(new Archer(_inputController, _arrowPuller, _arrow, _archer.transform.position));
        }
    }
}
