namespace ApiSample.Queries;

public class BaseGetQuery
{
    int Page { get; set; } = 1;
    int Limit { get; set; } = 10;
}
