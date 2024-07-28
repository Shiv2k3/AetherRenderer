using System.Diagnostics;
using Unity.Collections.LowLevel.Unsafe;
using Unity.Mathematics;

namespace Core.Octree
{
    public unsafe struct Node
    {
        public static Node Invalid => new() { _depth = -1 };

        public int Depth
        {
            readonly get
            {
                ThrowIfDisposed();
                return _depth;
            }
            set
            {
                ThrowIfDisposed();
                _depth = value;
            }
        }
        private int _depth;

        public float3 Position
        {
            readonly get
            {
                ThrowIfDisposed();
                return _position;
            }
            set
            {
                ThrowIfDisposed();
                _position = value;
            }
        }
        private float3 _position;

        public Children Octants
        {
            readonly get
            {
                ThrowIfDisposed();
                return _octants;
            }
            set
            {
                ThrowIfDisposed();
                _octants = value;
            }
        }
        [NativeDisableContainerSafetyRestriction] private Children _octants;

        public readonly bool IsLeaf => _octants.IsEmpty;
        public readonly bool IsValid => _depth > -1;

        [Conditional("UNITY_EDITOR")]
        private readonly void ThrowIfDisposed()
        {
            if (_depth == -1 || math.any(_position == math.NAN))
                throw new("Cannot access disposed Node");
        }

        public Node(in float3 position, in int depth)
        {
            _position = position;
            _depth = depth;
            _octants = Children.Empty;
        }

        public void Divide()
        {
            _octants = SparseOctree.OctantPool.Data.Get();
            var nodes = _octants.nodes;
            int octantDepth = _depth + 1;
            for (int i = 0; i < 8; i++)
            {
                float3 childPosition = IndexPosition.CellPosition(i, 2);
                childPosition *= 1f / math.pow(2, octantDepth) * SparseOctree.World.Data.rootLength;
                childPosition += _position;
                nodes[i] = new(childPosition, octantDepth);
            }
            _octants.nodes = nodes;
        }

        public void Release()
        {
            Position = math.NAN;
            Depth = -1;

            if (!_octants.IsEmpty)
            {
                SparseOctree.OctantPool.Data.Release(ref _octants);
                _octants = Children.Empty;
            }
        }
    }
}