using UnityEngine;
using System.Collections.Generic;
using System;
using UnityEngine.Profiling;

namespace BzKovSoft.ObjectSlicer.Polygon
{
	class BzPolyLoop
	{
		public readonly LinkedLoop<int> edgeLoop;
		public readonly Vector2[] polyVertices2D;
		public readonly BzMeshData meshData;
		public readonly List<int> newTriangles;
		public bool OuterLoop { get; private set; }
		/// <summary>
		/// True if it is possible to create a polygon
		/// </summary>
		public bool Created { get; private set; }

		/// <param name="vertices">Chain of vertices for polygon</param>
		/// <param name="normal">Normal the polygon is facing to</param>
		public BzPolyLoop(BzMeshData meshData, LinkedLoop<int> edgeLoop, Vector3 normal, IBzSliceAdapter adapter)
		{
			this.meshData = meshData;
			this.edgeLoop = edgeLoop;

			if (edgeLoop.size < 3)
				return;


			Profiler.BeginSample("ConvertV3ToV2");
			polyVertices2D = ConvertV3ToV2(adapter, normal);
			Profiler.EndSample();


			Profiler.BeginSample("MakeMesh");
			var newTriangles1 = MakeMesh(true);
			var newTriangles2 = MakeMesh(false);
			Profiler.EndSample();

			// get triangle list with more vertices
			OuterLoop = newTriangles1.Count > newTriangles2.Count;
			if (OuterLoop)
				newTriangles = newTriangles1;
			else
				newTriangles = newTriangles2;

			if (newTriangles.Count != 0)
			{
				Created = true;
			}
		}

		/// <summary>
		/// Try to make mesh
		/// </summary>
		/// <param name="right">Clockwise if True</param>
		/// <returns>True if polygon was created</returns>
		private List<int> MakeMesh(bool right)
		{
			var newTriangles = new List<int>(polyVertices2D.Length - 2);
			if (polyVertices2D.Length < 3)
				return newTriangles;

			var linkList = new LinkedLoop<int>();
			for (int i = 0; i < polyVertices2D.Length; ++i)
				linkList.AddLast(i);


			var node = linkList.first;
			int counter = 0;
			while (linkList.size > 2 & counter <= linkList.size)
			{
				var node1 = node;
				var node2 = node1.next;
				var node3 = node2.next;

				var i1 = node1.value;
				var i2 = node2.value;
				var i3 = node3.value;

				++counter;

				bool allowed = IsAllowedToCreateTriangle(linkList, i1, i2, i3, right);

				if (allowed)
				{
					CreateTriangle(newTriangles, i1, i2, i3, right);
					node2.Remove();
					node = node3;
					counter = 0;
				}
				else
					node = node2;
			}

			return newTriangles;
		}

		/// <summary>
		/// Transfom vertices from modal space to plane space and convert them to Vector2[]
		/// </summary>
		private Vector2[] ConvertV3ToV2(IBzSliceAdapter adapter, Vector3 normal)
		{
			var rotation = Quaternion.FromToRotation(normal, Vector3.back);
			var v2s = new Vector2[edgeLoop.size];


			var edge = edgeLoop.first;
			for (int i = 0; i < edgeLoop.size; i++)
			{
				Vector3 v3 = adapter.GetWorldPos(meshData, edge.value);
				v3 = rotation * v3;
				v2s[i] = v3;

				edge = edge.next;
			}

			return v2s;
		}

		/// <param name="right">Clockwise if True</param>
		private static void CreateTriangle(List<int> triangles, int i1, int i2, int i3, bool right)
		{
			triangles.Add(i1);

			if (right)
			{
				triangles.Add(i2);
				triangles.Add(i3);
			}
			else
			{
				triangles.Add(i3);
				triangles.Add(i2);
			}
		}

		/// <summary>
		/// Check if triangle in right sequence and other points does not in it
		/// </summary>
		/// <param name="right">Clockwise if True</param>
		private bool IsAllowedToCreateTriangle(LinkedLoop<int> linkList, int i1, int i2, int i3, bool right)
		{
			Vector2 v1 = polyVertices2D[i1];
			Vector2 v2 = polyVertices2D[i2];
			Vector2 v3 = polyVertices2D[i3];

			var node = linkList.first;
			int counter = linkList.size;
			while (counter != 0)
			{
				--counter;

				int i = node.value;
				node = node.next;

				if (i == i1 | i == i2 | i == i3)
					continue;

				var p = polyVertices2D[i];
				bool b1 = PointInTriangle(ref p, ref v1, ref v2, ref v3);
				if (b1)
					return false;
			}

			Vector3 vA = v1 - v2;
			Vector3 vB = v3 - v2;
			Vector3 vC = Vector3.Cross(vB, vA);

			if (right)
				return vC.z < 0;
			else
				return vC.z >= 0;
		}

		/// <summary>
		/// True if point resides inside a triangle
		/// </summary>
		static bool PointInTriangle(ref Vector2 pt, ref Vector2 v1, ref Vector2 v2, ref Vector2 v3)
		{
			float s1 = SideOfLine(ref pt, ref v1, ref v2);
			float s2 = SideOfLine(ref pt, ref v2, ref v3);
			float s3 = SideOfLine(ref pt, ref v3, ref v1);

			return
				(s1 > 0 & s2 > 0 & s3 > 0) |
				(s1 < 0 & s2 < 0 & s3 < 0);
		}

		/// <summary>
		/// It is 0 on the line, and +1 on one side, -1 on the other side.
		/// </summary>
		static float SideOfLine(ref Vector2 p, ref Vector2 a, ref Vector2 b)
		{
			return (b.x - a.x) * (p.y - a.y) - (b.y - a.y) * (p.x - a.x);
		}
	}
}