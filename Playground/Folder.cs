using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

namespace Playground
{
	//Tree traverse sample.
	public class Folder
	{
		private readonly List<Folder> _children = new List<Folder>();

		public Folder(Folder root, string path)
		{
			Root = root;
			Path = path;
		}

		public string Path { get; }
		public Folder? Root { get; }
		public IEnumerable<Folder> ChildFolders => _children;

		public Folder AddChild(string path)
		{
			var folder = new Folder(this, path);
			_children.Add(folder);
			return folder;
		}

		public IEnumerable<Folder> Descendants()
		{
			yield return this;

			foreach (var elem in _children)
			foreach (var elemChild in elem.Descendants())
				yield return elemChild;
		}

		public IEnumerable<Folder> UseStack()
		{
			var stack = new Stack<Folder>();
			stack.Push(this);

			while (stack.Count != 0)
			{
				var pivot = stack.Pop();
				yield return pivot;

				foreach (var elem in Enumerable.Reverse(pivot._children)) stack.Push(elem);
			}
		}

		public IEnumerable<Folder> FullScratch() => new FolderEnumerable(this);

		class FolderEnumerator : IEnumerator<Folder>
		{
			private readonly Stack<Folder> _stack = new Stack<Folder>();

			public FolderEnumerator(Folder root) => _stack.Push(root);


			public bool MoveNext()
			{
				if (_stack.Count == 0) return false;

				var piv = _stack.Pop();
				Current = piv;

				foreach (var elem in Enumerable.Reverse(piv._children)) _stack.Push(elem);

				return true;
			}

			public void Reset() => throw new NotSupportedException();
#pragma warning disable CS8766
			public Folder? Current { get; private set; }
#pragma warning restore CS8766

			object? IEnumerator.Current => Current;

			public void Dispose()
			{
			}
		}

		class FolderEnumerable : IEnumerable<Folder>
		{
			private readonly Folder _root;

			public FolderEnumerable(Folder root) => _root = root;

			public IEnumerator<Folder> GetEnumerator() => new FolderEnumerator(_root);

			IEnumerator IEnumerable.GetEnumerator() => GetEnumerator();
		}
	}
}