using System.IO; // For Path.Combine
using Sharpmake; // Contains the entire Sharpmake object library.

[Generate]
public class GLFWProject : BaseCppProject
{
    public GLFWProject()
    {
        Name = "glfw";
        SourceRootPath = @"[project.SharpmakeCsPath]\src";
        AddTargets(TargetUtil.DefaultTarget);

        SourceFilesExtensions.Clear();

        SourceFiles.Add(new string[]
        {
            "glfw_config.h",
            "context.c",
            "init.c",
            "input.c",
            "monitor.c",
            "vulkan.c",
            "window.c"
        });
    }

    [Configure]
    public new void ConfigureAll(Project.Configuration conf, Target target)
    {
        base.ConfigureAll(conf, target);

        conf.SolutionFolder = Constants.EXTERNAL_FOLDER;

        conf.Output = Project.Configuration.OutputType.Lib;
        conf.TargetPath = @"[project.SharpmakeCsPath]\Out\Bin\[target.Platform]-[target.Optimization]";
        conf.IntermediatePath = @"[project.SharpmakeCsPath]\Out\Intermediate\[target.Platform]-[target.Optimization]";
        conf.IncludePaths.Add(@"[project.SharpmakeCsPath]\include");

        if(target.Platform == Platform.win32 || target.Platform == Platform.win64)
        {
            SourceFiles.Add(new string[]
            {
                "win32_init.c",
                "win32_joystick.c",
                "win32_monitor.c",
                "win32_time.c",
                "win32_thread.c",
                "win32_window.c",
                "wgl_context.c",
                "egl_context.c",
                "osmesa_context.c"
            });

            conf.Defines.Add("_GLFW_WIN32");
            conf.Defines.Add("_CRT_SECURE_NO_WARNINGS");
        }
        else if(target.Platform == Platform.linux)
        {
            SourceFiles.Add(new string[]
            {
                "src/x11_init.c",
                "src/x11_monitor.c",
                "src/x11_window.c",
                "src/xkb_unicode.c",
                "src/posix_time.c",
                "src/posix_thread.c",
                "src/glx_context.c",
                "src/egl_context.c",
                "src/osmesa_context.c",
                "src/linux_joystick.c"
            });

            conf.Defines.Add("_GLFW_X11");
        }
    }
}