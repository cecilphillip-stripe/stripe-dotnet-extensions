using System.Collections.Generic;
using JetBrains.Annotations;
using Nuke.Common;
using Nuke.Common.IO;
using Nuke.Common.ProjectModel;
using Nuke.Common.Utilities.Collections;
using Nuke.Components;
using Serilog;

namespace ExtensionsBuild;

class Build : NukeBuild, IPack, ITest
{
    public static int Main() => Execute<Build>(x => x.Print);

    // string BaseExProjectName => "Stripe.Extensions";
    //string StripeExDepInjectProjectName => $"{BaseExProjectName}.DependencyInjection";
    //string StripeExAspNetCoreProjectName => $"{BaseExProjectName}.AspNetCore";

    AbsolutePath SourceDirectory => RootDirectory / "src";
    AbsolutePath TestsDirectory => RootDirectory / "tests";
    AbsolutePath SamplesDirectory => RootDirectory / "samples";
    
    
    Target Print => target => target
        .Unlisted()
        .Description("Prints resolved project metadata")
        .Executes(() =>
        {
            var solution = (this as IHazSolution).Solution;
            Log.Information("Solution path = {Value}", solution.Path);
            Log.Information("Solution directory = {Value}", solution.Directory);
        });

    Target CleanSamples => target => target
        .Description("Clean sample projects directories")
        .Executes(() =>
        {
            SamplesDirectory.GlobDirectories("**/{obj,bin}")
                .ForEach(static dir => dir.DeleteDirectory());
        });

    Target CleanTests => target => target
        .Description("Clean test test directories")
        .Executes(() =>
        {
            TestsDirectory.GlobDirectories("**/{obj,bin}")
                .ForEach(static dir => dir.DeleteDirectory());
        });

    Target CleanSource => target => target
        .Description("Clean source projects directories")
        .Executes(() =>
        {
            SourceDirectory.GlobDirectories("**/{obj,bin}")
                .ForEach(static dir => dir.DeleteDirectory());
        });

    [PublicAPI]
    Target Clean => target => target
        .Description("Clean all projects directories")
        .DependsOn(CleanSamples, CleanTests, CleanSource)
        .Executes(() =>
        {
            (this as IHazArtifacts).ArtifactsDirectory
                .CreateOrCleanDirectory();
        });
    
    public IEnumerable<Project> TestProjects => (this as IHazSolution).Solution.GetAllProjects("*.Tests");
}