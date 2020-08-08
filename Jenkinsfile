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
                
                pwsh(script: 'docker images -a')

            }

        }

    }

}