using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Assertions;
using Object = UnityEngine.Object;

#if UNITY_EDITOR
using UnityEditor;
#endif

public class DebugEx : Debug
{
    #region Color
    public static void Cyan(object title, object message, Object context = null)
    {
        if(context != null)
            Log($"<color=cyan> {title} :: {message}</color>");
        else
            Log($"<color=cyan> {title} :: {message}</color>", context);
    }
    public static void Black(object title, object message, Object context = null)
    {
        if(context != null)
            Log($"<color=black> {title} :: {message}</color>");
        else
            Log($"<color=black> {title} :: {message}</color>", context);
    }
    public static void Brown(object title, object message, Object context = null)
    {
        if (context != null)
            Log($"<color=brown> {title} :: {message}</color>");
        else
            Log($"<color=brown> {title} :: {message}</color>", context);
    }
    public static void DBlue(object title, object message, Object context = null)
    {
        if(context != null)
            Log($"<color=blue > {title} :: {message}</color>");
        else
            Log($"<color=blue> {title} :: {message}</color>", context);
    }
    public static void Magenta(object title, object message, Object context = null)
    {
        if(context != null)
            Log($"<color=magenta> {title} :: {message}</color>");
        else
            Log($"<color=magenta> {title} :: {message}</color>", context);
    }
    public static void Green(object title, object message, Object context = null)
    {
        if(context != null)
            Log($"<color=green> {title} :: {message}</color>");
        else
            Log($"<color=green> {title} :: {message}</color>", context);
    }
    public static void Gray(object title, object message, Object context = null)
    {
        if(context != null)
            Log($"<color=gray> {title} :: {message}</color>");
        else
            Log($"<color=gray> {title} :: {message}</color>", context);
    }
    public static void LBlue(object title, object message, Object context = null)
    {
        if(context != null)
            Log($"<color=lightblue> {title} :: {message}</color>");
        else
            Log($"<color=lightblue> {title} :: {message}</color>", context);
    }
    public static void Lime(object title, object message, Object context = null)
    {
        if(context != null)
            Log($"<color=lime> {title} :: {message}</color>");
        else
            Log($"<color=lime> {title} :: {message}</color>", context);
    }
    public static void Maroon(object title, object message, Object context = null)
    {
        if(context != null)
            Log($"<color=maroon> {title} :: {message}</color>");
        else
            Log($"<color=maroon> {title} :: {message}</color>", context);
    }
    public static void Navy(object title, object message, Object context = null)
    {
        if(context != null)
            Log($"<color=navy> {title} :: {message}</color>");
        else
            Log($"<color=navy> {title} :: {message}</color>", context);
    }
    public static void Olive(object title, object message, Object context = null)
    {
        if(context != null)
            Log($"<color=olive> {title} :: {message}</color>");
        else
            Log($"<color=olive> {title} :: {message}</color>", context);
    }
    public static void Orange(object title, object message, Object context = null)
    {
        if(context != null)
            Log($"<color=orange> {title} :: {message}</color>");
        else
            Log($"<color=orange> {title} :: {message}</color>", context);
    }
    public static void Purple(object title, object message, Object context = null)
    {
        if(context != null)
            Log($"<color=purple> {title} :: {message}</color>");
        else
            Log($"<color=purple> {title} :: {message}</color>", context);
    }
    public static void Red(object title, object message, Object context = null)
    {
        if(context != null)
            Log($"<color=red> {title} :: {message}</color>");
        else
            Log($"<color=red> {title} :: {message}</color>", context);
    }
    public static void Silver(object title, object message, Object context = null)
    {
        if(context != null)
            Log($"<color=silver> {title} :: {message}</color>");
        else
            Log($"<color=silver> {title} :: {message}</color>", context);
    }
    public static void LogTeal(object title, object message, Object context = null)
    {
        if(context != null)
            Log($"<color=teal> {title} :: {message}</color>");
        else
            Log($"<color=teal> {title} :: {message}</color>", context);
    }
    public static void White(object title, object message, Object context = null)
    {
        if(context != null)
            Log($"<color=white> {title} :: {message}</color>");
        else
            Log($"<color=white> {title} :: {message}</color>", context);
    }
    public static void Yellow(object title, object message, Object context = null)
    {
        if (context != null)
            Log($"<color=yellow> {title} :: {message}</color>");
        else
            Log($"<color=yellow> {title} :: {message}</color>", context);
    }
    public static void Cyan(object message, Object context = null)
    {
        if (context != null)
            Log($"<color=cyan>{message}</color>");
        else
            Log($"<color=cyan>{message}</color>", context);
    }
    public static void Black(object message, Object context = null)
    {
        if (context != null)
            Log($"<color=black>{message}</color>");
        else
            Log($"<color=black>{message}</color>", context);
    }
    public static void Brown(object message, Object context = null)
    {
        if (context != null)
            Log($"<color=brown>{message}</color>");
        else
            Log($"<color=brown>{message}</color>", context);
    }
    public static void DBlue(object message, Object context = null)
    {
        if (context != null)
            Log($"<color=blue >{message}</color>");
        else
            Log($"<color=blue>{message}</color>", context);
    }
    public static void Magenta(object message, Object context = null)
    {
        if (context != null)
            Log($"<color=magenta>{message}</color>");
        else
            Log($"<color=magenta>{message}</color>", context);
    }
    public static void Green(object message, Object context = null)
    {
        if (context != null)
            Log($"<color=green>{message}</color>");
        else
            Log($"<color=green>{message}</color>", context);
    }
    public static void Gray(object message, Object context = null)
    {
        if (context != null)
            Log($"<color=gray>{message}</color>");
        else
            Log($"<color=gray>{message}</color>", context);
    }
    public static void LBlue(object message, Object context = null)
    {
        if (context != null)
            Log($"<color=lightblue>{message}</color>");
        else
            Log($"<color=lightblue>{message}</color>", context);
    }
    public static void Lime(object message, Object context = null)
    {
        if (context != null)
            Log($"<color=lime>{message}</color>");
        else
            Log($"<color=lime>{message}</color>", context);
    }
    public static void Maroon(object message, Object context = null)
    {
        if (context != null)
            Log($"<color=maroon>{message}</color>");
        else
            Log($"<color=maroon>{message}</color>", context);
    }
    public static void Navy(object message, Object context = null)
    {
        if (context != null)
            Log($"<color=navy>{message}</color>");
        else
            Log($"<color=navy>{message}</color>", context);
    }
    public static void Olive(object message, Object context = null)
    {
        if (context != null)
            Log($"<color=olive>{message}</color>");
        else
            Log($"<color=olive>{message}</color>", context);
    }
    public static void Orange(object message, Object context = null)
    {
        if (context != null)
            Log($"<color=orange>{message}</color>");
        else
            Log($"<color=orange>{message}</color>", context);
    }
    public static void Purple(object message, Object context = null)
    {
        if (context != null)
            Log($"<color=purple>{message}</color>");
        else
            Log($"<color=purple>{message}</color>", context);
    }
    public static void Red(object message, Object context = null)
    {
        if (context != null)
            Log($"<color=red>{message}</color>");
        else
            Log($"<color=red>{message}</color>", context);
    }
    public static void Silver(object message, Object context = null)
    {
        if (context != null)
            Log($"<color=silver>{message}</color>");
        else
            Log($"<color=silver>{message}</color>", context);
    }
    public static void LogTeal(object message, Object context = null)
    {
        if (context != null)
            Log($"<color=teal>{message}</color>");
        else
            Log($"<color=teal>{message}</color>", context);
    }
    public static void White(object message, Object context = null)
    {
        if (context != null)
            Log($"<color=white>{message}</color>");
        else
            Log($"<color=white>{message}</color>", context);
    }
    public static void Yellow(object message, Object context = null)
    {
        if (context != null)
            Log($"<color=yellow>{message}</color>");
        else
            Log($"<color=yellow>{message}</color>", context);
    }
    #endregion

