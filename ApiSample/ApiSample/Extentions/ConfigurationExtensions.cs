using ApiSample.Constants;

namespace ApiSample.Extentions;

public static class ConfigurationExtensions
{
    public static int GetInt32(this IConfiguration configuration, string name)
    {
        var value = configuration[name];
        return int.TryParse(value, out var result) ? result : 0;
    }

    public static bool GetBoolean(this IConfiguration configuration, string name)
    {
        var value = configuration[name];
        return bool.TryParse(value, out var result) && result;
    }

    public static string GetConnectionString(this IConfiguration configuration, string name)
    {
        return configuration.GetSection(ConfigurationSectionNames.ConnectionString)[name] ?? "";
    }

    public static bool IsPgSql(this IConfiguration configuration)
    {
        return ConfigurationSectionNames.DbEnginePgSql.Equals(configuration[ConfigurationSectionNames.DbEngine], StringComparison.OrdinalIgnoreCase);
    }

    public static bool IsMsSql(this IConfiguration configuration) {
        return ConfigurationSectionNames.DbEngineMsSql.Equals(configuration[ConfigurationSectionNames.DbEngine], StringComparison.OrdinalIgnoreCase);
    }

}
