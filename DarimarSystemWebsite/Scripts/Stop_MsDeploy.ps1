$msdeploy = "C:\Program Files (x86)\IIS\Microsoft Web Deploy V3\msdeploy.exe";

$offlinePath = $args[0]
$publishBinPath = $args[1]
$hostSiteName = $args[2]
$computerName = $args[3]
$username = $args[4]
$password = $args[5]

Copy-Item "${offlinePath}\app_offline.htm" -Destination $publishBinPath

$computerNameArgument = $computerName + '/MsDeploy.axd?site=' + $hostSiteName

$msdeployArguments = 
    "-verb:sync",
    "-allowUntrusted",
    "-source:contentPath=${offlinePath}\app_offline.htm",
    ("-dest:" + 
        "contentPath=${hostSiteName}\app_offline.htm," +
        "computerName=${computerNameArgument}," + 
        "username=${username}," +
        "password=${password}," +
        "authtype='Basic'," +
        "includeAcls='False'"
    ),
    "-disableLink:AppPoolExtension",
    "-disableLink:ContentExtension",
    "-disableLink:CertificateExtension",
    "-verbose"

& $msdeploy $msdeployArguments
