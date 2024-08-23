using Xunit.Sdk;
using System.Reflection;
using Newtonsoft.Json.Linq;

namespace Bitrix24RestApiClient.Test.Utilities;

public class JsonFileDataAttribute : DataAttribute
{
    private readonly string filePath;
    private readonly string propertyName;

    /// <summary>
    /// Load data from a JSON file as the data source for a theory
    /// </summary>
    /// <param name="filePath">The absolute or relative path to the JSON file to load</param>
    public JsonFileDataAttribute(string filePath)
        : this(filePath, null) { 
    }

    /// <summary>
    /// Load data from a JSON file as the data source for a theory
    /// </summary>
    /// <param name="filePath">The absolute or relative path to the JSON file to load</param>
    /// <param name="propertyName">The name of the property on the JSON file that contains the data for the test</param>
    public JsonFileDataAttribute(string filePath, string propertyName)
    {
        this.filePath = filePath;
        this.propertyName = propertyName;
    }

    public override IEnumerable<object[]> GetData(MethodInfo testMethod)
    {

        ArgumentNullException.ThrowIfNull(testMethod);

        var path = Path.IsPathRooted(filePath)
            ? filePath
            : Path.GetRelativePath(Directory.GetCurrentDirectory(), filePath);

        if (!File.Exists(path))
            return [];
            // throw new ArgumentException($"Could not find file at path: {path}");

        var fileData = File.ReadAllText(filePath);

        if (string.IsNullOrEmpty(propertyName))
            return GetData(JArray.Parse(fileData), testMethod);

        var allData = JObject.Parse(fileData);
        var data = allData[propertyName];

        return GetData(data, testMethod); 
    }

    private IEnumerable<object[]> GetData(JToken data, MethodInfo testMethod)
    {
        var testDataList = data as JArray;

        foreach (var jToken in testDataList)
        {
            var argsFromJson = (JArray)jToken;
            yield return ParseArgs(argsFromJson, testMethod);
        }
    }


    private object[] ParseArgs(JArray argsFromJson, MethodInfo testMethod)
    {
        var methodParams = testMethod.GetParameters();

        if (methodParams.Length != argsFromJson.Count)
            throw new ArgumentException(
                $"Test data in the file '{filePath}' should contain the same number of objects as the number of the test method arguments. " +
                $"Expected: {methodParams.Length}. " +
                $"Actual: {argsFromJson.Count}");

        var objData = new object[methodParams.Length];
        for (var i = 0; i < methodParams.Length; i++)
        {
            objData[i] = ParseArgs(argsFromJson[i], methodParams[i]);
        }
        return objData;
    }

    private object ParseArgs(JToken argFromJson, ParameterInfo paramInfo)
    {            
        var methodParamType = paramInfo.ParameterType;
        return argFromJson.ToObject(methodParamType);
    }
}