<?php
//variables
$cameraip = "172.16.206.56";
$zoomspeed = "4";
$focusspeed = "4";
$panspeed = "10";
$tiltspeed = "10";
$stopdelay = "1"; //in seconds
?>
<html>
<head>
<title>PTZOptics HTTP-CGI API Demo in PHP</title>
<body>
<!-- script to update the preview image -->
<script>
setInterval(function() {
    var myImageElement = document.getElementById('snapshot');
    myImageElement.src = 'http://<?php echo $cameraip; ?>/snapshot.jpg?rand=' + Math.random();
}, 2000);
</script>
<!-- links to operate the cameras -->
<h1>Directional</h1>
<a href="?x=up">Up</a><br>
<a href="?x=down">Down</a><br>
<a href="?x=left">Left</a><br>
<a href="?x=right">Right</a><br>
<a href="?x=leftup">Left Up</a><br>
<a href="?x=rightup">Right Up</a><br>
<a href="?x=leftdown">Left Down</a><br>
<a href="?x=rightdown">Right Down</a><br>
<br>
<h1>Zoom/Focus</h1>
<a href="?x=zoomin">Zoom In</a><br>
<a href="?x=zoomout">Zoom Out</a><br>
<a href="?x=focusin">Focus In</a><br>
<a href="?x=focusout">Focus Out</a><br>

<h1>Presets - Call</h1>
<a href="?x=preset1">Preset 1</a><br>
<a href="?x=preset2">Preset 2</a><br>
<a href="?x=preset3">Preset 3</a><br>
<a href="?x=preset4">Preset 4</a><br>
<a href="?x=preset5">Preset 5</a><br>
<a href="?x=preset6">Preset 6</a><br>
<a href="?x=preset7">Preset 7</a><br>
<a href="?x=preset8">Preset 8</a><br>
<a href="?x=preset9">Preset 9</a><br>
<br>
<h1>Presets - Set</h1>
<a href="?x=setpreset1">Preset 1</a><br>
<a href="?x=setpreset2">Preset 2</a><br>
<a href="?x=setpreset3">Preset 3</a><br>
<a href="?x=setpreset4">Preset 4</a><br>
<a href="?x=setpreset5">Preset 5</a><br>
<a href="?x=setpreset6">Preset 6</a><br>
<a href="?x=setpreset7">Preset 7</a><br>
<a href="?x=setpreset8">Preset 8</a><br>
<a href="?x=setpreset9">Preset 9</a><br>

<?php
//Looking for the GET variable of X
 $x = $_GET['x'];
