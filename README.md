# Qubeyond.WordFinder.Api

## Description 

Qubeyond.WordFinder.Api is a fun project created for the Que Beyond Challenge. It’s built with .NET 8 and runs in a container, making it super easy to set up and use. We’re using Redis to cache results, which means everything runs faster and smoother. Dive in and check it out

### System Overview:

Here's a quick snapshot of what you'll find inside:

1. **Qubeyond.WordFinder.Api**: Manages the WordFinder logic for the QU Beyond Challenge. 
2. **Redis**: Enhances the performance of Qubeyond.WordFinder.Api.
3. **Fluent Validation** (just in case to approve the change)


### Development Environment Setup 

To get things rolling, make sure you've got these tools in your toolkit:

1. Docker - You can download and install Docker from [docker.com](https://www.docker.com/products/docker-desktop).

### Running the Local Environment 

Ready to fire things up locally? Just follow these simple steps:

1. Open a terminal and navigate to the root folder of the project.

2. Now, let's kickstart the local development environment with this command:

   ```bash
   docker-compose up --build

## Possible Improvements
 I’ve implemented two versions of the solution: one that sticks to the original requirements and another that brings in some improvements with FluentValidation.
 
 1. **Original Implementation:** The WordFinder class is intact and meets the challenge's requirements. This version is designed to fit the original challenge specs perfectly.
 
 2. **Improved Implementation:** I've created a second version that uses FluentValidation to step up the validation game. This service runs the search method and checks the matrix and word stream for validity before proceeding, ensuring everything's in order. For this implementation to work, you’ll need to comment out the Validation method in WordFinder, and you could also remove the Find method from WordFinder. While this version is more robust and easier to read, it’s available as an optional add-on and doesn’t mess with the original functionality.
 
### Why the Improvement?
1. **Separation of Concerns:** I separated the search logic from the validation, making the code clearer and more organized.

2. **Better Maintainability:** Using FluentValidation provides a clearer and more modular way to validate, which helps keep things manageable in the long run.

### Documentation

After you have the environment up, if you want to check Swagger info you can go: http://localhost:3001/swagger/index.html 
### Unit Tests 

We’ve set up some tests to help ensure everything works as expected. To run these tests and keep everything running smoothly, just follow these steps:

1. Navigate to the project's root folder.
2. Jump into the qubeyond.wordfinder.unitTest folder:
   ```bash
   cd qubeyond.wordfinder.unitTest
3. Execute the tests using this command:
   ```bash
   dotnet test
