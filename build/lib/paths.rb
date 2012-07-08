COMMANDS = {
  :nuget                    => 'tools/nuget/NuGet.exe',
}

PATHS = {
  :output                   => 'build/artifacts',
  :rake_tasks               => 'build/lib/tasks',

  :main => {
    :assemblies => {
      :templates => {
        :assembly => 'Enjoy.Templates.dll',
        :release_dir => 'src/app/Enjoy.Templates/bin/Release',
        :merge => ['packages/Own.Failure.0.1.0-alpha/lib/net40/Own.Failure.dll'],
      },
      :web => {
        :assembly => 'Enjoy.Templates.Web.dll',
        :release_dir => 'src/app/Enjoy.Templates.Web/bin/Release',
        :merge => ['packages/fasterflect.2.1.0/lib/net40/Fasterflect.dll'],
      },
      :mvc => {
        :assembly => 'Enjoy.Templates.Mvc.dll',
        :release_dir => 'src/app/Enjoy.Templates.Mvc/bin/Release',
        :merge => [
          'packages/fasterflect.2.1.0/lib/net40/Fasterflect.dll',
          'packages/Own.Failure.0.1.0-alpha/lib/net40/Own.Failure.dll',
        ],
      },
    },
    :common_assembly_info   => 'src/CommonAssemblyInfo.cs',
    :nuspec                 => 'Enjoy.Templates.nuspec',
    :solution               => 'src/Enjoy.Templates.sln',
  },
}