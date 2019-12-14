using BzKovSoft.ObjectSlicer.Polygon;
using NUnit.Framework;
using System.Linq;
using UnityEngine;

namespace BzKovSoft.ObjectSlicer
{
	public class BzPolyTests
	{
		class SliceAdapterMock : IBzSliceAdapter
		{
			public bool Check(BzMeshData meshData)
			{
				throw new System.NotImplementedException();
			}

			public Vector3 GetObjectCenterInWorldSpace()
			{
				throw new System.NotImplementedException();
			}

			public Vector3 GetWorldPos(int index)
			{
				throw new System.NotImplementedException();
			}

			public Vector3 GetWorldPos(BzMeshData meshData, int index)
			{
				var v = meshData.Vertices[index];
				return new Vector3(v.x, v.y, v.z);
			}

			public void RebuildMesh(Mesh mesh, Material[] materials, Renderer meshRenderer)
			{
				throw new System.NotImplementedException();
			}
		}

		[Test]
		public void SimpleSquare()
		{
			//Arrange
			var vertices = new Vector2[]
			{
				new Vector2(-1f, -1f),
				new Vector2(-1f,  1f),
				new Vector2( 1f,  1f),
				new Vector2( 1f, -1f),
			};
			int[] outerIndexes = CreateIndexesFromVertices(vertices);
			BzPolyLoop outer = GetLoop(vertices, outerIndexes);
			BzPoly poly = new BzPoly(outer, new BzPolyLoop[0]);

			//Act
			var data = poly.GetMeshData();

			//Assert
			Assert.AreEqual(2, data.triangles.Length / 3);
			CollectionAssert.AreEqual(outerIndexes, data.triangles.Distinct().OrderBy(i => i));
		}

		[Test]
		public void NextVertexOnSameLine()
		{
			//        .
			//       / \
			//     /     \
			//   /         \ upper triangle should not close everyghint
			//   ---     ---
			//      \   /
			//       \ /
			//        *

			//Arrange
			var vertices = new Vector2[]
			{
				new Vector2(  -1f,  0f),
				new Vector2(   0f,  1f),
				new Vector2(   1f,  0f),
				new Vector2( 0.5f,  0f),
				new Vector2(   0f, -1f),
				new Vector2(-0.5f,  0f),
			};
			int[] outerIndexes = CreateIndexesFromVertices(vertices);
			BzPolyLoop outer = GetLoop(vertices, outerIndexes);
			BzPoly poly = new BzPoly(outer, new BzPolyLoop[0]);

			//Act
			var data = poly.GetMeshData();

			//Assert
			Assert.AreEqual(4, data.triangles.Length / 3);
			CollectionAssert.AreEqual(outerIndexes, data.triangles.Distinct().OrderBy(i => i));
		}

		[Test]
		public void SimpleWhole()
		{
			//Arrange
			var vertices = new Vector2[]
			{
				// outer
				new Vector2(-1f, -1f),
				new Vector2(-1f,  1f),
				new Vector2( 1f,  1f),
				new Vector2( 1f, -1f),

				// inner
				new Vector2( 0.5f, -0.5f),
				new Vector2( 0.5f,  0.5f),
				new Vector2(-0.5f,  0.5f),
				new Vector2(-0.5f, -0.5f),
			};
			int[] outerIndexes = new [] {0, 1, 2, 3};
			int[] innerIndexes = new [] {4, 5, 6, 7};
			BzPolyLoop outer = GetLoop(vertices, outerIndexes);
			BzPolyLoop inner = GetLoop(vertices, innerIndexes);
			BzPoly poly = new BzPoly(outer, new BzPolyLoop[] { inner });

			//Act
			var data = poly.GetMeshData();

			//Assert
			Assert.AreEqual(8, data.triangles.Length / 3);
			CollectionAssert.AreEqual(outerIndexes.Union(innerIndexes), data.triangles.Distinct().OrderBy(i => i));
		}

		[Test]
		public void DoNotConnectNearestFromOtherSide()
		{
			// A should not be connected with B
			//
			//                     *
			//                   /   \
			//                /         \
			//             /     __A__     \
			//          /    __--     --__    \
			//       /    *^^^^^^^^^^^^^^^^^*    \
			//    /________________B________________\

			//Arrange
			var vertices = new Vector2[]
			{
				// outer
				new Vector2(   0f,  900f),
				new Vector2( 900f,    0f),
				new Vector2(   0f,   -1f),
				new Vector2(-900f,    0f),

				// inner
				new Vector2(   0f,    1f),
				new Vector2( -10f,    0f),
				new Vector2(  10f,    0f),
			};
			int[] outerIndexes = new [] {0, 1, 2, 3};
			int[] innerIndexes = new [] {4, 5, 6};
			BzPolyLoop outer = GetLoop(vertices, outerIndexes);
			BzPolyLoop inner = GetLoop(vertices, innerIndexes);
			BzPoly poly = new BzPoly(outer, new BzPolyLoop[] { inner });

			//Act
			var data = poly.GetMeshData();

			//Assert
			Assert.AreEqual(7, data.triangles.Length / 3);
			CollectionAssert.AreEqual(outerIndexes.Union(innerIndexes), data.triangles.Distinct().OrderBy(i => i));
		}

