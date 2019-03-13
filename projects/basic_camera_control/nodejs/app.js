// Here we are adding dependencies and declaring them to varibles.
var express = require('express');
var path = require('path');
var bodyParser = require('body-parser');
var request = require('request');

// Express is a widely used minimal and flexible Node.js web framework
// For more info about Express visit https://expressjs.com/
// Here we are declaring the varible app to a new express instance.
var app = express();

// Here we are adding and configuring a middleware to the express instance. Body Parser helps us read incoming http request.
app.use(bodyParser.urlencoded({extended: true}));
app.use(bodyParser.json());

app.set('views', path.join(__dirname, './views'));
// app.locals.pretty will make sure the app will output readable formatted HTML
app.locals.pretty = true;

// This configures the templating library dot-emc
// Templating Engines allows the server to render specific content into our html code.
app.engine('html', require('dot-emc').init({
    app: app,
    fileExtension: "html",
    options: {
        templateSettings: {
            cache: false
        }
    }
}).__express);
app.set("view engine", "html");
// Here we are telling the app where to find the files we need for the templating engine.
app.use(express.static(path.join(__dirname, '.../view'), {
    maxAge: 1
}));

/*******************************************routes********************************************/
// A route is the code we use to associate the type of HTTP request we receive and the url path pattern.
// A route allows us to code a specific response for a specific request

// In this case we are looking for any post request that was sent with a url ending in '/camctrl'
// Inside the function you will see 'req', and 'res' which represent 'Request' and 'Response' objects.
// The request and response objects represent the data that is received and sent to the user respectively
app.post('/camctrl', function(req, res) {
    // The Body Parser middleware grabbed the data included with the request and placed it in req.body.
    // To see what else is inside the request body object. Uncomment the code (remove the slashes) below and run the app again.
    // console.log(req.body);
    var param = req.body.param;
    sendCameraControl(buildUrl(param));
    // res.json is the response data we will send back to the client. The data will get sent as JSON
    res.json({
        status: "Success"
    });
});

// Render and send the html file to the requester
app.get('/', function(req, res) {
    res.render('index');
});

// This function receieves a url string and sends a "GET" HTTP request to the url
function sendCameraControl(url) {
    request(url, {method: 'GET'
}, function(err, res) {
        if (err) {
            console.log("Error: ", err);
        } else {
            console.log("Command: " + url + " was successful");
        }
    });
}

// The comment below is used for automating documentation.
// We will learn more about code documentation in a later course.

/**
 * Build url based on ip address and action
 * @param: camera control parameter object
 * {
 *   ip: '192.168.1.60',
 *   action: 'home|up|down|left|right|ptzstop|zoomin|zoomout|zoomstop|focusin|focusout|focusstop|posset|poscall',
 *   param: {
 *       panSpeed: number,
 *       tiltSpeed: number,
 *       zoomSpeed: number,
 *       focusSpeed: number,
 *       presetIndex: number
 *   }
 * }
 * base url: http://[ip]/cgi-bin/ptzctrl.cgi?ptzcmd[&[home|up|down|left|right|ptzstop]&panspeed&tiltspeed][&[zoomin|zoomout|zoomstop|focusin|focusout|focusstop]&[focusspeed|zoomspeed]][&[posset|poscall]&index]
 * e.g http://192.168.1.60/cgi-bin/ptzctrl.cgi?ptzcmd&up&10&10
 **/

// buildUrl will create the HTTP CGI commands using the data passed to it.
// Once finished it will return the processed url to the function that called it.
function buildUrl(param) {
    var url = 'http://' + param.ip + '/cgi-bin/ptzctrl.cgi?ptzcmd&';
    var action = param.action;
    // To learn more about switch statements head to
    // https://developer.mozilla.org/en-US/docs/Web/JavaScript/Reference/Statements/switch
    switch (action) {
        // All vector motion actions
        case "up":
        case "down":
        case "left":
        case "right":
            return url + action + '&' + param.panSpeed + '&' + param.tiltSpeed;

        // All non-directional motion actions
        case "home":
        case "ptzstop":
            return url + action;
        // All Zoom actions
        case "zoomin":
        case "zoomout":
        case "zoomstop":
            return url + action + '&' + param.zoomSpeed;

        // All Focus actions
        case "focusin":
        case "focusout":
        case "focusstop":
            return url + action + '&' + param.focusSpeed;

        // All preset actions
        case "postset":
        case "postcall":
            return url + action + '&' + param.presetNumber;

        // Default action is to send 'home' cgi command
        default:
            return url + 'home' + '&10&10';
    }
}
// Here we are exporting the app.js file so our server-configuration file "bin/www"
module.exports = app;
