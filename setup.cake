#load nuget:?package=Cake.Recipe&version=1.0.0

//////////////////////////////////////////////////////////////////////
// PARAMETERS
//////////////////////////////////////////////////////////////////////

Environment.SetVariableNames();

BuildParameters.SetParameters(
    context: Context, 
    buildSystem: BuildSystem,
    sourceDirectoryPath: "./src",
    title: "BBT.StrategyPattern",
    repositoryOwner: "bbtsoftware",
    repositoryName: "BBT.StrategyPattern",
    appVeyorAccountName: "BBTSoftwareAG",
    shouldPublishMyGet: false,
    shouldRunCodecov: true,
    shouldDeployGraphDocumentation: false);

BuildParameters.PrintParameters(Context);

ToolSettings.SetToolSettings(
    context: Context,
    dupFinderExcludePattern: new string[] { BuildParameters.RootDirectoryPath + "/src/BBT.StrategyPattern.Tests/*.cs" },
    testCoverageFilter: "+[*]* -[xunit.*]* -[*.Tests]* -[Shouldly]*",
    testCoverageExcludeByAttribute: "*.ExcludeFromCodeCoverage*",
    testCoverageExcludeByFile: "*/*Designer.cs;*/*.g.cs;*/*.g.i.cs");

//////////////////////////////////////////////////////////////////////
// EXECUTION
//////////////////////////////////////////////////////////////////////

Build.RunDotNetCore();