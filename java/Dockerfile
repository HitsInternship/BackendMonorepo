FROM gradle:8.5-jdk21 AS build
WORKDIR /app
COPY java/ .
RUN gradle build -x test

FROM eclipse-temurin:21-jdk-jammy
WORKDIR /app
COPY --from=build /app/build/libs/*.jar app.jar
ENTRYPOINT ["java", "-jar", "app.jar"]