FROM gradle:8.5-jdk21 AS build
WORKDIR /app
COPY /java .
RUN gradle bootJar -x test && ls -lah build/libs && cp $(ls build/libs/*.jar | head -n 1) app.jar

FROM eclipse-temurin:21-jdk-jammy
WORKDIR /app
COPY --from=build /app/app.jar app.jar
ENTRYPOINT ["java", "-jar", "app.jar"]