    #region Drawing Extensions

    public static void DrawArrow(Vector3 origin, Vector3 direction, Color color, float duration = 0, bool depthTest = true)
    {
        // original Direction
        DrawRay(origin, direction, color, duration, depthTest);
        // Arrow Shapes
        Vector3 right = Quaternion.LookRotation(direction) * Quaternion.Euler(0, 180 + 20, 0) * new Vector3(0, 0, 1);
        Vector3 left = Quaternion.LookRotation(direction) * Quaternion.Euler(0, 180 - 20, 0) * new Vector3(0, 0, 1);
        DrawRay(origin + direction, right * 0.25f, color, duration, depthTest);
        DrawRay(origin + direction, left * 0.25f, color, duration, depthTest);
    }
    public static void DrawCircle(Vector3 center, Vector3 normal, Color color, float radius = 1.0f, float duration = 0, bool depthTest = true)
    {
        
        Vector3 from = Vector3.Cross(normal, Vector3.up) * radius;
        for (int i = 0; i < 361; i++)
        {
            Vector3 to = Quaternion.Euler(0, i, 0) * from;
            DrawLine(center + from, center + to, color, duration, depthTest);
            from = to;
        }
    }

    public static void DrawCone(Vector3 origin, Vector3 direction, Color color, float angle = 45, float duration = 0, bool depthTest = true)
    {
        float length = direction.magnitude;
        Vector3 forward = direction.normalized;
        Vector3 up = Vector3.Slerp(forward, -forward, 0.5f);
        Vector3 right = Vector3.Cross(forward, up).normalized;
        up = Vector3.Cross(forward, right).normalized;

        float circleRadius = length * Mathf.Tan(angle * 0.5f * Mathf.Deg2Rad);
        DrawCircle(origin + forward * length, forward, color, circleRadius, duration, depthTest);
        DrawLine(origin, origin + right * circleRadius + forward * length, color, duration, depthTest);
        DrawLine(origin, origin - right * circleRadius + forward * length, color, duration, depthTest);
        DrawLine(origin, origin + up * circleRadius + forward * length, color, duration, depthTest);
        DrawLine(origin, origin - up * circleRadius + forward * length, color, duration, depthTest);
    }
    public static void DrawBounds(Bounds bounds, Color color, float duration = 0, bool depthTest = true)
    {
        Vector3 min = bounds.min;
        Vector3 max = bounds.max;
        float threshold = 0.001f;
        Vector3[] vertices = {
            new Vector3(min.x, min.y, min.z),
            new Vector3(max.x, min.y, min.z),
            new Vector3(min.x, max.y, min.z),
            new Vector3(min.x, min.y, max.z),
            new Vector3(min.x, max.y, max.z),
            new Vector3(max.x, max.y, min.z),
            new Vector3(max.x, min.y, max.z),
            new Vector3(max.x, max.y, max.z)
        };

        for (int i = 0; i < 8; i++)
        {
            for (int j = i + 1; j < 8; j++)
            {
                if (Math.Abs(Vector3.Distance(vertices[i], vertices[j]) - bounds.size.x) < threshold ||
                    Math.Abs(Vector3.Distance(vertices[i], vertices[j]) - bounds.size.y) < threshold ||
                    Math.Abs(Vector3.Distance(vertices[i], vertices[j]) - bounds.size.z) < threshold)
                {
                    DrawLine(vertices[i], vertices[j], color, duration, depthTest);
                }
            }
        }
    }
    public static void DrawLabel(string text, Vector3 position, Color color)
    {
#if UNITY_EDITOR
    GUIStyle style = new GUIStyle();
    style.normal.textColor = color;
    Handles.Label(position, text, style);
#endif
    }

