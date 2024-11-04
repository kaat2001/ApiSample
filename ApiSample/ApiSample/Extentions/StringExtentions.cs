namespace ApiSample.Extentions;

public static class StringExtentions
{
    /// <summary>
    ///     Converts the specified string to camelcase.
    /// </summary>
    /// <returns> The specified string converted to camelcase.</returns>
    /// <param name="self">The string to convert to camelcase.</param>
    /// <exception cref="T:System.ArgumentNullException">When <paramref name="self" /> is null.</exception>
    public static string ToCamelCase(this string self)
    {
        if (self.Length > 1)
        {
            var o = self[0];
            var l = char.ToLower(o);
            if (o != l)
            {
                return l + self[1..];
            }
        }

        return self;
    }
}
