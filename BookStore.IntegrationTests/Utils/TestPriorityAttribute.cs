namespace BookStore.IntegrationTests.Utils;
[AttributeUsage(AttributeTargets.Method, AllowMultiple = false)]
public class TestPriorityAttribute :Attribute
{
    protected int Priority { get; }
    public TestPriorityAttribute(int priority)
    {
        Priority = priority;
    }

}