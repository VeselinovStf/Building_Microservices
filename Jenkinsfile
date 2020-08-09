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

        stage('Container Scanning'){
            parallel{

                stage("Run Anchore"){
                    steps {
                        pwsh(script: """
                                Write-Output "src/JustOrganize.TeamService" > anchore_images
                            """
                        )
                        anchore bailOnFail: false, bailOnPluginFail: false, name: 'anchore_images'
                    }
                        
                }

                stage("Run Trivy") {
                    steps {
                        sleep(time: 10, unit: 'SECONDS')

                    }
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

    
    }

}