    public static void DrawCross(Vector3 position, Color color, float size = 1.0f, float duration = 0,
        bool depthTest = true)
    {
        DrawLine(position + Vector3.up * size * 0.5f, position - Vector3.up * size * 0.5f, color, duration, depthTest);
        DrawLine(position + Vector3.right * size * 0.5f, position - Vector3.right * size * 0.5f, color, duration,
            depthTest);
        DrawLine(position + Vector3.forward * size * 0.5f, position - Vector3.forward * size * 0.5f, color, duration,
            depthTest);
    }

    #endregion

    #region AssertEx
    
    public static void AreEqual<T>(T expected, T actual, string message = "")
    {
        UnityEngine.Assertions.Assert.AreEqual(expected, actual, $"Expected value <color=lime>'{expected}'</color>, but got <color=red>'{actual}'</color>. {message}");
    }

    public static void AreNotEqual<T>(T notExpected, T actual, string message = "")
    {
        UnityEngine.Assertions.Assert.AreNotEqual(notExpected, actual, $"Unexpected value <color=red>'{actual}'</color>. {message}");
    }

    public static void IsTrue(bool condition, string message = "")
    {
        UnityEngine.Assertions.Assert.IsTrue(condition, $"Expected condition to be <color=lime>true</color>, but <color=red>it was false</color>. {message}");
    }

    public static void IsFalse(bool condition, string message = "")
    {
        UnityEngine.Assertions.Assert.IsFalse(condition, $"Expected condition to be <color=lime>false</color>, but <color=red>it was true</color>. {message}");
    }

    public static void IsNotNull<T>(T obj, string message = "") where T : UnityEngine.Object
    {
        UnityEngine.Assertions.Assert.IsNotNull(obj, $"Expected object to be <color=lime>not null</color>, but <color=red>it was null.</color> {message}");
    }
    public static void IsNull<T>(T obj, string message = "") where T : UnityEngine.Object
    {
        UnityEngine.Assertions.Assert.IsNull(obj, $"Expected object to <color=lime>be null</color>, but <color=red>it was not.</color> {message}");
    }
    public static void AreApproximatelyEqual(float expect, float actual, string message = "")
    {
        UnityEngine.Assertions.Assert.AreApproximatelyEqual(expect, actual, $"Expect <color=lime>Approximately Equal</color> , but <color=red>it was not equal.</color> {message}");
    }
    public static void AreNotApproximatelyEqual(float expect, float actual, string message = "")
    {
        UnityEngine.Assertions.Assert.AreNotApproximatelyEqual(expect, actual, $"Expect <color=lime>Approximately Not Equal</color> , but <color=red>it was equal.</color> {message}");
    }
    
    #endregion
}

#region Fluent Assertions

