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
                powershell('docker-compose build')
                powershell('docker ps -a')
            }
        }


        stage("Run Dev App") {
            steps {
                powershell('docker-compose up -d')
            }
            post {
                success {
                    echo "App is running successfully"
                }
                failure {
                    echo "App failed to start"
                }
            }
        }

        stage("Run Tests") {
            steps {
                powershell('dotnet test')
            }
        }

        stage("Push Container") {
            steps {
                echo "Workspace is $WORKSPACE"
                script {
                    docker.withRegistry('https://registry.hub.docker.com', 'DockerHub'){
                        def image = docker.build('web-api:latest')
                        image.push()
                    }
                }
            }
        }

        stage("Run Anchore Tests") {
            steps {
                anchore 'anchore_images'
            }
        }
     

    }

}