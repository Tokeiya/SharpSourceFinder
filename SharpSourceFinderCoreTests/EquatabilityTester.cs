﻿using ChainingAssertion;
using System;
using System.Collections.Generic;
using System.Linq;
using Xunit;
using Xunit.Sdk;


namespace SharpSourceFinderCoreTests
{
	/// <summary>
	/// Basic test of arbitrary type's Symmetrically,Transitively,Reflexively and GetHashCode 
	/// </summary>
	/// <typeparam name="T">Specify the test target type.</typeparam>
	public abstract class EquatabilityTester<T> where T : IEquatable<T>
	{
		private static readonly Func<T, T, bool> OpEqualityInvoker;
		private static readonly Func<T, T, bool> OpInequalityInvoker;


		static EquatabilityTester()
		{
			var parameters = new[] { typeof(T), typeof(T) };

			var method = typeof(T).GetMethod("op_Equality", parameters);

			if (!method?.IsSpecialName ?? true) throw new XunitException("op_Equality not found.");
			OpEqualityInvoker = (Func<T, T, bool>)(method?.CreateDelegate(typeof(Func<T, T, bool>)) ?? throw new XunitException("op_Equality not found."));



			method = typeof(T).GetMethod("op_Inequality", parameters);

			if (!method?.IsSpecialName ?? false) throw new XunitException("op_Inequality not found.");
			OpInequalityInvoker = (Func<T, T, bool>)(method?.CreateDelegate(typeof(Func<T, T, bool>)) ?? throw new XunitException("op_Inequality not found."));
		}

		protected abstract IEnumerable<(T x, T y, T z)> CreateTransitivelyTestSamples();
		protected abstract IEnumerable<(T x, T y)> CreateReflexivelyTestSamples();
		protected abstract IEnumerable<T> CreateSymmetricallyTestSamples();
		protected abstract IEnumerable<(T x, T y)> CreateInEqualTestSamples();

		protected abstract IEnumerable<(object x, object y)> CreateObjectEqualSamples();
		protected abstract IEnumerable<(object x, object y)> CreateObjectInEqualSamples();

		[Fact]
		public void TransitivelyTest()
		{
			CreateTransitivelyTestSamples().Any().IsTrue("Sample sequence is empty.");

			foreach ((T x, T y, T z) in CreateTransitivelyTestSamples())
			{
				OpEqualityInvoker(x, y).IsTrue();
				OpEqualityInvoker(y, z).IsTrue();
				OpEqualityInvoker(x, z).IsTrue();

				OpInequalityInvoker(x, y).IsFalse();
				OpInequalityInvoker(y, z).IsFalse();
				OpInequalityInvoker(z, x).IsFalse();

				x.Equals(y).IsTrue();
				y.Equals(z).IsTrue();
				z.Equals(x).IsTrue();

				x.Equals((object)y).IsTrue();
				y.Equals((object)z).IsTrue();
				z.Equals((object)x).IsTrue();
			}
		}

		[Fact]
		public void SymmetricallyTest()
		{
			CreateSymmetricallyTestSamples().Any().IsTrue("Sample sequence is empty.");
			foreach (var elem in CreateSymmetricallyTestSamples())
			{
				OpEqualityInvoker(elem, elem).IsTrue();
				OpInequalityInvoker(elem, elem).IsFalse();

				elem.Equals(elem).IsTrue();
				elem.Equals((object)elem).IsTrue();
			}
		}

		[Fact]
		public void ReflexivelyTest()
		{
			CreateReflexivelyTestSamples().Any().IsTrue("Sample sequence is empty.");
			foreach (var (x, y) in CreateReflexivelyTestSamples())
			{
				OpEqualityInvoker(x, y).IsTrue();
				OpInequalityInvoker(x, y).IsFalse();

				x.Equals(y).IsTrue();
				y.Equals(x).IsTrue();

				x.Equals((object)y).IsTrue();
				y.Equals((object)x).IsTrue();
			}
		}

		[Fact]
		public void GetHashCodeTest()
		{
			foreach (var (x, y) in CreateReflexivelyTestSamples())
			{
				x.GetHashCode().Is(y.GetHashCode());
			}
		}

		[Fact]
		public void InequalityTest()
		{
			CreateInEqualTestSamples().Any().IsTrue("Sequence is empty.");
			foreach (var (x, y) in CreateInEqualTestSamples())
			{
				OpInequalityInvoker(x, y).IsTrue();
				OpEqualityInvoker(y, x).IsFalse();
				OpEqualityInvoker(x, y).IsFalse();


				x.Equals(y).IsFalse();
				y.Equals(x).IsFalse();

				x.Equals((object)y).IsFalse();
				y.Equals((object)x).IsFalse();

			}
		}

		[Fact]
		public void ObjectEqualTest()
		{
			CreateObjectInEqualSamples().Any().IsTrue("Sequence is empty.");
			foreach (var (x, y) in CreateObjectEqualSamples())
			{
				x.Equals(y).IsTrue();
				y.Equals(x).IsTrue();


				//Maybe redundant...
				// ReSharper disable EqualExpressionComparison
				x.Equals(x).IsTrue();
				y.Equals(y).IsTrue();
				// ReSharper restore EqualExpressionComparison
			}
		}

		[Fact]
		public void ObjectInequalityTest()
		{
			CreateObjectInEqualSamples().Any().IsTrue("Sequence is empty.");
			foreach (var (x, y) in CreateObjectInEqualSamples())
			{
				x.Equals(y).IsFalse();
				y.Equals(x).IsFalse();
			}
		}


	}
}
