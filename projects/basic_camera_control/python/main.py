#!/usr/bin/env python

# Flask is a widely used minimal and flexible Python micro web framework
# For more information about Flask visit http://flask.pocoo.org/
from flask import Flask, redirect, request, url_for, send_file
# Here we are importing a library that helps send http request
import requests
# here we are declaring the variable to a new flask instance
app = Flask(__name__, static_url_path='',static_folder="/")

# This function receieves a url string and sends a "GET" HTTP request to the url
def sendCameraControl(url):
    r = requests.get(url)
    if r.status_code == requests.codes.ok:
        print("Command: " + url + " was successful")
        return "success"
    else:
        print("An Error occured while ")
        return "success"

# buildUrl will create the HTTP CGI commands using the data passed to it.
# Once finished it will return the processed url to the function that called it.
def buildCgiUrl(req):
    url = 'http://' + req["param[ip]"] + '/cgi-bin/ptzctrl.cgi?ptzcmd&'
    action = req["param[action]"]

    # All vector motion actions
    if action == "up" or action == "down" or action == "left" or action =="right":
        panSpeed = req["param[panSpeed]"]
        tiltSpeed = req["param[tiltSpeed]"]
        return url + action + '&' + panSpeed + '&' + tiltSpeed
    # All non-directional motion actions
    elif action == "home" or action == "ptzstop":
        return url + action
    # All Focus actions
    elif action == "focusin" or action == "focusout" or action == 'focusstop':
        focusSpeed = req["param[focusSpeed]"]
        return url + action + '&' + focusSpeed
    # All Zoom actions
    elif action == "zoomin" or action == "zoomout" or action == 'zoomstop':
        zoomSpeed = req["param[zoomSpeed]"]
        return url + action + '&' + zoomSpeed
    else:
        return url + 'home' + '&10&10';

# Render and send the html file to the requester
@app.route('/')
def root():
    return send_file('index.html')

# A route is the code we use to associate the type of HTTP request we receive and the url path pattern.
# A route allows us to code a specific response for a specific request
# In this case we are looking for any post request that was sent with a url ending in '/camctrl'
@app.route('/camctrl', methods=['POST'])
def post():
    return sendCameraControl(buildCgiUrl(request.form))

if __name__ == '__main__':
    import sys
    port = int(sys.argv[1]) if len(sys.argv) > 1 else None
    app.run(host='localhost', port=port)
