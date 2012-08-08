require 'rubygems'
require 'bundler/setup'
require 'require_relative'
require 'fileutils'
require 'albacore'
require 'semver'
require 'rake/clean'
require_relative '../paths'
require_relative '../projects_info'

desc 'Generates the common assembly info.'
task :prepare => ['main:assembly_info']

desc 'Build the project and run the tests.'
task :build => ['main:build']

desc 'Build the project without running the tests.'
task :build_skip_test => ['main:build_skip_test']

namespace :main do

  task :build => [:assembly_info, :msbuild, :merge]
  task :build_skip_test => [:assembly_info, :msbuild, :merge]
  task :merge => [:merge_templates, :merge_web, :merge_mvc]

  assemblyinfo :assembly_info do |asm|
    v = SemVer.find
    asm.version = v.format '%M.%m.%p'
    asm.file_version = v.format '%M.%m.%p'

    asm.product_name = PROJECTS[:main][:product]
    asm.company_name = PROJECTS[:main][:company]
    asm.copyright = PROJECTS[:main][:copyright]

    asm.custom_attributes({
      :AssemblyInformationalVersion => v.format('%M.%m.%p%s')
    })

    asm.output_file = PATHS[:main][:common_assembly_info]
  end

  msbuild :msbuild do |msb, args|
    msb.properties = { :Configuration => :Release }
    msb.targets :Clean, :Build
    msb.solution = PATHS[:main][:solution]
  end

  exec :merge_templates do |cmd|
    out_dir = File.join(PATHS[:output], 'lib', 'net40')
    FileUtils.mkdir_p out_dir

    lib = Dir['packages/**/*.dll'].collect {|path| "/lib:#{File.dirname(path)}"}
    lib = lib + Dir['build/artifacts/**/*.dll'].collect {|path| "/lib:#{File.dirname(path)}"}
    out = File.join(out_dir, PATHS[:main][:assemblies][:templates][:assembly])
    primary = File.join(PATHS[:main][:assemblies][:templates][:release_dir], PATHS[:main][:assemblies][:templates][:assembly])

    cmd.command = 'ilmerge'
    cmd.parameters.concat lib
    cmd.parameters.concat ['/internalize', '/t:library', '/v4', "/out:#{out}", primary]
    cmd.parameters.concat PATHS[:main][:assemblies][:templates][:merge]
  end

  exec :merge_web do |cmd|
    out_dir = File.join(PATHS[:output], 'lib', 'net40')
    FileUtils.mkdir_p out_dir

    lib = Dir['packages/**/*.dll'].collect {|path| "/lib:#{File.dirname(path)}"}
    lib = lib + Dir['build/artifacts/**/*.dll'].collect {|path| "/lib:#{File.dirname(path)}"}
    out = File.join(out_dir, PATHS[:main][:assemblies][:web][:assembly])
    primary = File.join(PATHS[:main][:assemblies][:web][:release_dir], PATHS[:main][:assemblies][:web][:assembly])

    cmd.command = 'ilmerge'
    cmd.parameters.concat lib
    cmd.parameters.concat ['/internalize', '/t:library', '/v4', "/out:#{out}", primary]
    cmd.parameters.concat PATHS[:main][:assemblies][:web][:merge]
  end

  exec :merge_mvc do |cmd|
    out_dir = File.join(PATHS[:output], 'lib', 'net40')
    FileUtils.mkdir_p out_dir

    lib = Dir['packages/**/*.dll'].collect {|path| "/lib:#{File.dirname(path)}"}
    lib = lib + Dir['build/artifacts/**/*.dll'].collect {|path| "/lib:#{File.dirname(path)}"}
    lib = lib + ['/lib:"/Program Files (x86)/Microsoft ASP.NET/ASP.NET MVC 3/Assemblies"']
    lib = lib + ['/lib:"/Program Files (x86)/Microsoft ASP.NET/ASP.NET Web Pages/v1.0/Assemblies"']
    out = File.join(out_dir, PATHS[:main][:assemblies][:mvc][:assembly])
    primary = File.join(PATHS[:main][:assemblies][:mvc][:release_dir], PATHS[:main][:assemblies][:mvc][:assembly])

    cmd.command = 'ilmerge'
    cmd.parameters.concat lib
    cmd.parameters.concat ['/internalize', '/t:library', '/v4', "/out:#{out}", primary]
    cmd.parameters.concat PATHS[:main][:assemblies][:mvc][:merge]
  end

end

CLOBBER.include("src/**/bin")
CLOBBER.include("src/**/obj")
CLOBBER.include(PATHS[:output])