//Pending what x= in the URL will tell this if statement what happens next
if ($x == "") { 
} elseif ($x == "up") {
sendToCam($cameraip,"ptzctrl.cgi?ptzcmd&up&" . $panspeed . "&" . $tiltspeed);
sleep($stopdelay);
sendToCam($cameraip,"ptzctrl.cgi?ptzcmd&ptzstop&" . $panspeed . "&" . $tiltspeed);
} elseif ($x == "down") {
sendToCam($cameraip,"ptzctrl.cgi?ptzcmd&down&" . $panspeed . "&" . $tiltspeed);
sleep($stopdelay);
sendToCam($cameraip,"ptzctrl.cgi?ptzcmd&ptzstop&" . $panspeed . "&" . $tiltspeed);
} elseif ($x == "left") {
sendToCam($cameraip,"ptzctrl.cgi?ptzcmd&left&" . $panspeed . "&" . $tiltspeed);
sleep($stopdelay);
sendToCam($cameraip,"ptzctrl.cgi?ptzcmd&ptzstop&" . $panspeed . "&" . $tiltspeed);
} elseif ($x == "right") {
sendToCam($cameraip,"ptzctrl.cgi?ptzcmd&right&" . $panspeed . "&" . $tiltspeed);
sleep($stopdelay);
sendToCam($cameraip,"ptzctrl.cgi?ptzcmd&ptzstop&" . $panspeed . "&" . $tiltspeed);
} elseif ($x == "down") {
sendToCam($cameraip,"ptzctrl.cgi?ptzcmd&down&" . $panspeed . "&" . $tiltspeed);
sleep($stopdelay);
sendToCam($cameraip,"ptzctrl.cgi?ptzcmd&ptzstop&" . $panspeed . "&" . $tiltspeed);
} elseif ($x == "leftup") {
sendToCam($cameraip,"ptzctrl.cgi?ptzcmd&leftup&" . $panspeed . "&" . $tiltspeed);
sleep($stopdelay);
sendToCam($cameraip,"ptzctrl.cgi?ptzcmd&ptzstop&" . $panspeed . "&" . $tiltspeed);
} elseif ($x == "rightup") {
sendToCam($cameraip,"ptzctrl.cgi?ptzcmd&rightup&" . $panspeed . "&" . $tiltspeed);
sleep($stopdelay);
sendToCam($cameraip,"ptzctrl.cgi?ptzcmd&ptzstop&" . $panspeed . "&" . $tiltspeed);
} elseif ($x == "leftdown") {
sendToCam($cameraip,"ptzctrl.cgi?ptzcmd&leftdown&" . $panspeed . "&" . $tiltspeed);
sleep($stopdelay);
sendToCam($cameraip,"ptzctrl.cgi?ptzcmd&ptzstop&" . $panspeed . "&" . $tiltspeed);
} elseif ($x == "rightdown") {
sendToCam($cameraip,"ptzctrl.cgi?ptzcmd&rightdown&" . $panspeed . "&" . $tiltspeed);
sleep($stopdelay);
sendToCam($cameraip,"ptzctrl.cgi?ptzcmd&ptzstop&" . $panspeed . "&" . $tiltspeed);
} elseif ($x == "zoomin") {
sendToCam($cameraip,"ptzctrl.cgi?ptzcmd&zoomin&" . $zoomspeed);
sleep($stopdelay);
sendToCam($cameraip,"ptzctrl.cgi?ptzcmd&zoomstop&" . $zoomspeed);
} elseif ($x == "zoomout") {
sendToCam($cameraip,"ptzctrl.cgi?ptzcmd&zoomout&" . $zoomspeed);
sleep($stopdelay);
sendToCam($cameraip,"ptzctrl.cgi?ptzcmd&zoomstop&" . $zoomspeed);
} elseif ($x == "focusin") {
sendToCam($cameraip,"ptzctrl.cgi?ptzcmd&focusin&" . $focusspeed);
sleep($stopdelay);
sendToCam($cameraip,"ptzctrl.cgi?ptzcmd&focusstop&" . $focusspeed);
} elseif ($x == "focusout") {
sendToCam($cameraip,"ptzctrl.cgi?ptzcmd&focusout&" . $focusspeed);
sleep($stopdelay);
sendToCam($cameraip,"ptzctrl.cgi?ptzcmd&focusstop&" . $focusspeed);
} elseif ($x == "preset1") {
sendToCam($cameraip,"ptzctrl.cgi?ptzcmd&poscall&1");
} elseif ($x == "preset2") {
sendToCam($cameraip,"ptzctrl.cgi?ptzcmd&poscall&2");
} elseif ($x == "preset3") {
sendToCam($cameraip,"ptzctrl.cgi?ptzcmd&poscall&3");
} elseif ($x == "preset4") {
sendToCam($cameraip,"ptzctrl.cgi?ptzcmd&poscall&4");
} elseif ($x == "preset5") {
sendToCam($cameraip,"ptzctrl.cgi?ptzcmd&poscall&5");
} elseif ($x == "preset6") {
sendToCam($cameraip,"ptzctrl.cgi?ptzcmd&poscall&6");
} elseif ($x == "preset7") {
sendToCam($cameraip,"ptzctrl.cgi?ptzcmd&poscall&7");
} elseif ($x == "preset8") {
sendToCam($cameraip,"ptzctrl.cgi?ptzcmd&poscall&8");
} elseif ($x == "preset9") {
sendToCam($cameraip,"ptzctrl.cgi?ptzcmd&poscall&9");
} elseif ($x == "setpreset1") {
sendToCam($cameraip,"ptzctrl.cgi?ptzcmd&posset&1");
} elseif ($x == "setpreset2") {
sendToCam($cameraip,"ptzctrl.cgi?ptzcmd&posset&2");
} elseif ($x == "setpreset3") {
sendToCam($cameraip,"ptzctrl.cgi?ptzcmd&posset&3");
} elseif ($x == "setpreset4") {
sendToCam($cameraip,"ptzctrl.cgi?ptzcmd&posset&4");
} elseif ($x == "setpreset5") {
sendToCam($cameraip,"ptzctrl.cgi?ptzcmd&posset&5");
} elseif ($x == "setpreset6") {
sendToCam($cameraip,"ptzctrl.cgi?ptzcmd&posset&6");
} elseif ($x == "setpreset7") {
sendToCam($cameraip,"ptzctrl.cgi?ptzcmd&posset&7");
} elseif ($x == "setpreset8") {
sendToCam($cameraip,"ptzctrl.cgi?ptzcmd&posset&8");
} elseif ($x == "setpreset9") {
sendToCam($cameraip,"ptzctrl.cgi?ptzcmd&posset&9");
} else {
}


//a function to send the data to the camera that is outlined in the variables section.
function sendToCam($camip, $urlparam) {
// Get cURL resource
$curl = curl_init();
// Set some options
curl_setopt_array($curl, [
    CURLOPT_RETURNTRANSFER => 1,
	//the curl url is pointing to the camera IP.
    CURLOPT_URL => "http://" . $camip . "/cgi-bin/" . $urlparam,
]);
// Send the request & save response to $resp
$resp = curl_exec($curl);
// Close request to clear up some resources
curl_close($curl);
//We aren't using the $resp but we have it just in case it's needed in the future.
         }
?>
<br>
<img src="http://<?php echo $cameraip; ?>/snapshot.jpg" width="600" id="snapshot">
</html>
