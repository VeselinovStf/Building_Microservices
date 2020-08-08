pipeline {
    agent any

    stages {

        stage("Verify GitHub") {

            steps {
                echo "$GIT_BRANCH"
            }

        }

        stage('Docker Build'){
            
            steps {
                
                powershell('docker images -a')
                powershell('docker build -t justorganizeapp .')
                powershell('docker ps -a')

            }

        }

    }

}