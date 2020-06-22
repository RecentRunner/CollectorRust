using System;
using System.Collections.Generic;
using System.Linq;

//Quadtree code
//https://github.com/futurechris/QuadTree

namespace QuadTree
{

    public class QuadTree<T>
    {
        private static Stack<Branch> branchPool = new Stack<Branch>();
        private static Stack<Leaf> leafPool = new Stack<Leaf>();

        private readonly Branch _root;
        private readonly int _splitCount;
        private readonly int _depthLimit;
        private readonly Dictionary<T, Leaf> _leafLookup = new Dictionary<T, Leaf>();


        public QuadTree(int splitCount, int depthLimit, ref Quad region)
        {
            _splitCount = splitCount;
			_depthLimit = depthLimit;
            _root = CreateBranch(this, null, 0, ref region);
        }

		public QuadTree(int splitCount, int depthLimit, Quad region)
            : this(splitCount, depthLimit, ref region)
        {

        }

        public QuadTree(int splitCount, int depthLimit, float x, float y, float width, float height)
            : this(splitCount, depthLimit, new Quad(x, y, x + width, y + height))
        {

        }


        public void Clear()
        {
            _root.Clear();
            _root.Tree = this;
            _leafLookup.Clear();
        }


        public static void ClearPools()
        {
            branchPool = new Stack<Branch>();
            leafPool = new Stack<Leaf>();
        }


        private void Insert(T value, ref Quad quad)
        {
            if (!_leafLookup.TryGetValue(value, out var leaf))
            {
                leaf = CreateLeaf(value, ref quad);
                _leafLookup.Add(value, leaf);
            }
            _root.Insert(leaf);
        }

        public void Insert(T value, Quad quad)
        {
            Insert(value, ref quad);
        }

        public void Insert(T value, float x, float y, float width, float height)
        {
            var quad = new Quad(x, y, x + width, y + height);
            Insert(value, ref quad);
        }

        private bool SearchArea(ref Quad quad, ref List<T> values)
        {
            if (values != null)
                values.Clear();
            else
                values = new List<T>();
            _root.SearchQuad(ref quad, values);
            return values.Count > 0;
        }
        
        public bool SearchArea(Quad quad, ref List<T> values)
        {
            return SearchArea(ref quad, ref values);
        }

        public bool SearchArea(float x, float y, float width, float height, ref List<T> values)
        {
            var quad = new Quad(x, y, x + width, y + height);
            return SearchArea(ref quad, ref values);
        }

        public bool SearchPoint(float x, float y, ref List<T> values)
        {
            if (values != null)
                values.Clear();
            else
                values = new List<T>();
            _root.SearchPoint(x, y, values);
            return values.Count > 0;
        }

        public bool FindCollisions(T value, ref List<T> values)
        {
            if (values != null)
                values.Clear();
            else
                values = new List<T>(_leafLookup.Count);

            if (!_leafLookup.TryGetValue(value, out var leaf)) return false;
            var branch = leaf.Branch;

            //Add the leaf's siblings (prevent it from colliding with itself)
            if (branch.Leaves.Count > 0)
                foreach (var t in branch.Leaves)
                    if (leaf != t && leaf.Quad.Intersects(ref t.Quad))
                        values.Add(t.Value);

            //Add the branch's children
            if (branch.Split)
                for (var i = 0; i < 4; ++i)
                    if (branch.Branches[i] != null)
                        branch.Branches[i].SearchQuad(ref leaf.Quad, values);

            //Add all leaves back to the root
            branch = branch.Parent;
            while (branch != null)
            {
                if (branch.Leaves.Count > 0)
                    foreach (var t in branch.Leaves)
                        if (leaf.Quad.Intersects(ref t.Quad))
                            values.Add(t.Value);

                branch = branch.Parent;
            }
            return false;
        }
        
        public int CountBranches()
        {
            var count = 0;
            CountBranches(_root, ref count);
            return count;
        }

        private static void CountBranches(Branch branch, ref int count)
        {
            ++count;
            if (!branch.Split) return;
            for (var i = 0; i < 4; ++i)
                if (branch.Branches[i] != null)
                    CountBranches(branch.Branches[i], ref count);
        }

