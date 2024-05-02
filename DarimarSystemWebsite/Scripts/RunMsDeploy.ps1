$msdeploy = "C:\Program Files (x86)\IIS\Microsoft Web Deploy V3\msdeploy.exe";

$publishBinPath = $args[0]
$hostSiteName = $args[1]
$computerName = $args[2]
$username = $args[3]
$password = $args[4]

$computerNameArgument = $computerName + '/MsDeploy.axd?site=' + $hostSiteName

$msdeployArguments = 
    "-verb:sync",
    "-allowUntrusted",
    "-source:contentPath=${publishBinPath}",
    ("-dest:" + 
        "contentPath=${hostSiteName}," +
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