public static class FluentAssertEx
{
    public static ObjectAssertions Should(this UnityEngine.Object actual)
    {
        return new ObjectAssertions(actual);
    } 
    public static StringAssertions Should(this string actual)
    {
        return new StringAssertions(actual);
    }
    public static NumericAssertions Should(this int actual)
    {
        return new NumericAssertions(actual);
    }
    public static NumericAssertions Should(this float actual)
    {
        return new NumericAssertions(actual);
    }
    public static VectorAssertions Should(this Vector4 actual)
    {
        return new VectorAssertions(actual);
    }
    public static VectorAssertions Should(this Vector3 actual)
    {
        return new VectorAssertions(actual);
    }
    public static VectorAssertions Should(this Vector2 actual)
    {
        return new VectorAssertions(actual);
    }
    public static VectorAssertions Should(this Vector3Int actual)
    {
        return new VectorAssertions(actual);
    }
    public static VectorAssertions Should(this Vector2Int actual)
    {
        return new VectorAssertions(actual);
    }
    public static BoolAssertions Should(this bool actual)
    {
        return new BoolAssertions(actual);
    }
    public static ListAssertions<T> Should<T>(this List<T> actual)
    {
        return new ListAssertions<T>(actual);
    }
    public static ArrayAssertions<T> Should<T>(this T[] actual)
    {
        return new ArrayAssertions<T>(actual);
    }
    public static DictionaryAssertions<TKey,TValue> Should<TKey,TValue>(this Dictionary<TKey,TValue> actual)
    {
        return new DictionaryAssertions<TKey,TValue>(actual);
    }
    public static GameObjectAssertions Should(this GameObject actual)
    {
        return new GameObjectAssertions(actual);
    }
    public static TransformAssertions Should(this Transform actual)
    {
        return new TransformAssertions(actual);
    } 
    public static LayerMaskAssertions Should(this LayerMask actual)
    {
        return new LayerMaskAssertions(actual);
    }
    public static ColliderAssertion Should(this Collider actual)
    {
        return new ColliderAssertion(actual);
    }
    public static RigidBodyAssertion Should(this Rigidbody actual)
    {
        return new RigidBodyAssertion(actual);
    }
    public static RigidBodyAssertion Should(this Rigidbody2D actual)
    {
        return new RigidBodyAssertion(actual);
    }
}
public class LayerMaskAssertions
{
    private LayerMask actual;

    public LayerMaskAssertions(LayerMask actual)
    {
        this.actual = actual;
    }

    public void HaveLayer(int layer, string message = "")
    {
        Assert.IsTrue(actual.ContainsLayer(layer), $"Expected LayerMask to <color=lime>have layer '{layer}'</color>, but  <color=red>it does not.</color> {message}");
    }

    public void NotHaveLayer(int layer, string message = "")
    {
        Assert.IsFalse(actual.ContainsLayer(layer), $"Expected LayerMask to <color=lime>not have layer '{layer}'</color>, but  <color=red>it does.</color> {message}");
    }
    public void HaveLayer(string name, string message = "")
    {
        int layer = LayerMask.NameToLayer(name);
        Assert.IsTrue(actual.ContainsLayer(layer), $"Expected LayerMask to <color=lime>have layer '{name} : {layer}'</color>, but  <color=red>it does not.</color> {message}");
    }

    public void NotHaveLayer(string name, string message = "")
    {
        int layer = LayerMask.NameToLayer(name);
        Assert.IsFalse(actual.ContainsLayer(layer), $"Expected LayerMask to <color=lime>not have layer '{name} : {layer}'</color>, but  <color=red>it does.</color> {message}");
    }
}
public static class LayerMaskExtensions
{
    public static bool ContainsLayer(this LayerMask layerMask, int layer)
    {
        return (layerMask.value & (1 << layer)) != 0;
    }
}

public class ObjectAssertions
{
    private UnityEngine.Object actual;

    public ObjectAssertions(UnityEngine.Object actual)
    {
        this.actual = actual;
    }

    public void BeNull(string message = "")
    {
        Assert.IsNull(actual, $"Expected object to be <color=lime>null</color>, but <color=red>it was not.</color> {message}");
    }

    public void NotBeNull(string message = "")
    {
        Assert.IsNotNull(actual, $"Expected object to be <color=lime>not null</color>, but <color=red>it was null.</color> {message}");
    }
}
public class StringAssertions
{
    private string actual;

    public StringAssertions(string actual)
    {
        this.actual = actual;
    }

    public void Be(string expected, string message = "")
    {
        Assert.AreEqual(expected, actual, $"Expected value <color=lime>'{expected}'</color>, but got <color=red>'{actual}'</color>. {message}");
    }

    public void NotBe(string notExpected, string message = "")
    {
        Assert.AreNotEqual(notExpected, actual, $"Unexpected value <color=red>'{actual}'</color>. {message}");
    }

    public void BeNullOrEmpty(string message = "")
    {
        Assert.IsTrue(string.IsNullOrEmpty(actual), $"Expected string to be <color=lime>null or empty</color>, but <color=red>it was not</color>. {message}");
    }

    public void NotBeNullOrEmpty(string message = "")
    {
        Assert.IsFalse(string.IsNullOrEmpty(actual), $"Expected string to be <color=lime>not null or empty</color>, but <color=red>it was</color>. {message}");
    }

    public void BeNullOrWhiteSpace(string message = "")
    {
        Assert.IsTrue(string.IsNullOrWhiteSpace(actual), $"Expected string to be <color=lime>null or whitespace</color>, but <color=red>it was not</color>. {message}");
    }

    public void NotBeNullOrWhiteSpace(string message = "")
    {
        Assert.IsFalse(string.IsNullOrWhiteSpace(actual), $"Expected string to be <color=lime>not null or whitespace</color>, but <color=red>it was</color>. {message}");
    }
}

public class NumericAssertions
{
    private int intNumber;
    private float floatingNumber;

