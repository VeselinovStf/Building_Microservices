pipeline {
    agent any

        environment {
        registry = "sonicsitebuilderdev/docker-hub"
        registryCredential = 'DockerHub'
    }

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

        stage('Building image') {
            steps{
              script {
                dockerImage = docker.build registry + ":$BUILD_NUMBER"
              }
            }
        }

        stage('Deploy Image') {
          steps{
            script {
              docker.withRegistry( '', registryCredential ) {
                dockerImage.push()
              }
            }
          }
        }

        stage("Run Anchore Tests") {
            steps {
                  powershell("powershell.exe Write-Output 'src/JustOrganize.TeamService' > anchore_images")
                  anchore name: 'anchore_images'            
                               
            }
        }

        stage('Remove Unused docker image') {
            steps{
              sh "docker rmi $registry:$BUILD_NUMBER"
            }
        }
     

    }

}