docker build -t manheim-data --build-arg FILES_FOLDER=/files/ .
docker run -i -v ~/files:/files manheim-data