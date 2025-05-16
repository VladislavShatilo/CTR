using UnityEngine;

public static class ComponentValidator
{
    public static bool CheckAndLog<T>(T component, string componentName, MonoBehaviour context, bool disableOnError = true)
    {
        if (component == null)
        {
            Debug.LogError($"{componentName} is missing in {context.gameObject.name}", context.gameObject);
            if (disableOnError)
            {
                context.enabled = false;
            }
            return false;
        }
        return true;
    }
}