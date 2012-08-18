MSBUILD_PATH = "C:/Windows/Microsoft.NET/Framework/v4.0.30319/msbuild.exe"
MSPEC_PATH = "lib/Machine.Specifications.0.5.6.0/tools/mspec-clr4.exe"
#MSTEST_PATH = "\"" + ENV['VS90COMNTOOLS'].gsub("Tools","IDE") + "mstest.exe\""
BUILD_PATH = File.expand_path('build')
DEPLOY_PATH = File.expand_path('deploy')
REPORTS_PATH = File.expand_path('testresults')
SOLUTION = "src/BlingBag.sln"
TRXFILE = File.join(REPORTS_PATH, SOLUTION + '.trx')
CONFIG = "Debug"

task :default => [:all]

task :all => [:prepare, :compile, :tests, :deploy ]

task :prepare do
	require 'fileutils'
	FileUtils.rm_rf BUILD_PATH
	end
	
task :compile do
	puts 'Compiling solution...'
	sh "#{MSBUILD_PATH} /p:Configuration=#{CONFIG} /p:OutDir=\"#{BUILD_PATH}/\" /p:PostBuildEvent=\"\" #{SOLUTION}"
end

task :deploy do
	puts 'Preparing deployment...'
	mkdir DEPLOY_PATH unless File.exist?(DEPLOY_PATH)
	cp BUILD_PATH + "/DomainEvents.dll", DEPLOY_PATH
	cp BUILD_PATH + "/DomainEvents.Testing.dll", DEPLOY_PATH
	cp BUILD_PATH + "/DomainEvents.StructureMap.dll", DEPLOY_PATH
end

task :tests do
    puts "Locating and running MSpec tests in #{BUILD_PATH}."
    puts 'This can take a while, please wait...'
    
    # Create output directory if necessary
    mkdir REPORTS_PATH unless File.exist?(REPORTS_PATH)
    
    # Clear out old reports, but don't talk about it.
    verbose(false) do
        FileList.new("#{REPORTS_PATH}/*").each { |file| rm file }
    end
    
    # Find all the specs DLLs
    testDlls = FileList.new("#{BUILD_PATH}/*.Specs.dll")
    
    # Join the file list into a single string with quotes surrounding the file paths
    TEST_DLL_LIST = '"' + testDlls.join('" "') + '"'
    
    # Specify our mspec command line parameters
    HTML_SWITCH = "--html \"#{REPORTS_PATH}\""
    XML_SWITCH = "--xml \"#{REPORTS_PATH}\\testresults.xml\""
    
    # Run the tests and give a message on failure
    # But we don't want to hear the chatter, so dump the console output to a file
    File.new("#{REPORTS_PATH}/mspec.output.txt",'w') << `#{MSPEC_PATH} #{HTML_SWITCH} #{XML_SWITCH} #{TEST_DLL_LIST}`
    result = $?.to_i
    
    raise "One or more tests failed.  Please see results in #{REPORTS_PATH}\\index.html" unless result == 0

    # If we get here, all's good
    puts "All tests passed"
end