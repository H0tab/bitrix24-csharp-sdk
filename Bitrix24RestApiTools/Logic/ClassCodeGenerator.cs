using System.Text;
using Bitrix24RestApiClient.Core.Models.Response.FieldsResponse;
using Humanizer;

namespace Bitrix24RestApiTools.Logic;

public class ClassCodeGenerator(string className, string description, Dictionary<string, FieldInfo> fields)
{
    public string GenerateModelClass()
    {
        var sb = new StringBuilder();
        sb.AppendLine($$"""
                        #nullable enable
                        using Bitrix24RestApiClient.Models.Core.Attributes;
                        using Bitrix24RestApiClient.Models.Core.Enums;
                        using Newtonsoft.Json;

                        namespace G_{{className}}
                        {
                             /// <summary>
                             /// {{description}}
                             /// </summary>
                             public class {{className}}
                             {
                        """);

        var duplicateFieldNames = GetDuplicatedPropertyNames(fields);

        foreach (var field in fields)
        {
            var propertyName = GetPropertyName(field, duplicateFieldNames);

            sb.AppendLine($"""
                           /// <summary>
                           /// {(field.Value.IsDynamic ? field.Value.ListLabel : field.Value.Title)}
                           /// Тип: {field.Value.TypeExt}
                           """);

            if (field.Value.IsRequired)
                sb.AppendLine("\t\t/// Обязательное поле");

            if (field.Value.IsReadOnly)
                sb.AppendLine("\t\t/// Только для чтения");

            sb.AppendLine($$"""
                            /// </summary>
                            [JsonProperty({{className}}Fields.{{propertyName}})]
                            public {{field.Value.TypeCSharp}} {{propertyName}} {get; set;}
                            """);
        }

        sb.AppendLine("\t}");
        sb.AppendLine("}");
        return sb.ToString();
    }

    public string GenerateFieldNamesClass()
    {
        var sb = new StringBuilder();
        sb.AppendLine($$"""
                        namespace G_{{className}}
                        {
                            public static class {{className}}Fields
                            {
                        """);

        var duplicateFieldNames = GetDuplicatedPropertyNames(fields);

        foreach (var field in fields)
        {
            var propertyName = GetPropertyName(field, duplicateFieldNames);
            var key = field.Value.UpperName ?? field.Key;

            sb.AppendLine($"\t\tpublic const string {propertyName} = \"{key}\";");
        }

        sb.AppendLine("\t}");
        sb.AppendLine("}");
        return sb.ToString();
    }

    public string GenerateContainerClass() =>
        $$"""
          namespace G_{{className}}
          {
              /// <summary>
              /// {{description}}
              /// </summary>
              public class {{className}}s: AbstractEntities<{{className}}>
              {
                  public {{className}}s(IBitrix24Client client) : base(client, EntryPointPrefix.{{className}})
                  {}
              }
          }
          """;

    private static HashSet<string> GetDuplicatedPropertyNames(Dictionary<string, FieldInfo> fields) =>
        fields
            .Select(x => GetPropertyName(x))
            .GroupBy(x => x)
            .Where(x => x.Count() > 1)
            .Select(x => x.Key)
            .ToHashSet();

    private static string GetPropertyName(KeyValuePair<string, FieldInfo> field, HashSet<string>? duplicatedPropertyNames = null)
    {
        var keyName = field.Value.UpperName != null
            ? field.Value.UpperName.Transform(To.LowerCase, To.TitleCase).Dehumanize()
            : field.Key.Transform(To.LowerCase, To.TitleCase).Dehumanize();
        var titleName = field.Value.ListLabel?.Transform(To.LowerCase, To.TitleCase)?.Dehumanize() ?? string.Empty;

        var name = field.Value.IsDynamic
            ? titleName
            : keyName;

        return duplicatedPropertyNames != null && duplicatedPropertyNames.Contains(titleName)
            ? $"{titleName}_{keyName}"
            : name;
    }
}