    public NumericAssertions(int intNumber)
    {
        this.intNumber = intNumber;
    }
    public NumericAssertions(float actual)
    {
        this.floatingNumber = actual;
    }
/// <summary>
/// when actual Number Type is Integer.
/// </summary>
/// <param name="expected"></param>
/// <param name="message"></param>
    public void Be(int expected, string message = "")
    {
        Assert.AreEqual(expected, intNumber, $"Expected value <color=lime>'{expected}'</color>, but got <color=red>'{intNumber}'</color>. {message}");
    }

    public void NotBe(int notExpected, string message = "")
    {
        Assert.AreNotEqual(notExpected, intNumber, $"Unexpected value <color=red>'{intNumber}'</color>. {message}");
    }
    
    public void BeGreaterThanOrEqualTo(int minValue, string message = "")
    {
        Assert.IsTrue(intNumber >= minValue, $"Expected value to <color=lime>be greater than or equal</color> to <color=lime>'{minValue}'</color>, but got <color=red>'{intNumber}'</color>. {message}");
    }

    public void BeLessThanOrEqualTo(int maxValue, string message = "")
    {
        Assert.IsTrue(intNumber <= maxValue, $"Expected value to <color=lime>be less than or equal</color> to <color=lime>'{maxValue}'</color>, but got <color=red>'{intNumber}'</color>. {message}");
    }

    public void BeInRange(int expectedMin, int expectedMax, string message = "")
    {
        Assert.IsTrue(intNumber >= expectedMin && intNumber <= expectedMax, $"Expected value <color=lime>'{intNumber}' to be in range <color=lime>[{expectedMin}, {expectedMax}]</color>, but <color=red>it is not.</color> {message}");
    }

    /// <summary>
    /// when actual NumberType is Single
    /// </summary>
    /// <param name="expected"></param>
    /// <param name="message"></param>
    public void ApproximatelyBe(float expected, string message = "")
    {
        Assert.AreApproximatelyEqual(expected, floatingNumber, $"Expect <color=lime>Approximately Equal</color> , but <color=red>it was not equal</color>. {message}");
    }

    public void NotApproximatelyBe(float expect, string message = "")
    {
        Assert.AreNotApproximatelyEqual(expect, floatingNumber, $"Expect <color=lime>Approximately Not Equal</color> , but <color=red>it was equal</color>. {message}");
    }
    public void BeGreaterThanOrEqualTo(float minValue, string message = "")
    {
        Assert.IsTrue(floatingNumber >= minValue, $"Expected value to <color=lime>be greater than or equal</color> to <color=lime>'{minValue}'</color>, but got <color=red>'{intNumber}'</color>. {message}");
    }

    public void BeLessThanOrEqualTo(float maxValue, string message = "")
    {
        Assert.IsTrue(floatingNumber <= maxValue, $"Expected value to <color=lime>be less than or equal</color> to <color=lime>'{maxValue}'</color>, but got <color=red>'{intNumber}'</color>. {message}");
    }
    public void BeInRange(float expectedMin, float expectedMax, string message = "")
    {
        Assert.IsTrue(floatingNumber >= expectedMin && floatingNumber <= expectedMax, $"Expected value <color=lime>'{floatingNumber}'</color> to be in range <color=lime>[{expectedMin}, {expectedMax}]</color>, but <color=red>it is not.</color> {message}");
    }

}
public class VectorAssertions
{
    private Vector4 actualVec4;
    private Vector3 actualVec3;
    private Vector2 actualVec2;
    private Vector3Int actualVc3Int;
    private Vector2Int actualVec2Int;

    public VectorAssertions(Vector4 actual)
    {
        this.actualVec4 = actual;
    }
    public VectorAssertions(Vector3 actual)
    {
        this.actualVec3 = actual;
    }
    public VectorAssertions(Vector2 actual)
    {
        this.actualVec2 = actual;
    }
    public VectorAssertions(Vector3Int actual)
    {
        this.actualVc3Int = actual;
    }
    public VectorAssertions(Vector2Int actual)
    {
        this.actualVec2Int = actual;
    }

    public void Be(Vector4 expected, string message = "", float tolerance = 0.0001f)
    {
        Assert.IsTrue(Vector4.Distance(actualVec4, expected) <= tolerance, $"Expected value <color=lime>'{expected}'</color>, but got <color=red>'{actualVec4}'</color>. {message}");
    }

    public void NotBe(Vector4 notExpected, string message = "", float tolerance = 0.0001f)
    {
        Assert.IsFalse(Vector4.Distance(actualVec4, notExpected) <= tolerance, $"Unexpected value <color=red>'{actualVec4}'</color>. {message}");
    }
    public void Be(Vector3 expected, string message = "", float tolerance = 0.0001f)
    {
        Assert.IsTrue(Vector3.Distance(actualVec3, expected) <= tolerance, $"Expected value <color=lime>'{expected}'</color>, but got <color=red>'{actualVec3}'</color>. {message}");
    }

