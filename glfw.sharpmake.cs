using System.IO; // For Path.Combine
using Sharpmake; // Contains the entire Sharpmake object library.

[Generate]
public class HdnCodeExternalGLFWProject : BaseCppProject
{
    public HdnCodeExternalGLFWProject()
    {
        Name = "hdn.code.external.glfw";
        SourceRootPath = @"[project.SharpmakeCsPath]\src";
        AddTargets(TargetUtil.DefaultTarget);
    }

    [Configure]
    public new void ConfigureAll(Project.Configuration conf, Target target)
    {
        base.ConfigureAll(conf, target);

        conf.SolutionFolder = Constants.EXTERNAL_VS_CATEGORY;

        conf.Output = Project.Configuration.OutputType.Lib;
        conf.TargetPath = @"[project.SharpmakeCsPath]\out\bin\[target.Platform]-[target.Optimization]";
        conf.IntermediatePath = @"[project.SharpmakeCsPath]\out\intermediate\[target.Platform]-[target.Optimization]";
        conf.IncludePaths.Add(@"[project.SharpmakeCsPath]\include");

        if(target.Platform == Platform.win32 || target.Platform == Platform.win64)
        {
            conf.SourceFilesBuildExclude.Add("x11_init.c");
            conf.SourceFilesBuildExclude.Add("x11_monitor.c");
            conf.SourceFilesBuildExclude.Add("x11_window.c");
            conf.SourceFilesBuildExclude.Add("linux_joystick.c");

            conf.Defines.Add("_GLFW_WIN32");
            conf.Defines.Add("_CRT_SECURE_NO_WARNINGS");
        }
        else if(target.Platform == Platform.linux)
        {
            conf.SourceFilesBuildExclude.Add("win32_init.c");
            conf.SourceFilesBuildExclude.Add("win32_joystick.c");
            conf.SourceFilesBuildExclude.Add("win32_monitor.c");
            conf.SourceFilesBuildExclude.Add("win32_time.c");
            conf.SourceFilesBuildExclude.Add("win32_thread.c");
            conf.SourceFilesBuildExclude.Add("win32_window.c");

            conf.Defines.Add("_GLFW_X11");
        }
    }
}