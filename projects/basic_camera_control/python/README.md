Camera Control Basics Python
======================

### Prerequisites

- [Python3+](https://www.python.org/downloads/)
- [PTZOptics camera](https://ptzoptics.com/)

### Installing

1. Configure your **PTZOptics camera** to your local network. [PTZOptics Knowledge Base](https://help.ptzoptics.com/support/solutions/folders/13000001062)
2. Clone this repo and then extract to your preferred location
3. Open the **command prompt** and enter the commands in steps 4 - 7
4. Change directories to where you cloned the repository
```
  cd /The/path/to/the/repo
```
5. Install python dependencies using pip
```
  pip install -r requirements.txt
```
6. Set Flask environment to **development**  
```
  set FLASK_ENV=development
```
7. Start the application
```
  python main.py
```
8. The server will output the following
```
  * Serving Flask app "main" (lazy loading)
  * Environment: development
  * Debug mode: on
  * Restarting with stat
  * Debugger is active!
  * Debugger PIN: xxx-xxx-xxx
  * Running on http://localhost:5000/ (Press CTRL+C to quit)
```
9. Open your browser and enter the url from the previous step


### Deployment

This app is not meant for production


## Authors

- [**PTZOptics**](https://ptzoptics.com/)

## License

This project is licensed under the MIT License - see the [LICENSE.txt](LICENSE.txt) file for details