    public void NotBe(Vector3 notExpected, string message = "", float tolerance = 0.0001f)
    {
        Assert.IsFalse(Vector3.Distance(actualVec3, notExpected) <= tolerance, $"Unexpected value <color=red>'{actualVec3}'</color>. {message}");
    }

    public void Be(Vector2 expected, string message = "", float tolerance = 0.0001f)
    {
        Assert.IsTrue(Vector2.Distance(actualVec2, expected) <= tolerance, $"Expected value <color=lime>'{expected}'</color>, but got <color=red>'{actualVec2}'</color>. {message}");
    }

    public void NotBe(Vector2 notExpected, string message = "", float tolerance = 0.0001f)
    {
        Assert.IsFalse(Vector2.Distance(actualVec2, notExpected) <= tolerance, $"Unexpected value <color=red>'{actualVec2}'</color>. {message}");
    }

    public void Be(Vector3Int expected, string message = "", float tolerance = 0.0001f)
    {
        Assert.IsTrue(Vector3Int.Distance(actualVc3Int, expected) <= tolerance, $"Expected value <color=lime>'{expected}'</color>, but got <color=red>'{actualVc3Int}'</color>. {message}");
    }

    public void NotBe(Vector3Int notExpected, string message = "", float tolerance = 0.0001f)
    {
        Assert.IsFalse(Vector3Int.Distance(actualVc3Int, notExpected) <= tolerance, $"Unexpected value <color=red>'{actualVc3Int}'</color>. {message}");
    }

    public void Be(Vector2Int expected, string message = "", float tolerance = 0.0001f)
    {
        Assert.IsTrue(Vector2Int.Distance(actualVec2Int, expected) <= tolerance, $"Expected value <color=lime>'{expected}'</color>, but got <color=red>'{actualVec2Int}'</color>. {message}");
    }

    public void NotBe(Vector2Int notExpected, string message = "", float tolerance = 0.0001f)
    {
        Assert.IsFalse(Vector2Int.Distance(actualVec2Int, notExpected) <= tolerance, $"Unexpected value <color=red>'{actualVec2Int}'</color>. {message}");
    }
}
public class BoolAssertions
{
    private bool actual;

    public BoolAssertions(bool actual)
    {
        this.actual = actual;
    }

    public void BeTrue(string message = "")
    {
        Assert.IsTrue(actual, $"Expected value to <color=lime>be true</color>, but <color=red>it was false.</color> {message}");
    }

    public void BeFalse(string message = "")
    {
        Assert.IsFalse(actual, $"Expected value to <color=lime>be false</color>, but <color=red>it was true.</color> {message}");
    }
}
public class ListAssertions<T>
{
    private List<T> actual;

    public ListAssertions(List<T> actual)
    {
        this.actual = actual;
    }

    public void BeEmpty(string message = "")
    {
        Assert.AreEqual(0, actual.Count, $"Expected list to <color=lime>be empty</color>, but <color=red>it has {actual.Count} items.</color> {message}");
    }

    public void NotBeEmpty(string message = "")
    {
        Assert.AreNotEqual(0, actual.Count, $"Expected list to <color=lime>not be empty</color>, but <color=red>it is.</color> {message}");
    }

    public void Contain(T item, string message = "")
    {
        Assert.IsTrue(actual.Contains(item), $"Expected list to <color=lime>contain '{item}'</color>, but <color=red>it does not.</color> {message}");
    }

    public void NotContain(T item, string message = "")
    {
        Assert.IsFalse(actual.Contains(item), $"Expected list to <color=lime>not contain '{item}'</color>, but <color=red>it does.</color> {message}");
    }

    public void HaveCount(int count, string message = "")
    {
        Assert.AreEqual(count, actual.Count, $"Expected list to <color=lime>have {count} items</color>, but <color=red>it has {actual.Count} items.</color> {message}");
    }
}
public class ArrayAssertions<T>
{
    private T[] actual;

    public ArrayAssertions(T[] actual)
    {
        this.actual = actual;
    }

    public void BeEmpty(string message = "")
    {
        Assert.AreEqual(0, actual.Length, $"Expected array to <color=lime>be empty</color>, but <color=red>it has {actual.Length} items.</color> {message}");
    }

    public void NotBeEmpty(string message = "")
    {
        Assert.AreNotEqual(0, actual.Length, $"Expected array to <color=lime>not be empty</color>, but <color=red>it is.</color> {message}");
    }

    public void Contain(T item, string message = "")
    {
        Assert.IsTrue(Array.Exists(actual, i => i.Equals(item)), $"Expected array to <color=lime>contain '{item}'</color>, but <color=red>it does not.</color> {message}");
    }

    public void NotContain(T item, string message = "")
    {
        Assert.IsFalse(Array.Exists(actual, i => i.Equals(item)), $"Expected array to <color=lime>not contain '{item}'</color>, but <color=red>it does.</color> {message}");
    }

    public void HaveCount(int count, string message = "")
    {
        Assert.AreEqual(count, actual.Length, $"Expected array to <color=lime>have {count} items</color>, but <color=red>it has {actual.Length} items.</color> {message}");
    }
}
public class DictionaryAssertions<K,V>
{
    private Dictionary<K, V> actual;

