using System.Diagnostics;

namespace ContosoAcai.Application;

public class InstrumentationConfig
{
    private const string ServiceName = "Azure.AI*";

    public static ActivitySource ActivitySource { get; set; } = new(ServiceName);

    public static string Service => ServiceName;
}