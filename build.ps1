properties {
    $testResultPath = '.\test-results'
    $solutionPath = '.\NitriqTeamCity.sln'
}

task default -depends Test

task Coverage -depends Compile, PreTest, Clean {
    Exec { .\packages\OpenCover.4.0.724\OpenCover.console.exe "-target:.\packages\NUnit.Runners.2.6.1\tools\nunit-console.exe" "-targetargs:/nologo /noshadow /result=./test-results/result.xml ./NitriqTeamCity.Tests/bin/Release/NitriqTeamCity.Tests.dll" -filter:"+[NitriqTeamCity]*" -output:"./test-results/coverage.xml" -register:user }
    Exec { .\packages\ReportGenerator.1.5.0.0\ReportGenerator.exe "-reports:./test-results/coverage.xml" "-targetdir:./test-results/" }
}

task Test -depends Compile, PreTest, Clean {
    Exec { .\packages\NUnit.Runners.2.6.1\tools\nunit-console.exe /nologo /noshadow /result=$testResultPath\result.xml ./NitriqTeamCity.Tests\bin\Release\NitriqTeamCity.Tests.dll }
}

task PreTest {
    New-Item "$testResultPath" -Type directory
}

task Compile -depends Clean {
    Exec { msbuild "$solutionPath" /t:Build /p:Configuration=Release /v:quiet }
}

task Clean {
    if (Test-Path "$testResultPath") {
        Remove-Item -Recurse "$testResultPath"
    }
    Exec { msbuild "$solutionPath" /t:Clean /p:Configuration=Release /v:quiet } 
}