    public DictionaryAssertions(Dictionary<K, V> actual)
    {
        this.actual = actual;
    }

    public void BeEmpty(string message = "")
    {
        Assert.AreEqual(0, actual.Count, $"Expected dictionary to <color=lime>be empty</color>, but <color=red>it has {actual.Count} items.</color> {message}");
    }

    public void NotBeEmpty(string message = "")
    {
        Assert.AreNotEqual(0, actual.Count, $"Expected dictionary to <color=lime>not be empty</color>, but <color=red>it is.</color> {message}");
    }
    public void ContainKey(K key, string message = "")
    {
        Assert.IsTrue(actual.ContainsKey(key), $"Expected dictionary to <color=lime>contain key '{key}'</color>, but <color=red>it does not.</color> {message}");
    }

    public void NotContainKey(K key, string message = "")
    {
        Assert.IsFalse(actual.ContainsKey(key), $"Expected dictionary to <color=lime>not contain key '{key}'</color>, but <color=red>it does.</color> {message}");
    }
    public void ContainValue(V value, string message = "")
    {
        Assert.IsTrue(actual.ContainsValue(value), $"Expected dictionary to <color=lime>contain value '{value}'</color>, but <color=red>it does not.</color> {message}");
    }

    public void NotContainValue(V value, string message = "")
    {
        Assert.IsFalse(actual.ContainsValue(value), $"Expected dictionary to <color=lime>not contain value '{value}'</color>, but <color=red>it does.</color> {message}");
    }

    public void HaveCount(int count, string message = "")
    {
        Assert.AreEqual(count, actual.Count, $"Expected dictionary to <color=lime>have {count} items</color>, but <color=red>it has {actual.Count} items.</color> {message}");
    }
}

public class GameObjectAssertions
{
    private GameObject actual;

    public GameObjectAssertions(GameObject actual)
    {
        this.actual = actual;
    }

    public void HaveComponent<T>(string message = "") where T : Component
    {
        Assert.IsNotNull(actual.GetComponent<T>(), $"Expected GameObject <color=lime>'{actual.name}'</color> to have component '{typeof(T)}', but <color=red>it does not.</color> {message}");
    }

    public void NotHaveComponent<T>(string message = "") where T : Component
    {
        Assert.IsNull(actual.GetComponent<T>(), $"Expected GameObject <color=lime>'{actual.name}'</color> to not have component '{typeof(T)}', but <color=red>it does.</color>{message}");
    }

    public void BeActive(string message = "")
    {
        Assert.IsTrue(actual.activeSelf, $"Expected GameObject <color=lime>'{actual.name}'</color> to be active, but <color=red>it is not.</color> {message}");
    }

    public void NotBeActive(string message = "")
    {
        Assert.IsFalse(actual.activeSelf, $"Expected GameObject <color=lime>'{actual.name}'</color> to not be active, but <color=red>it is.</color>{message}");
    }
    public void HaveComponentInChildren<T>(string message = "") where T : Component
    {
        Assert.IsNotNull(actual.GetComponentInChildren<T>(), $"Expected GameObject <color=lime>'{actual.name}'</color> to have component '{typeof(T)}' in its children, but <color=red>it does not.</color> {message}");
    }

}
public class TransformAssertions
{
    private Transform actual;

    public TransformAssertions(Transform actual)
    {
        this.actual = actual;
    }

    public void HavePosition(Vector3 expectedPosition, string message = "", float tolerance = 0.0001f)
    {
        Assert.AreEqual(expectedPosition, actual.position, $"Expected Transform <color=lime>'{actual.name}'</color> to have position <color=lime>'{expectedPosition}'</color>, but it has position <color=red>'{actual.position}'</color>. {message}");
    }

    public void HaveRotation(Quaternion expectedRotation, string message = "", float tolerance = 0.0001f)
    {
        Assert.AreEqual(expectedRotation, actual.rotation, $"Expected Transform <color=lime>'{actual.name}'</color> to have rotation <color=lime>'{expectedRotation}'</color>, but it has rotation <color=red>'{actual.rotation}'</color>. {message}");
    }

    public void HaveScale(Vector3 expectedScale, string message = "", float tolerance = 0.0001f)
    {
        Assert.AreEqual(expectedScale, actual.localScale, $"Expected Transform <color=lime>'{actual.name}'</color> to have scale <color=lime>'{expectedScale}'</color>, but it has scale <color=red>'{actual.localScale}'</color>. {message}");
    }
    public void BeWithinDistance(Transform otherTransform, float expectedDistance, string message = "")
    {
        float actualDistance = Vector3.Distance(actual.position, otherTransform.position);
        Assert.IsTrue(actualDistance <= expectedDistance, $"Expected Transform <color=lime>'{actual.name}'</color> to <color=lime>be within {expectedDistance} units</color> of Transform <color=lime>'{otherTransform.name}'</color>, but <color=red>it is {actualDistance} units away.</color> {message}");
    }
}
public class ColliderAssertion
{
    private Collider actual;

    public ColliderAssertion(Collider actual)
    {
        this.actual = actual;
    }

