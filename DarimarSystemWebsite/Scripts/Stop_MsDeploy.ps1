$msdeploy = "C:\Program Files (x86)\IIS\Microsoft Web Deploy V3\msdeploy.exe";

$recycleApp = $args[0]
$hostSiteName = $args[1]
$computerName = $args[2]
$username = $args[3]
$password = $args[4]

$computerNameArgument = $computerName + '/MsDeploy.axd?site=' + $hostSiteName

$msdeployArguments = 
    "-verb:sync",
    "-allowUntrusted",
    "-source:recycleApp",
    ("-dest:" + 
        "recycleApp=${recycleApp}," +
        "recycleMode=StopAppPool," +
        "computerName=${computerNameArgument}," + 
        "username=${username}," +
        "password=${password}," +
        "AuthType='Basic'"
    )

& $msdeploy $msdeployArguments