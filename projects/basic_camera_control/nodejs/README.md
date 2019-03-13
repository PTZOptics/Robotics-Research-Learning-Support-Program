Camera Control Basics NodeJS
======================

### Prerequisites

- [Node.js LTS versions: 10+](https://nodejs.org/en/download/)
- [PTZOptics camera](https://ptzoptics.com/)

### Installing

1. Configure your **PTZOptics camera** to your local network. [PTZOptics Knowledge Base](https://help.ptzoptics.com/support/solutions/folders/13000001062)
2. Clone this repo and then extract to your preferred location
3. Open the **command prompt** and enter the commands in steps 4 - 7
4. Change directories to where you cloned the repository
```
  cd /The/path/to/the/repo
```
5. Install npm packages
```
  npm install
```
7. Start the application
```
  npm start
```
8. The server will output the following
```
...
PTZOptics Camera Control Sample HTTP Service is running on port 8081
```
9. Open your browser and enter the url from the previous step


### Deployment

This app is not meant for production

## Authors

- [**PTZOptics**](https://ptzoptics.com/)

## License

This project is licensed under the MIT License - see the [LICENSE.txt](LICENSE.txt) file for details
