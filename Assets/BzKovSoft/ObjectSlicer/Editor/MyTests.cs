using UnityEngine;
using NUnit.Framework;

namespace BzKovSoft.ObjectSlicer
{
	public class MyTests
	{
		[Test]
		public void ColliderTest()
		{
			var go = new GameObject();
			var cldr = go.AddComponent<MeshCollider>();
			go.AddComponent<Rigidbody>();

			var mesh = new Mesh();
			mesh.vertices = new[]
			{
				new Vector3(-1f, -1f, 0f),
				new Vector3(-1f,  1f, 0f),
				new Vector3( 1f,  1f, 0f),
				new Vector3( 1f, -1f, 0.0000001f),
			};

			mesh.triangles = new[]
			{
				0, 1, 2,
				0, 2, 3,

				0, 2, 1,
				0, 3, 2,
			};

			cldr.sharedMesh = mesh;
			cldr.convex = true;
		}
		[Test]
		public void LinePlaneIntersectionTest()
		{
			LinePlaneIntersectionTest2(
				new Vector2(0f, 0f), new Vector2(1f, 0f),
				new Vector2(1f, 1f), new Vector2(1f, -1f));
		}
		[Test]
		public void LinePlaneIntersectionTest2()
		{
			LinePlaneIntersectionTest2(
				new Vector2(0f, 0f), new Vector2(-1f, 0f),
				new Vector2(1f, 1f), new Vector2(1f, -1f));
		}
		[Test]
		public void LinePlaneIntersectionTest3()
		{
			LinePlaneIntersectionTest2(
				new Vector2(0f, 0f), new Vector2(1f, 0f),
				new Vector2(-1f, 1f), new Vector2(1f, -1f));
		}
		[Test]
		public void LinePlaneIntersectionTest4()
		{
			LinePlaneIntersectionTest2(
				new Vector2(11f, -1000f), new Vector2(156f, 0f),
				new Vector2(1f, 634f), new Vector2(1f, -5464f));
		}
		public void LinePlaneIntersectionTest2(Vector2 a1, Vector3 a2, Vector2 b1, Vector3 b2)
		{
			Vector2 etalon;
			LineSegmentsIntersection(out etalon, a1, a2, b1, b2);

			Vector3 intersection;
			LineLineIntersection(out intersection, a1, a2, b1, b2);

			Assert.AreEqual(new Vector3(etalon.x, etalon.y, 0f), intersection);
		}

		public bool LineSegmentsIntersection(out Vector2 intersection, Vector2 a1, Vector3 a2, Vector2 b1, Vector3 b2)
		{
			intersection = new Vector2();

			var d = (a2.x - a1.x) * (b2.y - b1.y) - (a2.y - a1.y) * (b2.x - b1.x);

			if (d != 0.0f)
			{
				var u = ((b1.x - a1.x) * (b2.y - b1.y) - (b1.y - a1.y) * (b2.x - b1.x)) / d;
				//var v = ((b1.x - a1.x) * (a2.y - a1.y) - (b1.y - a1.y) * (a2.x - a1.x)) / d;

				intersection.x = a1.x + u * (a2.x - a1.x);
				intersection.y = a1.y + u * (a2.y - a1.y);
				return true;
			}

			return false;
		}

		public static bool LineLineIntersection(out Vector3 intersection, Vector3 a1, Vector3 a2, Vector3 b1, Vector3 b2)
		{
			Vector3 aDir = a2 - a1;
			Vector3 bDir = b2 - b1;

			Vector3 planePoint = b1;
			Vector3 planeNormal = Vector3.Cross(aDir, bDir);
			planeNormal = Vector3.Cross(bDir, planeNormal);
			intersection = LineIntersection(planePoint, planeNormal, a1, aDir);

			return true;
		}

		/// <summary>
		/// Determines the point of intersection between a plane defined by a point and a normal vector and a line defined by a point and a direction vector.
		/// </summary>
		/// <param name="planePoint">A point on the plane.</param>
		/// <param name="planeNormal">The normal vector of the plane.</param>
		/// <param name="linePoint">A point on the line.</param>
		/// <param name="aDir">The direction vector of the line.</param>
		/// <returns>The point of intersection between the line and the plane, null if the line is parallel to the plane.</returns>
		private static Vector3 LineIntersection(Vector3 planePoint, Vector3 planeNormal, Vector3 linePoint, Vector3 aDir)
		{
			if (Vector3.Dot(planeNormal, aDir) == 0)
			{
				return Vector3.zero;
			}

			float t = (Vector3.Dot(planeNormal, planePoint) - Vector3.Dot(planeNormal, linePoint))
					/ Vector3.Dot(planeNormal, aDir);
			return linePoint + aDir * t;
		}
	}
}