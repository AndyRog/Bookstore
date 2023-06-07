using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit.Abstractions;

[assembly:CollectionBehavior(DisableTestParallelization = true)]
namespace BookStore.IntegrationTests.Utils;

public class PrioritizedOrderer : ITestCaseStarting
{
    public ITestCase TestCase => throw new NotImplementedException();

    public ITestMethod TestMethod => throw new NotImplementedException();

    public ITestClass TestClass => throw new NotImplementedException();

    public ITestCollection TestCollection => throw new NotImplementedException();

    public ITestAssembly TestAssembly => throw new NotImplementedException();

    public IEnumerable<ITestCase> TestCases => throw new NotImplementedException();
}