        private static Branch CreateBranch(QuadTree<T> tree, Branch parent, int branchDepth, ref Quad quad)
        {
            var branch = branchPool.Count > 0 ? branchPool.Pop() : new Branch();
            branch.Tree = tree;
            branch.Parent = parent;
            branch.Split = false;
			branch.Depth = branchDepth;
            var midX = quad.MinX + (quad.MaxX - quad.MinX) * 0.5f;
            var midY = quad.MinY + (quad.MaxY - quad.MinY) * 0.5f;
            branch.Quads[0].Set(quad.MinX, quad.MinY, midX, midY);
            branch.Quads[1].Set(midX, quad.MinY, quad.MaxX, midY);
            branch.Quads[2].Set(midX, midY, quad.MaxX, quad.MaxY);
            branch.Quads[3].Set(quad.MinX, midY, midX, quad.MaxY);
            return branch;
        }

        private static Leaf CreateLeaf(T value, ref Quad quad)
        {
            var leaf = leafPool.Count > 0 ? leafPool.Pop() : new Leaf();
            leaf.Value = value;
            leaf.Quad = quad;
            return leaf;
        }

        public class Branch
        {
            internal QuadTree<T> Tree;
            internal Branch Parent;
            internal readonly Quad[] Quads = new Quad[4];
            internal readonly Branch[] Branches = new Branch[4];
            internal readonly List<Leaf> Leaves = new List<Leaf>();
            internal bool Split;
			internal int Depth;

            internal void Clear()
            {
                Tree = null;
                Parent = null;
                Split = false;

                for (var i = 0; i < 4; ++i)
                {
                    if (Branches[i] == null) continue;
                    branchPool.Push(Branches[i]);
                    Branches[i].Clear();
                    Branches[i] = null;
                }

                foreach (var t in Leaves)
                {
                    leafPool.Push(t);
                    t.Branch = null;
                    t.Value = default;
                }

                Leaves.Clear();
            }

            internal void Insert(Leaf leaf)
            {
                //If this branch is already split
                if (Split)
                {
                    for (var i = 0; i < 4; ++i)
                    {
                        if (!Quads[i].Contains(ref leaf.Quad)) continue;
                        Branches[i] ??= CreateBranch(Tree, this, Depth + 1, ref Quads[i]);
                        Branches[i].Insert(leaf);
                        return;
                    }

                    Leaves.Add(leaf);
                    leaf.Branch = this;
                }
                else
                {
                    //Add the leaf to this node
                    Leaves.Add(leaf);
                    leaf.Branch = this;

                    //Once I have reached capacity, split the node
                    if (Leaves.Count >= Tree._splitCount && Depth < Tree._depthLimit)
                    {
                        Split = true;
                    }
                }
            }

            internal void SearchQuad(ref Quad quad, List<T> values)
            {
                if (Leaves.Count > 0)
                    foreach (var t in Leaves)
                        if (quad.Intersects(ref t.Quad))
                            values.Add(t.Value);

                for (var i = 0; i < 4; ++i)
                    if (Branches[i] != null)
                        Branches[i].SearchQuad(ref quad, values);
            }

            internal void SearchPoint(float x, float y, List<T> values)
            {
                if (Leaves.Count > 0) values.AddRange(from t in Leaves where t.Quad.Contains(x, y) select t.Value);

                for (var i = 0; i < 4; ++i)
                    if (Branches[i] != null)
                        Branches[i].SearchPoint(x, y, values);
            }
        }

        internal class Leaf
        {
            internal Branch Branch;
            internal T Value;
            internal Quad Quad;
        }
    }


    public struct Quad
    {
        public float MinX;
        public float MinY;
        public float MaxX;
        public float MaxY;
        
        public Quad(float minX, float minY, float maxX, float maxY)
        {
            MinX = minX;
            MinY = minY;
            MaxX = maxX;
            MaxY = maxY;
        }
        
        public void Set(float minX, float minY, float maxX, float maxY)
        {
            MinX = minX;
            MinY = minY;
            MaxX = maxX;
            MaxY = maxY;
        }


        public bool Intersects(ref Quad other)
        {
            return MinX < other.MaxX && MinY < other.MaxY && MaxX > other.MinX && MaxY > other.MinY;
        }

        public bool Contains(ref Quad other)
        {
            return other.MinX >= MinX && other.MinY >= MinY && other.MaxX <= MaxX && other.MaxY <= MaxY;
        }
        
        public bool Contains(float x, float y)
        {
            return x > MinX && y > MinY && x < MaxX && y < MaxY;
        }
    }
}

