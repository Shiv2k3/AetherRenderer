using System.Diagnostics;
using Unity.Collections.LowLevel.Unsafe;
using Unity.Mathematics;

namespace Core.Octree
{
    public unsafe struct Node
    {
        public static Node Invalid => new() { _depth = -1, _position = math.NAN };

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

        public Children Children
        {
            readonly get
            {
                ThrowIfDisposed();
                return _children;
            }
            set
            {
                ThrowIfDisposed();
                _children = value;
            }
        }
        [NativeDisableContainerSafetyRestriction] private Children _children;

        public readonly bool IsLeaf => _children.IsEmpty;
        public readonly bool IsValid => _depth > -1 && !math.any(_position == math.NAN);

        [Conditional("UNITY_EDITOR")]
        private readonly void ThrowIfDisposed()
        {
            if (_depth < 0 || math.any(_position == math.NAN))
                throw new("Cannot access disposed Node");
        }

        public Node(in float3 position, in int depth)
        {
            _position = position;
            _depth = depth;
            _children = Children.Empty;
        }

        public void Divide(byte activeOctants)
        {
            int totalNodes = math.countbits((int)activeOctants);
            _children = SparseOctree.ChildrenPool.Data.Get(totalNodes);
            int octantDepth = _depth + 1;
            for (int i = 0, index = 0; i < 8; i++)
            {
                int bit = 1 << i;
                if ((activeOctants & bit) == bit)
                {
                    var position = SparseOctree.OctantPosition(i, octantDepth, _position);
                    _children[index] = new(position, octantDepth);
                    index++;
                }
            }
            _children.IsEmpty = false;
        }

        public void Release()
        {
            Position = math.NAN;
            Depth = -1;

            if (!_children.IsEmpty)
            {
                SparseOctree.ChildrenPool.Data.Release(_children);
                _children = Children.Empty;
            }
        }
    }
}