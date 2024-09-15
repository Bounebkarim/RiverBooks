using ArchUnitNET.Domain;
using ArchUnitNET.Fluent.Syntax.Elements.Types;
using ArchUnitNET.Loader;
using ArchUnitNET.xUnit;
using Xunit;
using Xunit.Abstractions;
using static ArchUnitNET.Fluent.ArchRuleDefinition;

namespace RiverBooks.OrderProcessing.Tests;

public class InfrastructureDependencyTests(ITestOutputHelper outputHelper)
{
  private static readonly Architecture _architecture =
    new ArchLoader()
      .LoadAssemblies(typeof(AssemblyInfo).Assembly)
      .Build();

  [Fact]
  public void DomainTypesShouldNotReferenceInfrastructure()
  {
    var domainTypes = Types()
      .That().ResideInNamespace("RiverBooks.OrderProcessing.Domain.*", useRegularExpressions: true)
      .As("OrderProcessing Domain types");
    var infrastructureTypes = Types()
      .That().ResideInNamespace("RiverBooks.OrderProcessing.Infrastructure.*", useRegularExpressions: true)
      .As("OrderProcessing Infrastructure types");
    var rule = domainTypes.Should().NotDependOnAny(infrastructureTypes);
    PrintTypes(domainTypes, infrastructureTypes);
    rule.Check(_architecture);
  }

  private void PrintTypes(GivenTypesConjunctionWithDescription domainTypes, GivenTypesConjunctionWithDescription infrastructureTypes)
  {
    //Debugging - Inspecting classes and their dependencies
    foreach (var domainClass in domainTypes.GetObjects(_architecture))
    {
      outputHelper.WriteLine($"Domain Types : {domainClass.FullName}");
      foreach (var dependency in domainClass.Dependencies)
      {
        var targetType = dependency.Target;
        if (infrastructureTypes.GetObjects(_architecture).Any(infraClass=>infraClass.FullName == targetType.FullName))
          outputHelper.WriteLine($"  Depends on infrastructure: {targetType.FullName}");
        
      }
    }

    foreach (var iType in infrastructureTypes.GetObjects(_architecture))
    {
      outputHelper.WriteLine($"Infrastructure Types : {iType.FullName}");
    }
  }
}
