using Nuke.Common;
using Nuke.Common.IO;
using Nuke.Common.ProjectModel;
using Nuke.Common.Tools.DotNet;
using Nuke.Common.Utilities.Collections;
using static Nuke.Common.Tools.DotNet.DotNetTasks;
using Serilog;

namespace ExtensionsBuild;

class Build : NukeBuild
{
    public static int Main() => Execute<Build>(x => x.Print);

    [Parameter("Configuration to build - Default is 'Debug' (local) or 'Release' (server)")]
    readonly Configuration Configuration = IsLocalBuild ? Configuration.Debug : Configuration.Release;

    [Solution(GenerateProjects = true)] [Required] readonly Solution Solution;

    string BaseExProjectName => "Stripe.Extensions";
    string StripeExDepInjectProjectName => $"{BaseExProjectName}.DependencyInjection";
    string StripeExAspNetCoreProjectName => $"{BaseExProjectName}.AspNetCore";

    AbsolutePath SourceDirectory => RootDirectory / "src";
    AbsolutePath TestsDirectory => RootDirectory / "tests";
    AbsolutePath SamplesDirectory => RootDirectory / "samples";
    AbsolutePath ArtifactsDirectory => RootDirectory / "artifacts";

    Target Print => target => target
        .Unlisted()
        .Description("Prints resolved project metadata")
        .Executes(() =>
        {
            Log.Information("Solution path = {Value}", Solution);
            Log.Information("Solution directory = {Value}", Solution.Directory);
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

    Target Clean => target => target
        .Description("Clean all projects directories")
        .DependsOn(CleanSamples, CleanTests, CleanSource)
        .Executes(() =>
        {
            ArtifactsDirectory.CreateOrCleanDirectory();
        });

    Target Restore => target => target
        .DependsOn(Clean)
        .Executes(() =>
        {
            DotNetRestore(s => s
                .SetProjectFile(Solution));
        });

    Target Compile => target => target
        .DependsOn(Restore)
        .Executes(() =>
        {
            DotNetBuild(s => s
                .SetProjectFile(Solution)
                .SetConfiguration(Configuration)
                .EnableNoRestore());
        });
}