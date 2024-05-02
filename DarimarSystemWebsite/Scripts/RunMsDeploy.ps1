$msdeploy = "C:\Program Files (x86)\IIS\Microsoft Web Deploy V3\msdeploy.exe";

$contentPath = $args[0]
$computerName = $args[1]
$username = $args[2]
$password = $args[3]

$computerNameArgument = $computerName + '/MsDeploy.axd?site=' + $contentPath

$msdeployArguments = 
    "-verb:sync",
    "-allowUntrusted",
    "-source:contentPath",
    ("-dest:" + 
        "contentPath=${contentPath}," +
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
