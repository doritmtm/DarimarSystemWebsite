$msdeploy = "C:\Program Files (x86)\IIS\Microsoft Web Deploy V3\msdeploy.exe";

$hostSiteName = $args[0]
$computerName = $args[1]
$username = $args[2]
$password = $args[3]

$computerNameArgument = $computerName + '/MsDeploy.axd?site=' + $hostSiteName

$msdeployArguments = 
    "-verb:delete",
    "-allowUntrusted",
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