    public void BeTrigger(string message = "")
    {
        Assert.IsTrue(actual.isTrigger, $"Expected Collider <color=lime>'{actual.name}'</color> to be a trigger, but <color=red>it is not.</color> {message}");
    }

    public void BeColliderEnabled(string message = "")
    {
        Assert.IsTrue(actual.enabled, $"Expected Collider <color=lime>'{actual.name}'</color> to be enabled, but <color=red>it is not.</color> {message}");
    }

    public void BeColliderDisabled(string message = "")
    {
        Assert.IsFalse(actual.enabled, $"Expected Collider <color=lime>'{actual.name}'</color> to be disabled, but <color=red>it is not.</color> {message}");
    }

    public void HaveMaterial(Material expectedMaterial, string message = "")
    {
        Assert.AreEqual(expectedMaterial, actual.sharedMaterial, $"Expected Collider <color=lime>'{actual.name}'</color> to have shared material <color=lime>'{expectedMaterial.name}'</color>, but <color=red>it has material '{actual.sharedMaterial.name}'.</color> {message}");
    }

    public void HaveNoMaterial(string message = "")
    {
        Assert.IsNull(actual.sharedMaterial, $"Expected Collider <color=lime>'{actual.name}'</color> to have no shared material, but <color=red>it has material '{actual.sharedMaterial.name}'.</color> {message}");
    }

    public void HaveLayer(int layer, string message = "")
    {
        Assert.AreEqual(layer, actual.gameObject.layer, $"Expected Collider <color=lime>'{actual.name}'</color> to be on layer <color=lime>'{layer}'</color>, but <color=red>it is on layer '{actual.gameObject.layer}'.</color> {message}");
    }
}
public class RigidBodyAssertion
{
    private Rigidbody rb;
    private Rigidbody2D rb2D;

    public RigidBodyAssertion(Rigidbody actual)
    {
        this.rb = actual;
    }

    public RigidBodyAssertion(Rigidbody2D actual)
    {
        this.rb2D = actual;
    }

    public void HaveMass(float expectedMass, string message = "")
    {
        Assert.IsNull(rb, "rb == null");
        Assert.IsTrue(Mathf.Approximately(expectedMass, rb.mass), $"Expected Rigidbody <color=lime>'{rb.name}'</color> to have mass <color=lime>'{expectedMass}'</color>, but <color=red>it has mass '{rb.mass}'.</color> {message}");
    }

    public void BeKinematic(string message = "")
    {
        Assert.IsNull(rb, "rb == null");
        Assert.IsTrue(rb.isKinematic, $"Expected Rigidbody <color=lime>'{rb.name}'</color> to be kinematic, but <color=red>it is not.</color> {message}");
    }

    public void BeDynamic(string message = "")
    {
        Assert.IsNull(rb, "rb == null");
        Assert.IsFalse(rb.isKinematic, $"Expected Rigidbody <color=lime>'{rb.name}'</color> to be dynamic, but <color=red>it is not.</color> {message}");
    }

    public void HaveVelocity(Vector3 expectedVelocity, string message = "", float tolerance = 0.001f)
    {
        Assert.IsNull(rb, "rb == null");
        Assert.IsTrue(Vector3.Distance(expectedVelocity, rb.velocity) < tolerance, $"Expected Rigidbody <color=lime>'{rb.name}'</color> to have velocity <color=lime>'{expectedVelocity}'</color>, but <color=red>it has velocity '{rb.velocity}'.</color> {message}");
    }
    
    // 2D
    public void Have2DMass(float expectedMass, string message = "")
    {
        Assert.IsNull(rb2D, "rb2D == null");
        Assert.IsTrue(Mathf.Approximately(expectedMass, rb2D.mass), $"Expected Rigidbody <color=lime>'{rb2D.name}'</color> to have mass <color=lime>'{expectedMass}'</color>, but <color=red>it has mass '{rb2D.mass}'.</color> {message}");
    }

    public void Be2DKinematic(string message = "")
    {
        Assert.IsNull(rb2D, "rb2D == null");
        Assert.IsTrue(rb2D.isKinematic, $"Expected Rigidbody <color=lime>'{rb2D.name}'</color> to be kinematic, but <color=red>it is not.</color> {message}");
    }

    public void Be2DDynamic(string message = "")
    {
        Assert.IsNull(rb2D, "rb2D == null");
        Assert.IsFalse(rb2D.isKinematic, $"Expected Rigidbody <color=lime>'{rb2D.name}'</color> to be dynamic, but <color=red>it is not.</color> {message}");
    }

    public void Have2DVelocity(Vector3 expectedVelocity, string message = "", float tolerance = 0.001f)
    {
        Assert.IsNull(rb2D, "rb2D == null");
        Assert.IsTrue(Vector3.Distance(expectedVelocity, rb2D.velocity) < tolerance, $"Expected Rigidbody <color=lime>'{rb2D.name}'</color> to have velocity <color=lime>'{expectedVelocity}'</color>, but <color=red>it has velocity '{rb2D.velocity}'.</color> {message}");
    }
}

#endregion;
