using System.Collections.Generic;
using UnityEngine;

namespace Puzzle
{
    [CreateAssetMenu(menuName = "Custom/FlowerPuzzleCore")]
    public class FlowerPuzzleCore : ScriptableObject, ICore, IPuzzleCore
    {
        public DataReader DataReader { get; private set; } = new FlowerReader();
        private bool _isStart;
        private CubeMap<byte> _puzzle;
        private IMediatorCore _mediator;
        private CubePuzzleReaderForCore _reader;
        private readonly CoreFunction _crossAttack = FlowerCoreFuntions.AttackCross;
        private readonly CoreFunction _dotAttack = FlowerCoreFuntions.AttackDot;
        private readonly CoreFunction _createFlower = FlowerCoreFuntions.CreateFlower;
        private readonly CoreFunction _clearCheck = FlowerCoreFuntions.CheckFlowerNormalStageClear;

        public void InstreamData(byte[] data)
        {
            if (!_isStart)
            {
                return;
            }
            List<byte[]> puzzleMessages = new();
            switch (data[3])
            {
                case 1:
                    _crossAttack(data, _puzzle, out puzzleMessages);
                    break;
                case 2:
                    _dotAttack(data, _puzzle, out puzzleMessages);
                    break;
                case 3:
                    _createFlower(data, _puzzle, out puzzleMessages);
                    break;
                default:
                    return;
            }
            foreach (var message in puzzleMessages)
            {
                _mediator.InstreamDataCore<FlowerReader>(message);
            }

            _clearCheck(data, _puzzle, out var systemMessages);
            foreach (var message in systemMessages)
            {
                _mediator.InstreamDataCore<SystemReader>(message);
            }
        }
        public void Init(CubePuzzleReaderForCore reader)
        {
            _reader = reader;
            _reader.OnStartLevel += (face) => _isStart = true;
            _reader.OnClearLevel += (face) => _isStart = false;
            _puzzle = reader.Map;
        }
        public void SetMediator(IMediatorCore mediator)
        {
            _mediator = mediator;
        }

    }
}