		[Test]
		public void TwoWholesWrongOrder()
		{
			// No metter the order of inners, the connections
			// must be created in clockwise order
			//
			//                     *
			//                   /   \
			//                /         \
			//             /  ___     ___  \
			//          /    | 1 |   | 2 |    \
			//       /       |___|   |___|       \
			//    /________________B________________\

			//Arrange
			var vertices = new Vector2[]
			{
				// outer
				new Vector2(   0f,  900f),
				new Vector2( 900f,    0f),
				new Vector2(   0f,   -1f),
				new Vector2(-900f,    0f),

				// inner 1
				new Vector2(-1f, 0f),
				new Vector2(-1f, 1f),
				new Vector2(-2f, 1f),
				new Vector2(-2f, 0f),

				// inner 2
				new Vector2( 1f, 0f),
				new Vector2( 2f, 0f),
				new Vector2( 2f, 1f),
				new Vector2( 1f, 1f),
			};
			int[] outerIndexes  = new [] {0, 1, 2, 3};
			int[] innerIndexes1 = new [] {4, 5, 6, 7};
			int[] innerIndexes2 = new [] {8, 9,10,11};
			BzPolyLoop outer = GetLoop(vertices, outerIndexes);
			BzPolyLoop inner1 = GetLoop(vertices, innerIndexes1);
			BzPolyLoop inner2 = GetLoop(vertices, innerIndexes2);
			BzPoly poly1 = new BzPoly(outer, new BzPolyLoop[] { inner1, inner2 });
			BzPoly poly2 = new BzPoly(outer, new BzPolyLoop[] { inner2, inner1 });

			//Act
			var data1 = poly1.GetMeshData();
			var data2 = poly2.GetMeshData();

			//Assert
			Assert.AreEqual(14, data1.triangles.Length / 3);
			Assert.AreEqual(14, data2.triangles.Length / 3);
			CollectionAssert.AreEqual(outerIndexes.Union(innerIndexes1).Union(innerIndexes2), data1.triangles.Distinct().OrderBy(i => i));
		}

		[Test]
		public void ConnectInnerToInner()
		{
			//           *
			//          / \
			//         /   \
			//        / /^\ \
			//       /  \2/  \
			//      /         \
			//     /    /^\    \
			//    /     \1/     \
			//   /_______B_______\

			//Arrange
			var vertices = new Vector2[]
			{
				// outer
				new Vector2(   0f,  900f),
				new Vector2( 900f,    0f),
				new Vector2(   0f,   -1f),
				new Vector2(-900f,    0f),

				// inner 1
				new Vector2(-1f, 1f),
				new Vector2( 0f, 0f),
				new Vector2( 1f, 1f),
				new Vector2( 0f, 2f),

				// inner 2
				new Vector2(-1f, 4f),
				new Vector2( 0f, 3f),
				new Vector2( 1f, 4f),
				new Vector2( 0f, 5f),
			};
			int[] outerIndexes  = new [] {0, 1, 2, 3};
			int[] innerIndexes1 = new [] {4, 5, 6, 7};
			int[] innerIndexes2 = new [] {8, 9,10,11};
			BzPolyLoop outer = GetLoop(vertices, outerIndexes);
			BzPolyLoop inner1 = GetLoop(vertices, innerIndexes1);
			BzPolyLoop inner2 = GetLoop(vertices, innerIndexes2);
			BzPoly poly = new BzPoly(outer, new BzPolyLoop[] { inner1, inner2 });

			//Act
			poly.GetMeshData();

			//Assert
			Assert.AreEqual(2, poly.outerToInnerConnections.Count);
			var conn1 = poly.outerToInnerConnections[0];
			var conn2 = poly.outerToInnerConnections[1];
			Assert.AreEqual(2, conn1.From);
			Assert.AreEqual(5, conn1.To);
			Assert.AreEqual(7, conn2.From);
			Assert.AreEqual(9, conn2.To);
		}

		private static BzPolyLoop GetLoop(Vector2[] vertices, int[] outerIndexes)
		{
			BzMeshData meshData = GetMeshData(vertices);
			BzPolyLoop outer = GetLoop(outerIndexes, meshData);
			return outer;
		}

		private static int[] CreateIndexesFromVertices(Vector2[] vertices)
		{
			var result = new int[vertices.Length];
			for (int i = 0; i < vertices.Length; i++)
			{
				result[i] = i;
			}
			return result;
		}

		private static BzPolyLoop GetLoop(int[] outerIndexes, BzMeshData meshData)
		{
			return new BzPolyLoop(meshData, new LinkedLoop<int>(
				outerIndexes.ToList()),
				Vector3.back,
				new SliceAdapterMock());
		}

		private static BzMeshData GetMeshData(Vector2[] vertices)
		{
			var mesh = new Mesh();
			mesh.vertices = vertices.Select(v => new Vector3(v.x, v.y, 0f)).ToArray();
			var meshData = new BzMeshData(mesh, null);
			return meshData;
		}
	}
}