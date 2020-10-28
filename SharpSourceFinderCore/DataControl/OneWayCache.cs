using System;
using System.Collections.Generic;
using System.Data;
using Tokeiya3.SharpSourceFinderCore.DataControl;

namespace Tokeiya3.SharpSourceFinderCore
{
	internal abstract class OneWayCache<T>:IDisposable where T:class
	{
		private readonly IDbConnection _connection;
		private readonly bool _isShare;

		private OrderControlElement<T> _controller;
		private Dictionary<T, OrderControlElement<T>> _dictionary;

		private OneWayCache(IDbConnection connection,IEqualityComparer<T> comparer, bool isShareConnection,int cacheSize,bool isGotoAhead)
		{
#warning OneWayCache_Is_NotImpl
			throw new NotImplementedException("OneWayCache is not implemented");
		}

		protected abstract T Insert(T element, IDbConnection connection);

		protected abstract T Query(T element, IDbConnection connection);

		public T Query(T element)
		{
#warning Query_Is_NotImpl
			throw new NotImplementedException("Query is not implemented");
		}


		public void Dispose()
		{
#warning Dispose_Is_NotImpl
			throw new NotImplementedException("Dispose is not implemented");
			if (!_isShare) _connection.Dispose();
		}
	}
}